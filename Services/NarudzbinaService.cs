using AutoMapper;
using web2projekat.Data;
using web2projekat.Dto;
using web2projekat.Interfaces;
using web2projekat.Models;

namespace web2projekat.Services
{
    public class NarudzbinaService : INarudzbinaService
    {
        private readonly IMapper _mapper;
        private readonly web2projekatContext _context;
        public NarudzbinaService(IMapper mapper1,  web2projekatContext context)
        {
            _mapper = mapper1;
            _context = context; 
        }
        public NarudzbinaDto AddNarudzbina(NarudzbinaDto newNarudzbina, int id)
        {
            Narudzbina narudzbina = _mapper.Map<Narudzbina>(newNarudzbina);
            Korisnik kupac = _context.Korisnik.Find(id);
            narudzbina.Kupac = kupac;
            narudzbina.ArtikalId = kupac.Id;
            _context.Narudzbina.Add(narudzbina);
            _context.SaveChanges();
            return _mapper.Map<NarudzbinaDto>(newNarudzbina);
        }

        public void DeleteNarudzbina(long id)
        {
            Narudzbina narudzbina = _context.Narudzbina.Find(id);
            _context.Narudzbina.Remove(narudzbina);
            _context.SaveChanges();
        }

        public NarudzbinaDto GetById(long id)
        {
            return _mapper.Map<NarudzbinaDto>(_context.Narudzbina.Find(id));
        }

        public List<NarudzbinaDto> GetNarudzbina()
        {
            return _mapper.Map<List<NarudzbinaDto>>(_context.Narudzbina.ToList());
        }

        public NarudzbinaDto UpdateNarudzbinak(long id, NarudzbinaDto newNarudzbina)
        {
            Narudzbina narudzbina = _context.Narudzbina.Find(id);
            narudzbina.Status = newNarudzbina.Status;
            narudzbina.Adresa = newNarudzbina.Adresa;
            narudzbina.Kolicina = newNarudzbina.Kolicina;
            narudzbina.DatumPorucivanja = newNarudzbina.DatumPorucivanja;
            narudzbina.DatumStizanja = newNarudzbina.DatumStizanja;
            _context.SaveChanges();
            return _mapper.Map<NarudzbinaDto>(narudzbina);
        }
    }
}
