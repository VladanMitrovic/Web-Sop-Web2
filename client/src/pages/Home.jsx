import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router';
import axios from 'axios';  
import Artikal from './Artikal';
import { Route, Routes } from 'react-router';
import Admin from './Admin';
import Prodavac from './Prodavac';


const Home = ({ user, onLogout }) => {
    
    const [korisnici, setKorisnici] = useState([]);
    const [artikli, setArtikli] = useState([]);
    const [narudzbina, setNarudzbina] = useState(false);
    const [verifikacija, setVerifikacija] = useState(false);
   
    const navigate = useNavigate();

   
    
    useEffect(() => {
      let cancelRequest = axios.CancelToken.source();
    
      // Fetch user data from the server
      axios.get(process.env.REACT_APP_USERS, {
        cancelToken: cancelRequest.token
      })
        .then(response => {
          // Update the users state with the fetched data
          setKorisnici(response.data);
        })
        .catch(error => {
          if (axios.isCancel(error)) {
            // Ako je zahtev otkazan, ne radimo ništa
          } else {
            console.error('Error fetching users:', error);
          }
        });
    
      // Otkazujemo zahtev ako se komponenta odmontira
      return () => {
        cancelRequest.cancel();
      };
    }, []);

      useEffect(() => {
        let cancelRequest = axios.CancelToken.source();
      
        // Fetch article data from the server
        axios.get(process.env.REACT_APP_ARTICLES, {
          cancelToken: cancelRequest.token
        })
          .then(response => {
            // Update the articles state with the fetched data
            setArtikli(response.data);
          })
          .catch(error => {
            if (axios.isCancel(error)) {
              // Ako je zahtev otkazan, ne radimo ništa
            } else {
              console.error('Error fetching articles:', error);
            }
          });
      
        // Otkazujemo zahtev ako se komponenta odmontira
        return () => {
          cancelRequest.cancel();
        };
      }, []);

      

      const renderRoutes = () => {
        if (user.tipKorisnika === 0) {
          return (
            <>
              <Route path="/" element={<Admin verifikacija={verifikacija} users={korisnici} />} />
            </>
          );
        } else if (user.tipKorisnika === 1) {
          return (
            <>
              <Route path="/" element={<Artikal user={user} newOrder={narudzbina} articles={artikli} />} />
            </>
          );
        } else if (user.tipKorisnika === 2) {
          console.log("Korisnik verifikacija", user.verifikacijaStatus);
          if (user.verifikacijaStatus === 0) {
            return (
              <>
                <Route path="/" element={<Prodavac user={user} articles={artikli} />} />
              </>
            );
          } else {
            return (
              <>
                <Route
                  path="/"
                  element={
                    <h1>
                      Vaš nalog mora prvo da bude verifikovan od strane admina da biste ga mogli koristiti.
                    </h1>
                  }
                />
              </>
            );
          }
        }
      };


    const renderDashboardItems = () => {
      
    const handleNarudzbina = () =>{
      setNarudzbina(!narudzbina);
      navigate('/home');
    }

    const handleLogout = () => {
     
      onLogout();
      localStorage.removeItem('user');
      navigate('/');

    };

    const handleDodajArtikal = () =>{
      navigate('/dodajArtikal');
    }

    const handleVerifikacija = () =>{
      setVerifikacija(!verifikacija);
      navigate('/home');
    }
    

    const handleIzmeniProfil = () =>{
      navigate('/izmeni');
    };

    const handlePorudzbineAdmin = () =>{
      navigate('/adminNarudzbine');
    }

    const handlePrethodnePorudzbine = () =>{
      navigate('/narudzbineKorisnik');
    }

    const handleAktivneNarudzbine = () =>{
      navigate('/aktivneNarudzbineKorisnik');
    }

    let items = [];

    // Common item visible for all roles
    items.push(<div key="profile" onClick={handleIzmeniProfil}>Profil</div>);
    items.push(<div key="log-out" onClick={handleLogout}>Izloguj se</div>);

    // Role-specific items
    if (user.tipKorisnika === 0) { 
      items.push(<div key="sve-porudzbine" onClick={handlePorudzbineAdmin}>Sve porudzbine</div>);
      items.push(<div key="verifikacija" onClick={handleVerifikacija}>Verifikacija</div>);
      
    } else if (user.tipKorisnika === 1) { 
      items.push(<div key="aktivne-porudzbina" onClick={handleAktivneNarudzbine}>Aktivne porudzbine</div>);
      items.push(<div key="nova-porudzbina" onClick={handleNarudzbina}>Nova porudzbina</div>);
      items.push(<div key="prethodne-porudzbine" onClick={handlePrethodnePorudzbine}>Prethodne porudzbine</div>);
    } else if (user.tipKorisnika === 2) { 
      items.push(<div key="dodavanje-artikla" onClick={handleDodajArtikal}>Dodavanje artikla</div>);
      items.push(<div key="moje-porudzbine" onClick={handlePrethodnePorudzbine}>Moje porudzbine</div>);
      items.push(<div key="nove-porudzbine" onClick={handleAktivneNarudzbine}>Nove porudzbine</div>);
    }
    return items;
  };
  
  if (!user) {
      navigate('/');
      return null;
    }

  return (
  <div style={{color:'silver'}}>

      <div className="pocetna-strana-kontejner">
          <h2 className="naslov-panela" style={{color:'white'}}>Dobrodosao na web shop, {user.korisnickoIme}!</h2>
          <div className="panel-stavke-kontejner">{renderDashboardItems()}</div>       
      </div>

      <Routes>
        {renderRoutes()}
      </Routes>
  </div>
  );
  };
  export default Home;