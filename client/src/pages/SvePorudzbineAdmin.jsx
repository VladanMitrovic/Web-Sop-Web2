import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router';

const AdminNarudzbine = () => {
  const [orders, setOrders] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  const handleHome = () => {
    navigate('/home');
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get(process.env.REACT_APP_GET_ORDERS);
        setOrders(response.data);
        setLoading(false);
      } catch (error) {
        console.error('Greška prilikom dobavljanja narudzbina:', error);
        setError('Greška prilikom dobavljanja narudzbina. Pokušajte kasnije.');
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  const Datum = (datum) => {
    const options = { year: 'numeric', month: 'long', day: 'numeric', hour: 'numeric', minute: 'numeric' };
    return new Date(datum).toLocaleDateString(undefined, options);
  };

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>Error: {error}</div>;
  }
  const CenaSaDostavom = (cena) => cena + 400;
  return (
    <div className="kontejner-tabele-artikala" style={{ color: 'silver' }}>
      <button
        className="submit-button"
        style={{ width: '100%' }}
        onClick={handleHome}
      >
        Nazad
      </button>
      <table className="tabela-artikala">
        <thead>
          <tr>
            <th>ID</th>
            <th>Ime artikla</th>
            <th>Kupac</th>
            <th>Status</th>
            <th>Adresa</th>
            <th>Cena</th>
            <th>Komentar</th>
            <th>Kolicina</th>
            <th>Datum porucivanja</th>
            <th>Datum brisanja</th>
          </tr>
        </thead>
        <tbody>
          {orders.map(narudzbina => (
            <tr key={narudzbina.id}>
              <td>{narudzbina.id}</td>
              <td>{narudzbina.imeArtikla}</td>
              <td>{narudzbina.imeKupca}</td>
              <td>{narudzbina.status}</td>
              <td>{narudzbina.adresa}</td>
              <td>{CenaSaDostavom(narudzbina.cena)}</td>
              <td>{narudzbina.komentar}</td>
              <td>{narudzbina.kolicina}</td>
              <td>{Datum(narudzbina.datumPorucivanja)}</td>
              <td>{Datum(narudzbina.datumStizanja)}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default AdminNarudzbine;
