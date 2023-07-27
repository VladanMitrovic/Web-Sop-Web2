using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using web2projekat.Data;
using web2projekat.Dto;
using web2projekat.Interfaces;
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
            artikal.ProdavacId = id;
            _context.Artikal.Add(artikal);
            _context.SaveChanges();
            
            return _mapper.Map<ArtikalDto>(artikal);

        }

        public ArtikalDto DeleteArtikal(int id, int korisnikId)
        {
            Artikal artikal = _context.Artikal.FirstOrDefault(a => a.Id == id && a.ProdavacId == korisnikId);
            _context.Artikal.Remove(artikal);
            _context.SaveChanges();
            return _mapper.Map<ArtikalDto>(artikal);
        }

        public List<ArtikalDto> GetArtikal(QueryZaArtikal query)
        {
            /* return _mapper.Map < List < ArtikalDto >> (_context.Artikal.ToList());*/
            List<Artikal> artikli = _context.Artikal.ToList();
            if(query.ProdavacId > 0)
            {
                artikli = artikli.Where(x => x.ProdavacId == query.ProdavacId).ToList();
            }
            return _mapper.Map<List<ArtikalDto>>(artikli); 
        }

        public ArtikalDto GetById(int id)
        {
            ArtikalDto artikal = _mapper.Map<ArtikalDto>(_context.Artikal.Find(id));
            return artikal; 
        }

        public ArtikalDto UpdateArtikal(int idArtikal,int idKorisnik, ArtikalDtoAdd newArtikal)
        {
            Artikal artikal = _context.Artikal.FirstOrDefault(a => a.Id== idArtikal && a.ProdavacId == idKorisnik );
            artikal.Naziv = newArtikal.Naziv;
            artikal.Cena = newArtikal.Cena;
            artikal.Kolicina = newArtikal.Kolicina;
            artikal.Opis = newArtikal.Opis;
            artikal.Fotografija = newArtikal.Fotografija;
            //artikal.Fotografija = newArtikal.Fotografija;

            _context.SaveChanges();

            return _mapper.Map<ArtikalDto>(artikal);
        }
    }
}
