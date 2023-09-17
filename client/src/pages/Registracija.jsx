import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router';

function Registracija(props) {
  const [korisnickoIme, setKorisnickoIme] = useState('');
  const [lozinka, setLozinka] = useState('');
  const [email, setEmail] = useState('');
  const [ime, setIme] = useState('');
  const [prezime, setPrezime] = useState('');
  const [adresa, setAdresa] = useState('');
  const [datumRodjenja, setDatumRodjenja] = useState('');
  const [tipKorisnika, setTipKorisnika] = useState('');
  const [slikaKorisnika, setSlikaKorisnika] = useState('');
  const [errors, setErrors] = useState({});
  const navigate = useNavigate();

  const Registracija = async (e) => {
    e.preventDefault();
    const korisnikDto = {
      KorisnickoIme: korisnickoIme,
      Lozinka: lozinka,
      Email: email,
      Ime: ime,
      Prezime: prezime,
      Adresa: adresa,
      DatumRodjenja: datumRodjenja,
      TipKorisnika: parseInt(tipKorisnika),
      SlikaKorisnika: slikaKorisnika,
    };

    try {
      const response = await axios.post(process.env.REACT_APP_USERS, korisnikDto, {
        headers: {
          'Content-Type': 'application/json',
        },
      });
    
      console.log('Registracija je uspesna:', response.data);
      navigate('/');
    } catch (error) {
      if (error.response) {
        const { status } = error.response;
        const errorMessage = status === 400 ? 'Neispravni podaci' : 'Došlo je do greške';
        window.alert(errorMessage);
        setErrors(error.response.data.errors);
      } else {
        console.error(error);
        window.alert('Došlo je do greške');
      }
      navigate('/registracija');
    }
  };

  return (
    <form className="forma-registracije" onSubmit={Registracija}>
  <br />
  <br />
  <h3 style={{ color: 'black' }}>Registruj se</h3>
  <br />
  <div className="form-grupa">
    <input
      type="text"
      className="form-input"
      placeholder="Korisničko ime"
      value={korisnickoIme}
      onChange={(e) => setKorisnickoIme(e.target.value)}
    />
    {errors.KorisnickoIme && <div className="neispravan-unos">{errors.KorisnickoIme[0]}</div>}
  </div>
  <div className="form-grupa">
    <input
      type="email"
      className="form-input"
      placeholder="Email"
      value={email}
      onChange={(e) => setEmail(e.target.value)}
    />
  </div>
  <div className="form-grupa">
    <input
      type="password"
      className="form-input"
      placeholder="Lozinka"
      value={lozinka}
      onChange={(e) => setLozinka(e.target.value)}
    />
  </div>
  <div className="form-grupa">
    <input
      type="text"
      className="form-input"
      placeholder="Ime"
      value={ime}
      onChange={(e) => setIme(e.target.value)}
    />
  </div>
  <div className="form-grupa">
    <input
      type="text"
      className="form-input"
      placeholder="Prezime"
      value={prezime}
      onChange={(e) => setPrezime(e.target.value)}
    />
  </div>
  <div className="form-grupa">
    <input
      type="text"
      className="form-input"
      placeholder="Adresa"
      value={adresa}
      onChange={(e) => setAdresa(e.target.value)}
    />
  </div>
  <div className="form-grupa">
  <label className="uloga-label" style={{ color: 'black' }}>
      Datum rodjenja?
    </label>
    <input
      type="date"
      className="form-input"
      placeholder="Datum rođenja"
      value={datumRodjenja}
      onChange={(e) => setDatumRodjenja(e.target.value)}
    />
  </div>
  <div className="form-grupa">
    <label className="uloga-label" style={{ color: 'black' }}>
      Uloga?
    </label>
    <select className="form-input" value={tipKorisnika} onChange={(e) => setTipKorisnika(e.target.value)}>
      <option value="0">Administrator</option>
      <option value="1">Kupac</option>
      <option value="2">Prodavac</option>
    </select>
  </div>
  <div className="form-grupa">
    <input
      type="text"
      className="form-input"
      placeholder="Slika korisnika"
      value={slikaKorisnika}
      onChange={(e) => setSlikaKorisnika(e.target.value)}
    />
  </div>
  <button type="submit" className="btn btn-primary">
    Registruj se
  </button>
  <div>
    <br />
    <button type="button" className="btn btn-secondary" onClick={() => navigate('/media')}>
      Registruj se preko društvene mreže
    </button>
  </div>
</form>

  );
}

export default Registracija;
