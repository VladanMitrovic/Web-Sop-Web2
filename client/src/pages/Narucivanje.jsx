import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router';

const Narucivanje = ({ article, quantity, user }) => {
  const [adresa, setAdresa] = useState('');
  const [komentar, setKomentar] = useState('');
  const navigate = useNavigate();

  const narudzbinaDto = {
    Kolicina: quantity,
    ArtikalId: article.id,
    ImeArtikla: article.naziv,
    ImeKupca: user.ime,
    ImeProdavca: "", 
    KupacId: user.id,
    Status: 1,
    Adresa: adresa,
    Komentar: komentar,
    
  }

  const Submit = (event) => {
    event.preventDefault();
  
    if (!adresa || !komentar) {
      window.alert('Molimo popunite sva polja pre nego što pošaljete narudžbinu.');
      return;
    }
  
    axios
      .post(process.env.REACT_APP_GET_ORDERS, narudzbinaDto, { params: { userId: user.id } })
      .then((response) => {
        // Handle the successful response
        console.log(response.data);
        //window.alert("Uspesno ste porucili robu pritisnite aktivne porudzbine da vidite");
        navigate('/aktivneNarudzbineKorisnik');
      })
      .catch((error) => {
        // Handle the error
        if (error.response) {
          console.error('Greška pri narudžbini:', error.response.data);
          window.alert('Greška pri narudžbini: ' + error.response.data.message);
        } else if (error.request) {
          console.error('Nema odgovora na zahtev:', error.request);
          window.alert('Nema odgovora na zahtev. Pokušajte ponovo kasnije.');
        } else {
          console.error('Nastala je greška:', error.message);
          window.alert('Nastala je greška. Pokušajte ponovo kasnije.');
        }
      });
  
    // Reset the form only after a successful request
    setAdresa('');
    setKomentar('');
  };

  const promenaKomentara = (event) => {
    setKomentar(event.target.value);
  };

  const promenaAdrese = (event) => {
    const novaAdresa = event.target.value;
    console.log(novaAdresa);
    setAdresa(novaAdresa === '' ? user.adresa : novaAdresa);
  };

  return (
    <form onSubmit={Submit}>
      <div>
        <label htmlFor="address">Adresa:</label>
        <input
          type="text"
          id="address"
          placeholder={user.adresa}
          onChange={promenaAdrese}
        />
      </div>
      <div>
        <label htmlFor="comment">Komentar:</label>
        <textarea
          id="comment"
          value={komentar}
          onChange={promenaKomentara}
        ></textarea>
      </div>
      <button type="submit" className="submit-button" >Poruci</button>
    </form>
  );
};

export default Narucivanje;
