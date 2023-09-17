import React from 'react';
import { useState } from 'react'; 
import Narucivanje from './Narucivanje';

const Artikal = ({ user,newOrder,articles }) => {
  const [quantities, setQuantities] = useState({});

  const promenaKolicine = (event, artikalId) => {
    const { value } = event.target;
    setQuantities(prethodnaKolicina => ({
      ...prethodnaKolicina,
      [artikalId]: parseInt(value),
    }));
  };

    const ArtikalTabela = () => {
      return (
        <div className="kontejner-tabele-artikala">
        <table className="tabela-artikala">
          <thead>
            <tr>
              <th>Naziv</th>
              <th>Cena(din)</th>
              <th>Kolicina</th>
              <th>Opis</th>
              {newOrder && user.tipKorisnika === 1 && <th>Order</th>}
            </tr>
          </thead>
          <tbody>
            {articles.map(artikal => (
              <tr key={artikal.id}>
                <td>{artikal.naziv}</td>
                <td>{artikal.cena}</td>
                <td>{artikal.kolicina}</td>
                <td>{artikal.opis}</td>
                {newOrder && user.tipKorisnika === 1 && (
                    <td>
                    <div className="order-container">
                      <input
                        type="number"
                        min="0"
                        max={artikal.kolicina}
                        value={quantities[artikal.id] || 0}
                        onChange={(event) => promenaKolicine(event, artikal.id)}
                      />
                      
                      {quantities[artikal.id] > 0 && (
                        <Narucivanje
                          article={artikal}
                          quantity={quantities[artikal.id]}
                          user={user}
                        />
                      )}
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
  
    return (
      <div>
        {ArtikalTabela()}
      </div>
    );
  };

  export default Artikal;