import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router';

const NarudzbineKorisnik = ({ user }) => {
  const [narudzbinas, setNarudzbinas] = useState([]);
  const navigate = useNavigate();

  
  useEffect(() => {
    const fetchData = async () => {
      try {
        if (!user) {
          return;
        }
  
        const odgovor = await axios.get(process.env.REACT_APP_GET_ORDERS);
        const filtriraneNarudzbine = NarudzbinePoTipu(odgovor.data, user);
  
        setNarudzbinas(filtriraneNarudzbine);
      } catch (error) {
        console.error('Greška pri dohvatanju porudžbina:', error);
      }
    };
  
    fetchData();
  }, [user]);
  
  const NarudzbinePoTipu = (orders, user) => {
    let filtriraneNarudzbine = [];
    if (user.tipKorisnika === 2) {
      filtriraneNarudzbine = orders.filter(order => order.pordavacId === user.id && new Date(order.datumStizanja) < new Date());
    } else {
      filtriraneNarudzbine = orders.filter(order => new Date(order.datumStizanja) < new Date());
    }
    return filtriraneNarudzbine;
  };

  const Home = () => {
    navigate('/home');
  };

  const CenaSaDostavom = (cena) => cena + 400;

  const Datum = (datum) => {
    const options = { year: 'numeric', month: 'long', day: 'numeric', hour: 'numeric', minute: 'numeric' };
    return new Date(datum).toLocaleDateString(undefined, options);
  };

  return (
    <div className="kontejner-tabele-artikala" style={{ color: 'silver' }}>
      <button className="dugme-nazad" style={{ width: '100%' }} onClick={Home}>
        Nazad
      </button>
      <table className="tabela-artikala">
        <thead>
          <tr>
            <th>ID</th>
            <th>Kolicina</th>
            <th>Ime artikla</th>
            <th>Prodavac</th>
            <th>Status</th>
            <th>Adresa</th>
            <th>Cena(din)</th>
            <th>Komentar</th>
            <th>Datum Porucivanja</th>
            <th>Datum stizanja</th>
          </tr>
        </thead>
        <tbody>
          {narudzbinas
            .map(narudzbina => (
            <tr key={narudzbina.id}>
              <td>{narudzbina.id}</td>
              <td>{narudzbina.kolicina}</td>
              <td>{narudzbina.imeArtikla}</td>
              <td>{narudzbina.imeProdavca}</td>
              <td>{narudzbina.status}</td>
              <td>{narudzbina.adresa}</td>
              <td>{CenaSaDostavom(narudzbina.cena)}</td>
              <td>{narudzbina.komentar}</td>
              <td>{Datum(narudzbina.datumPorucivanja)}</td>
              <td>{Datum(narudzbina.datumStizanja)}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default NarudzbineKorisnik;