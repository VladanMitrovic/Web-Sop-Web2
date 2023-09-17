import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router';

const Prodavac = ({ user, articles }) => {
  const navigate = useNavigate();
  const [listaArtikala, setListaArtikala] = useState(articles);

  const ArtikalObrisi = (id) => {
    const Obrisi = window.confirm('Da li ste sigurni da želite da obrišete ovaj artikal?');
  
    if (!Obrisi) {
      return;
    }
  
    axios
      .delete(`${process.env.REACT_APP_ARTICLES}/${id}`)
      .then((odgovor) => {
        console.log(odgovor.data);
        setListaArtikala((prevArticles) =>
          prevArticles.filter((article) => article.id !== id)
        );
      })
      .catch((error) => {
        console.error('Greška prilikom brisanja artikla:', error);
      });
  };

  const ArtikalIzmeni = (id) => {
    navigate(`/izmeniArtikal/${id}`);
  };

  const ArtikalTabela = () => {
    return (
      <div className="kontejner-tabele-artikala">
        <table className="tabela-artikala ">
          <thead>
            <tr>
              <th>Ime artikla</th>
              <th>Cena(din)</th>
              <th>Kolicina</th>
              <th>Opis</th>
              {user.tipKorisnika === 2 && <th>Opcije</th>}
            </tr>
          </thead>
          <tbody>
            {listaArtikala
              .filter((artikal) => artikal.prodavacId === user.id)
              .map((artikal) => (
                <tr key={artikal.id}>
                  <td>{artikal.naziv}</td>
                  <td>{artikal.cena}</td>
                  <td>{artikal.kolicina}</td>
                  <td>{artikal.opis}</td>
                  {user.tipKorisnika === 2 && (
                    <td>
                      <div className="akcije-container">
                        <button
                          className="dugme-promeni"
                          onClick={() => ArtikalIzmeni(artikal.id)}
                        >
                          Izmeni
                        </button>
                        <br />
                        <button
                          className="dugme-obrisi"
                          style={{ backgroundColor: 'red' }}
                          onClick={() => ArtikalObrisi(artikal.id)}
                        >
                          Obrisi
                        </button>
                      </div>
                    </td>
                  )}
                </tr>
              ))}
          </tbody>
        </table>
      </div>
    );
  };

  useEffect(() => {
    setListaArtikala(articles);
  }, [articles]);

  return <div>{ArtikalTabela()}</div>;
};

export default Prodavac;
