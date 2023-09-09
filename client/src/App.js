import { Route, Routes, BrowserRouter } from 'react-router-dom'; // Dodajte 'BrowserRouter'
import Login from './pages/Login';
import Register from './pages/Registracija';
import { useState, useEffect } from 'react';

import './App.css';

const App = () => {
  const [user, setUser] = useState(null);

  useEffect(() => {
    const storedUser = localStorage.getItem('user');
    if (storedUser) {
      setUser(JSON.parse(storedUser));
    }
  }, []);

  const handleLogin = (userData) => {
    setUser(userData);
    localStorage.setItem('user', JSON.stringify(userData));
    // navigate('/home'); // Pretpostavljam da želite da koristite rutu '/home' nakon logina, možete je dodati ovde
  };
  
  return (
    
      <Routes>
        <Route path="/" element={<Login onLogin={handleLogin}/>} />
        <Route path="/registration" element={<Register/>} />
      </Routes>
    
  );
}

export default App;
