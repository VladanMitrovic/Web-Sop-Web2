import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate, useParams } from 'react-router';

const IzmeniArtikal = ({ user }) => {
  const [naziv, setNaziv] = useState('');
  const [opis, setOpis] = useState('');
  const [cena, setCena] = useState(0);
  const [kolicina, setKolicina] = useState(0);
  const navigate = useNavigate();

  const { Id } = useParams();
  console.log("Article id u editu:",Id);
  

  const handleFormSubmit = (event) => {
    event.preventDefault();
    console.log("Article id u editu:",Id);

    // Create an ArticleDto object with the updated values
    const updatedArticle = {
      Id: Id,
      Naziv: naziv,
      Opis: opis,
      Cena: cena,
      kolicina: kolicina,
      ProdavacId: user.id
    };

    
    try {
      const odgovor =  axios.put(`${process.env.REACT_APP_ARTICLES}/${user.id}`, updatedArticle);
      console.log('Uspešno ste izmenili proizvod:', odgovor.data);
      window.alert('Uspešno ste izmenili proizvod');
      navigate('/home');
    } catch (error) {
      console.error('Greška prilikom izmene proizvoda:', error);
    }
  };

  return (
    <div className="izmeni-artikal-container">
      <h2 style={{ color: 'Black' }}>Izmeni Artikal</h2>
      <form onSubmit={handleFormSubmit} className="edit-article-form">
        <div className="izmeni-artikal-field">
          <label className="izmeni-artikal-label">Ime artikla:</label>
          <input
            type="text"
            value={naziv}
            placeholder='Unesite nov naziv proizvoda'
            onChange={(event) => setNaziv(event.target.value)}
            className="izmeni-artikal-input"
          />
        </div>
        <div className="izmeni-artikal-field">
          <label className="izmeni-artikal-label">Cena:</label>
          <input
            type="number"
            value={cena}
            onChange={(event) => setCena(Number(event.target.value))}
            className="izmeni-artikal-input"
          />
        </div>
        <div className="izmeni-artikal-field">
          <label className="izmeni-artikal-label">Kolicina:</label>
          <input
            type="number"
            value={kolicina}
            onChange={(event) => setKolicina(Number(event.target.value))}
            className="izmeni-artikal-input"
          />
        </div>
        <div className="izmeni-artikal-field">
          <label className="izmeni-artikal-label">Opis:</label>
          <textarea
            value={opis}
            onChange={(event) => setOpis(event.target.value)}
            placeholder='Opis proizvoda'
            className="izmeni-artikal-textarea"
          ></textarea>
        </div>
        <button type="submit" className="izmeni-artikal-button">Izmeni artikal</button>
      </form>
    </div>
  );
};

export default IzmeniArtikal;
