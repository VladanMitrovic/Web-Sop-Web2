import React, { useState } from 'react';
import axios from 'axios';  
import { FaSpinner } from 'react-icons/fa';

const Login = ({onLogin}) => {
    const [email, setEmail] = useState('');
    const [lozinka, setLozinka] = useState('');
    const [loading, setLoading] = useState(false);
  
    const Submit = async (e) => {
        e.preventDefault();
        setLoading(true);
        try {
            const odgovor = await axios.post(process.env.REACT_APP_LOGIN_USER, {
                Email: email,
                Password: lozinka
            });
            
            const rezultat = odgovor.data;        
            if (rezultat !== '') {
                localStorage.setItem('token', rezultat.data);
                console.log("Upao u trazenje emaila:");
                console.log(email);
                axios.get(process.env.REACT_APP_GET_USER_BY_EMAIL, {
                    params: {
                        email: email
                    }
                })
                .then((rezultat) => {
                    if (rezultat.data !== null) {
                        const podaciKorisnik = rezultat.data;
                        onLogin(podaciKorisnik);
                    }
                });
            }
        } catch (error) {
            window.alert("Pogresan email ili lozinka");
        } finally {
            setLoading(false); // Set loading state back to false after API call
        }
    };
  
    
    return(  
        <form onSubmit={Submit} className="forma-prijave">
    <h2>Prijavi se</h2>
    <div className="form-group">
      <label htmlFor="email">Email:</label>
      <input
        type="email"
        id="email"
        placeholder="Unesite svoj email"
        value={email}
        onChange={(e) => setEmail(e.target.value)}
        className="form-input"
      />
    </div>
    <div className="form-group">
      <label htmlFor="lozinka">Lozinka:</label>
      <input
        type="password"
        id="lozinka"
        placeholder="Unesite svoju lozinku"
        value={lozinka}
        onChange={(e) => setLozinka(e.target.value)}
        className="form-input"
      />
    </div>
    <button type="submit"  disabled={loading}>
      {loading ? <FaSpinner className="spinner" /> : 'Prijavi se'}
    </button>
    <a href="/registracija">Registruj se</a>
  </form>
    )  
};

export default Login;
