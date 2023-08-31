﻿using AutoMapper;
using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using System.Data;
using web2projekat.Data;
using web2projekat.Dto;
using web2projekat.Interfaces;
using web2projekat.Izuzeci;
using web2projekat.Models;
using web2projekat.ParametersForQuery;

namespace web2projekat.Services
{
    public class ArtikalService : IArtikalService
    {
        private  readonly IMapper _mapper;
        private  readonly web2projekatContext _context;
        public ArtikalService(IMapper mapper1, web2projekatContext context)
        {
            _mapper= mapper1;
            _context = context;
        }
        public ArtikalDto AddArtikal(ArtikalDtoAdd newArtikal, int id)
        {
            Artikal artikal = _mapper.Map<Artikal>(newArtikal);
            try
            {
                artikal.ProdavacId = id;
                _context.Artikal.Add(artikal);
                _context.SaveChanges();  
            }
            catch (CannotInsertNullException)
            {
                throw new ActionExceptioncs("Sva polja moraju biti popunjena!");
            }
            catch (Exception ex) 
            {
                throw new Exception("Dogodila se greška prilikom dodavanja artikla.");
            }
            return _mapper.Map<ArtikalDto>(artikal);
        }

        public ArtikalDto DeleteArtikal(int id, int korisnikId)
        {
            
            Artikal artikal = _context.Artikal.FirstOrDefault(a => a.Id == id && a.ProdavacId == korisnikId);
            if (artikal == null || artikal.ProdavacId != korisnikId)
            {
                throw new ActionExceptioncs(artikal == null ? "Ovaj artikal ne postoji!" : "Prodavci mogu da azuriraju samo sopstvene proizvode!");
            }
            
            _context.Artikal.Remove(artikal);
            _context.SaveChanges();
            return _mapper.Map<ArtikalDto>(artikal);
            
        }

        public List<ArtikalDto> GetArtikal(QueryZaArtikal query)
        {
            List<Artikal> artikli = _context.Artikal.ToList();

            if (query.ProdavacId > 0)
            {
                artikli = artikli.Where(x => x.ProdavacId == query.ProdavacId).ToList();
            }

            if (artikli.Count == 0)
            {
                throw new ActionExceptioncs("Nema artikala koji ispunjavaju dati upit.");
            }

            return _mapper.Map<List<ArtikalDto>>(artikli);
        }

        public ArtikalDto GetById(int id)
        {
            try
            {
                ArtikalDto artikal = _mapper.Map<ArtikalDto>(_context.Artikal.Find(id));
                if(artikal == null)
                {
                    throw new ActionExceptioncs("Ovaj artikal ne postoji!");
                }
                return artikal;
            }
            catch(Exception ex)
            {
                throw;
            }
           
        }

        public ArtikalDto UpdateArtikal(int idArtikal,int idKorisnik, ArtikalDtoAdd newArtikal)
        {
            
            Artikal artikal = _context.Artikal.FirstOrDefault(a => a.Id == idArtikal && a.ProdavacId == idKorisnik);
            if (artikal == null || artikal.ProdavacId != idKorisnik)
            {
                throw new ActionExceptioncs(artikal == null ? "Ovaj artikal ne postoji!" : "Prodavci mogu da azuriraju samo sopstvene proizvode!"); 
            }
            
            artikal.Naziv = newArtikal.Naziv;
            artikal.Cena = newArtikal.Cena;
            artikal.Kolicina = newArtikal.Kolicina;
            artikal.Opis = newArtikal.Opis;
            artikal.Fotografija = newArtikal.Fotografija;
            try
            {
                _context.SaveChanges();
            }
            catch(CannotInsertNullException) 
            {
                throw new ActionExceptioncs("Sva polja moraju biti popunjena!");
            }
            return _mapper.Map<ArtikalDto>(artikal);
           
           
        }
    }
}
