import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';


const IzmeniKorisnika = ({ user, onUpdate }) => {
  const [korisnikUpdateDto, setKorisnikUpdateDto] = useState({
    KorisnickoIme: '',
    Email: '',
    Ime: '',
    Prezime: '',
    DatumRodjenja: '',
    Adresa: '',
  });

  const [isUpdated, setIsUpdated] = useState(false);
  const navigate = useNavigate(); 
  
  const Submit = async (e) => {
    e.preventDefault();
  
    try {
      const response = await axios.put(`${process.env.REACT_APP_USERS}/${user.id}`, korisnikUpdateDto);
      onUpdate(response.data);
      setIsUpdated(true);
      localStorage.setItem('isUpdated', 'true');
      navigate('/izmeni');
    } catch (error) {
      if (error.response) {
        console.error('Greška prilikom izmene podataka. Server je vratio sledeći status:', error.response.status);
        console.error('Poruka od servera:', error.response.data);
      } else if (error.request) {
        
        console.error('Nije stigao odgovor od servera. Proverite konekciju ili pokušajte kasnije.');
      } else {
      
        console.error('Greška prilikom obrade zahteva:', error.message);
      }
    }
  };

  
  const handleGoBack = () => {
    navigate('/home');
  };
 
  useEffect(() => {
    const storedIsUpdated = localStorage.getItem('isUpdated');
    if (storedIsUpdated === 'true') {
      setIsUpdated(true);
      window.location.reload();
      localStorage.removeItem('isUpdated'); 
    }
  }, []);
  
 
  useEffect(() => {
    
    if (user) {
      setKorisnikUpdateDto({
        KorisnickoIme: user.korisnickoIme || '',
        Email: user.email || '',
        Ime: user.ime || '',
        Prezime: user.prezime || '',
        DatumRodjenja: user.datumRodjenja || '',
        Adresa: user.adresa || '',
      });
    }
  }, [user]);

  return (
    <form className="forma-azuriranje" onSubmit={Submit}>
  <h1>Izmeni svoj profil</h1>
  <div className="form-grupa">
    <label htmlFor="KorisnickoIme">Korisničko Ime:</label>
    <input
      type="text"
      id="KorisnickoIme"
      className="form-input"
      value={korisnikUpdateDto.KorisnickoIme}
      onChange={(e) =>
        setKorisnikUpdateDto({
          ...korisnikUpdateDto,
          KorisnickoIme: e.target.value,
        })
      }
    />
  </div>
  <div className="form-grupa">
    <label htmlFor="Email">Email:</label>
    <input
      type="email"
      id="Email"
      className="form-input"
      value={korisnikUpdateDto.Email}
      onChange={(e) =>
        setKorisnikUpdateDto({
          ...korisnikUpdateDto,
          Email: e.target.value,
        })
      }
    />
  </div>
  <div className="form-grupa">
    <label htmlFor="Ime">Ime:</label>
    <input
      type="text"
      id="Ime"
      className="form-input"
      value={korisnikUpdateDto.Ime}
      onChange={(e) =>
        setKorisnikUpdateDto({
          ...korisnikUpdateDto,
          Ime: e.target.value,
        })
      }
    />
  </div>
  <div className="form-grupa">
    <label htmlFor="Prezime">Prezime:</label>
    <input
      type="text"
      id="Prezime"
      className="form-input"
      value={korisnikUpdateDto.Prezime}
      onChange={(e) =>
        setKorisnikUpdateDto({
          ...korisnikUpdateDto,
          Prezime: e.target.value,
        })
      }
    />
  </div>
  <div className="form-grupa">
    <label htmlFor="Adresa">Adresa:</label>
    <input
      type="text"
      id="Adresa"
      className="form-input"
      value={korisnikUpdateDto.Adresa}
      onChange={(e) =>
        setKorisnikUpdateDto({
          ...korisnikUpdateDto,
          Adresa: e.target.value,
        })
      }
    />
  </div>
  <div className="form-grupa">
    <label htmlFor="DatumRodjenja">Datum Rodjenja:</label>
    <input
      type="date"
      id="DatumRodjenja"
      className="form-input"
      value={korisnikUpdateDto.DatumRodjenja}
      onChange={(e) =>
        setKorisnikUpdateDto({
          ...korisnikUpdateDto,
          DatumRodjenja: e.target.value,
        })
      }
    />
  </div>
  <button type="submit" className="btn-sacuvaj">
    Sačuvaj Izmene
  </button>
  <button type="button" onClick={handleGoBack} className="btn-nazad">
    Nazad
  </button>
</form>

  );
};

export default IzmeniKorisnika;
