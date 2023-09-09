import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router';


function Register(props) {
  const [korisnickoIme, setKorisnickoIme] = useState('');
  const [lozinka, setLozinka] = useState('');
  const [email, setEmail] = useState('');
  const [ime, setIme] = useState('');
  const [prezime, setPrezime] = useState('');
  const [adresa, setAdresa] = useState('');
  const [datumRodjenja, setDatumRodjenja] = useState('');
  const [tipKorisnika, setTipKorisnika] = useState('');
  const [slikaKorisnika, setSlikaKorisnika] = useState('');

  const navigate = useNavigate();

  const handleRegister = async (e) => {
    e.preventDefault();

    const korisnikDto = {
      Username: korisnickoIme,
      Password: lozinka,
      Email: email,
      Name: ime,
      Surname: prezime,
      Address: adresa,
      BirthDay: datumRodjenja,
      Role: parseInt(tipKorisnika),
      ProfilePicture: slikaKorisnika,
    };

    console.log('Korisnik DTO:', korisnikDto);

    try {
      axios
        .post(process.env.REACT_APP_USERS, korisnikDto)
        .then((response) => {
          console.log('Product successfully created:', response.data);
          console.log('response', response);
          navigate('/');
        })
        .catch(() => {
          window.alert('Username or email are already taken');
          navigate('/registration');
        });
    } catch {
      window.alert('Something went wrong');
      navigate('/registration');
    }
  };

  return (
    <form className="register-form" onSubmit={handleRegister}>
      <br />
      <br />
      <h3 style={{ color: 'white' }}>Register</h3>
      <br />
      <div className="form-group">
        <input
          type="text"
          className="form-control"
          placeholder="Username"
          value={korisnickoIme}
          onChange={(e) => setKorisnickoIme(e.target.value)}
        />
      </div>
      <div className="form-group">
        <input
          type="email"
          className="form-control"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
      </div>
      <div className="form-group">
        <input
          type="password"
          className="form-control"
          placeholder="Password"
          value={lozinka}
          onChange={(e) => setLozinka(e.target.value)}
        />
      </div>
      <div className="form-group">
        <input
          type="text"
          className="form-control"
          placeholder="Name"
          value={ime}
          onChange={(e) => setIme(e.target.value)}
        />
      </div>
      <div className="form-group">
        <input
          type="text"
          className="form-control"
          placeholder="Surname"
          value={prezime}
          onChange={(e) => setPrezime(e.target.value)}
        />
      </div>
      <div className="form-group">
        <input
          type="text"
          className="form-control"
          placeholder="Address"
          value={adresa}
          onChange={(e) => setAdresa(e.target.value)}
        />
      </div>
      <div className="form-group">
        <input
          type="date"
          className="form-control"
          placeholder="BirthDay"
          value={datumRodjenja}
          onChange={(e) => setDatumRodjenja(e.target.value)}
        />
      </div>
      <div className="form-group">
        <label className="role-label" style={{ color: 'white' }}>
          So...Who are you?
        </label>
        <select className="form-control" value={tipKorisnika} onChange={(e) => setTipKorisnika(e.target.value)}>
          <option value="">Select a role</option>
          <option value="1">User</option>
          <option value="2">Seller</option>
        </select>
      </div>
      <div className="form-group">
        <input
          type="text"
          className="form-control"
          placeholder="Profile Picture"
          value={slikaKorisnika}
          onChange={(e) => setSlikaKorisnika(e.target.value)}
        />
      </div>
      <button type="submit" className="btn btn-primary">
        Register
      </button>
      <div>
        <br />
        <button type="button" className="btn btn-secondary" onClick={() => navigate('/media')}>
          Register using social media
        </button>
      </div>
    </form>
  );
}

export default Register;
