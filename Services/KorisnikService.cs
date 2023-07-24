using AutoMapper;
using Microsoft.EntityFrameworkCore;
using web2projekat.Data;
using web2projekat.Dto;
using web2projekat.Interfaces;
using web2projekat.Models;

namespace web2projekat.Services
{
    public class KorisnikService : IKorisnikService
    {
        private readonly IMapper _mapper;
        private readonly web2projekatContext _context;
        public KorisnikService(IMapper mapper, web2projekatContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public KorisnikDto AddKorisnik(KorisnikDto newKorisnik)
        {
            Korisnik korisnik = _mapper.Map<Korisnik>(newKorisnik);
            _context.Korisnik.Add(korisnik);
            _context.SaveChanges();
            return _mapper.Map<KorisnikDto>(newKorisnik);
        }

        public void DeleteKorisnik(int id)
        {
            Korisnik korisnik = _context.Korisnik.Find(id);
            _context.Korisnik.Remove(korisnik);
            _context.SaveChanges();
        }

        public KorisnikDto GetById(int id)
        {
            return _mapper.Map<KorisnikDto>(_context.Korisnik.Find(id));
        }

        public List<KorisnikDto> GetKorisnik()
        {
            return _mapper.Map<List<KorisnikDto>>(_context.Korisnik.ToList());
        }

        public KorisnikDto UpdateKorisnik(int id, KorisnikDto newKorisnik)
        {
            Korisnik korisnik = _context.Korisnik.Find(id);
            korisnik.KorisnickoIme = newKorisnik.KorisnickoIme;
            korisnik.Email = newKorisnik.Email;
            korisnik.Lozinka = newKorisnik.Lozinka;
            korisnik.Ime = newKorisnik.Ime;
            korisnik.Prezime = newKorisnik.Prezime;
            korisnik.DatumRodjenja = newKorisnik.DatumRodjenja;
            korisnik.Adresa = newKorisnik.Adresa;
            korisnik.TipKorisnika = newKorisnik.TipKorisnika;
            korisnik.SlikaKorisnika = newKorisnik.SlikaKorisnika;
            _context.SaveChanges();
            return _mapper.Map<KorisnikDto>(korisnik);
            
        }
    }
}
