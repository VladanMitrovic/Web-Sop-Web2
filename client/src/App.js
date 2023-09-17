import { Route, Routes, BrowserRouter } from 'react-router-dom'; // Dodajte 'BrowserRouter'
import Login from './pages/Login';
import Registracija from './pages/Registracija';
import { useState, useEffect } from 'react';
import IzmeniKorisnika from './pages/IzmeniKorisnika';
import { useNavigate } from 'react-router';
import SocialMediaRegistration from './pages/RegistracijaPrekoDrustvenihMreza';
import Home from './pages/Home';
import DodajArtikal from './pages/DodajArtikal';
import EditArticle from './pages/IzmeniArtikal';
import NarudzbineKorisnik from './pages/NarudzbineKorisnik';
import AdminNarudzbine from './pages/SvePorudzbineAdmin';
import ActiveOrdersUser from './pages/AktivneNarudzbineKorisnik';
import './App.css';

const App = () => {
  const [user, setUser] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    const storedUser = localStorage.getItem('user');
    if (storedUser) {
      setUser(JSON.parse(storedUser));
    }
  }, []);

  const handleLogin = (userData) => {
    setUser(userData);
    localStorage.setItem('user', JSON.stringify(userData));
     navigate('/home');
  };
  const handleUpdate = (updatedUser) => {
    // Call the logout method passed as a prop
    localStorage.setItem('user', JSON.stringify(updatedUser));
    navigate('/home');
  };
  const handleLogout = () => {
    setUser(null);
    localStorage.removeItem('user');
  };

  return (
    <Routes>
      <Route path="/" element={<Login onLogin={handleLogin} />} />
      <Route path="/registracija" element={<Registracija />} />
      <Route path="/izmeni" element={<IzmeniKorisnika user={user} onUpdate={handleUpdate} />} />
      <Route path="/media" element={<SocialMediaRegistration />} />
      <Route path="/home/" element={<Home user={user} onLogout={handleLogout}/>} />
      <Route path="/dodajArtikal" element={<DodajArtikal user={user}/>} />
      <Route path="/izmeniArtikal/:Id" element={<EditArticle user={user}/>}/>
      <Route path="/narudzbineKorisnik" element={<NarudzbineKorisnik user={user}/>} />
      <Route path="/adminNarudzbine" element={<AdminNarudzbine/>} />
      <Route path="/aktivneNarudzbineKorisnik" element={<ActiveOrdersUser user={user}/>} />
    </Routes>
  );
};

export default App;
