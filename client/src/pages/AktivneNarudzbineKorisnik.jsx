import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router';

const AktivneNarudzbineKorisnik = ({ user }) => {
  const [narudzbinas, setNarudzbinas] = useState([]);
  const navigate = useNavigate();

  const Home = () => {
    navigate('/home');
  };
  //console.log('Vrednost korisnika:', user);
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
      filtriraneNarudzbine = orders.filter(order => order.pordavacId === user.id && new Date(order.datumStizanja) > new Date());
    } else {
      filtriraneNarudzbine = orders.filter(order => new Date(order.datumStizanja) > new Date());
    }
    return filtriraneNarudzbine;
  };

  const formatDate = dateString => {
    const options = { year: 'numeric', month: 'long', day: 'numeric', hour: 'numeric', minute: 'numeric' };
    return new Date(dateString).toLocaleDateString(undefined, options);
  };


  

  const otkaziNarudzbinu = (orderId) => {
    const confirmCancel = window.confirm('Da li ste sigurni da želite da otkažete porudžbinu?');
  
    if (confirmCancel) {
      axios
        .delete(`${process.env.REACT_APP_GET_ORDERS}/${orderId}`)
        .then((response) => {
          window.alert('Porudžbina uspešno otkazana');
          navigate('/home');
        })
        .catch((error) => {
          window.alert('Otkazivanje nije dozvoljeno, nije prošlo 1h od naručivanja. Pokušajte ponovo kasnije.');
        });
    }
  };
  const CenaSaDostavom = (cena) => cena + 400;

  return (
<div className="kontejner-tabela-artikala" style={{ color: 'silver' }}>
      <button className="dugme-nazad" style={{ width: '100%' }} onClick={Home}>
        Nazad
      </button>
      <table className="tabela-artikala">
        <thead>
          <tr>
            <th>ID</th>
            <th>Kolicina</th>
            <th>Ime artikla</th>
            <th>Kupac</th>
            <th>Prodavac</th>
            <th>Status</th>
            <th>Adresa</th>
            <th>Cena sa dostavom</th>
            <th>Komentar</th>
            <th>Datum porucivanja</th>
            <th>Datum stizanja</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {narudzbinas
            .map(narudzbina => (
            <tr key={narudzbina.id}>
              <td>{narudzbina.id}</td>
              <td>{narudzbina.kolicina}</td>
              <td>{narudzbina.imeArtikla}</td>
              <td>{narudzbina.imeKupca}</td>
              <td>{narudzbina.imeProdavca}</td>
              <td>{narudzbina.status}</td>
              <td>{narudzbina.adresa}</td>
              <td>{CenaSaDostavom(narudzbina.cena)}</td>
              <td>{narudzbina.komentar}</td>
              <td>{formatDate(narudzbina.datumPorucivanja)}</td>
              <td>{formatDate(narudzbina.datumStizanja)}</td>
              {user.tipKorisnika!==2 &&(
              <td>
                <button className="dugme-akcija" onClick={() => otkaziNarudzbinu(narudzbina.id)}>Cancel</button>
              </td>
              )}
            </tr>
          ))}
        </tbody>
      </table>
    </div>

  );
};

export default AktivneNarudzbineKorisnik;