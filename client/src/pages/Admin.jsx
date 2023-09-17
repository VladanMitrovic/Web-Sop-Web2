import React from 'react';
import axios from 'axios';

const VerifikacijaStatus = (verifikacijaStatus) => {
  switch (verifikacijaStatus) {
    case 0:
      return 'Prihvacen';
    case 1:
      return 'Odbijen';
    case 2:
      return 'Neresen';
    default:
      return '';
  }
};

const tipKorisnika = (tipKorisnika) => {
  switch (tipKorisnika) {
    case 0:
      return 'Administrator';
    case 1:
      return 'Korisnik';
    case 2:
      return 'Prodavac';
    default:
      return '';
  }
};



const Datum = (datum) => {
  const options = { year: 'numeric', month: 'long', day: 'numeric' };
  const date = new Date(datum);
  return date.toLocaleDateString(undefined, options);
};

const Admin = ({ verifikacija, users }) => {
  const Verifikuj = (userId, status) => {
    const data = {
      Id: userId,
      VerifikacijaStatus: status,
    };

    try {
      const odgovor =  axios.post(process.env.REACT_APP_USER_VERIFY, data);
      console.log('Verifikacija je uspesna:', odgovor.data);
      
      window.location.reload();
    } catch (error) {
      console.error('Greska prilikom verifikacije:', error);
      
    }
  };

  const AdminTabela = () => {
    return (
      <div className="kontejner-tabele-korisnika">
        <table className="tabela-korisnika">
          <thead>
            <tr>
              <th>Korisnicko Ime</th>
              <th>Email</th>
              <th>Prezime</th>
              <th>Adresa</th>
              <th>Datum Rodjenja</th>
              <th>Uloga</th>
              <th>Verifikacioni status</th>
              <th>Verifikuj</th>
            </tr>
          </thead>
          <tbody>
            {users
              .filter(user => user.tipKorisnika !== 0 && user.tipKorisnika !==1) // Filter users with role 2
              .filter(user => !verifikacija || user.verifikacijaStatus === 2)
              .map(user => (
                <tr key={user.id}>
                  <td>{user.korisnickoIme}</td>
                  <td>{user.email}</td>
                  <td>{user.prezime}</td>
                  <td>{user.adresa}</td>
                  <td>{Datum(user.datumRodjenja)}</td>
                  <td>{tipKorisnika(user.tipKorisnika)}</td>
                  <td>{VerifikacijaStatus(user.verifikacijaStatus)}</td>
                  <td>
                    <button className="dugme-verifikacije-prihvati" style={{ width: '100%' }} onClick={() => Verifikuj(user.id, 0)}>Prihvati</button>
                    <br />
                    <button className="dugme-verifikacije-odbij" style={{ width: '100%' }} onClick={() => Verifikuj(user.id, 1)}>Odbi</button>
                    <br />
                  </td>
                </tr>
              ))}
          </tbody>
        </table>
      </div>
    );
  };

  return (
    <div>
      {AdminTabela()}
    </div>
  );
};

export default Admin;
