using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using web2projekat.Data;
using web2projekat.Dto;
using web2projekat.Interfaces;
using web2projekat.Models;

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
        public ArtikalDto AddArtikal(ArtikalDto newArtikal, int id)
        {
            Artikal artikal = _mapper.Map<Artikal>(newArtikal);
            Korisnik prodavac = _context.Korisnik.Find(id);
            artikal.Prodavac = prodavac;
            artikal.ProdavacId = prodavac.Id;
            _context.Artikal.Add(artikal);
            _context.SaveChanges();
            
            return _mapper.Map<ArtikalDto>(newArtikal);

        }

        public void DeleteArtikal(int id)
        {
            Artikal artikal = _context.Artikal.Find(id);
            _context.Artikal.Remove(artikal);
            _context.SaveChanges();
        }

        public List<ArtikalDto> GetArtikal()
        {
            return _mapper.Map < List < ArtikalDto >> (_context.Artikal.ToList());
        }

        public ArtikalDto GetById(int id)
        {
            return _mapper.Map<ArtikalDto>(_context.Artikal.Find(id));
        }

        public ArtikalDto UpdateArtikal(int id, ArtikalDto newArtikal)
        {
            Artikal artikal = _context.Artikal.Find(id);
            artikal.Naziv = newArtikal.Naziv;
            artikal.Cena = newArtikal.Cena;
            artikal.Kolicina = newArtikal.Kolicina;
            artikal.Opis = newArtikal.Opis;
            //artikal.Fotografija = newArtikal.Fotografija;

            _context.SaveChanges();

            return _mapper.Map<ArtikalDto>(artikal);
        }
    }
}
