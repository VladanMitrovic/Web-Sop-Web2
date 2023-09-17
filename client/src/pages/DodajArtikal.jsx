import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router';

const DodajArtikal = ({ user }) => {
  const [naziv, setNaziv] = useState('');
  const [kolicina, setKolicina] = useState('');
  const [opis, setOpis] = useState('');
  const [cena, setCena] = useState('');
  
  
  const navigate = useNavigate();

  const Submit = (e) => {
    e.preventDefault();

    const artikalDtoAdd = {
      Naziv: naziv,
      Kolicina: kolicina,
      Opis: opis,
      Cena: cena,
    };

    try {
      const response = axios.post(process.env.REACT_APP_ARTICLES, artikalDtoAdd, { params: { prodavacId: user.id } });
      console.log('Uspesno ste dodali proizvod:', response.data);
      navigate('/home');
    } catch (error) {
      console.error('Greska prilikom pravljenja proizvoda:', error);
    }
  };

  return (
    <form onSubmit={Submit} className="forma-artikla" style={{ color: 'black' }}>
      <div className="form-grupa">
        <br />
        <h1>Dodaj novi artikal</h1>
        <br />
        <br />
        <label htmlFor="name">Naziv artikla:</label>
        <input
          type="text"
          id="name"
          value={naziv}
          onChange={(e) => setNaziv(e.target.value)}
          className="form-control"
        />
      </div>
      <div className="form-grupa">
        <label htmlFor="price">Cena:</label>
        <input
          type="number"
          id="price"
          value={cena}
          onChange={(e) => setCena(e.target.value)}
          className="form-control"
        />
      </div>
      <div className="form-grupa">
        <label htmlFor="quantity">Kolicina:</label>
        <input
          type="number"
          id="quantity"
          value={kolicina}
          onChange={(e) => setKolicina(e.target.value)}
          className="form-control"
        />
      </div>
      <div className="form-grupa">
        <label htmlFor="description">Opis:</label>
        <textarea
          id="description"
          value={opis}
          onChange={(e) => setOpis(e.target.value)}
          className="form-control"
        />
      </div>
      <button type="submit" className="btn btn-dodaj">
        Dodaj
      </button>
    </form>
  );
};

export default DodajArtikal;
