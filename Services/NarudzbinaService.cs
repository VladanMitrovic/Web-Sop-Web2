using AutoMapper;
using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;
using web2projekat.Data;
using web2projekat.Dto;
using web2projekat.Interfaces;
using web2projekat.Izuzeci;
using web2projekat.Models;
using web2projekat.ParametersForQuery;

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
        public NarudzbinaDto AddNarudzbina(NarudzbinaDto newNarudzbina, int userId)
        {
            Narudzbina narudzbina = _mapper.Map<Narudzbina>(newNarudzbina);
            Korisnik kupac = _context.Korisnik.Find(userId);
            if (kupac == null)
            {
                throw new Exception("Korisnik sa ovim id-jem ne postoji");
            }
            else
            {
                narudzbina.Kupac = kupac;
            }
            Artikal artikal = _context.Artikal.Find(narudzbina.ArtikalId);
            if (artikal == null)
            {
                throw new Exception("Artikal sa ovim id-jem ne postoji");
            }
            narudzbina.PordavacId = artikal.ProdavacId;
            narudzbina.ImeProdavca = _context.Korisnik.Find(artikal.ProdavacId).Ime;
            artikal.Kolicina -= narudzbina.Kolicina;
            narudzbina.Status = StanjeArtikla.Rezervisano;
            narudzbina.Cena = artikal.Cena * narudzbina.Kolicina;
            TimeZoneInfo belgradeTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");

            narudzbina.DatumPorucivanja = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, belgradeTimeZone);
            Random rnd = new Random();
            narudzbina.DatumStizanja = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddHours(rnd.Next(1, 48)), belgradeTimeZone);
            try
            {
                _context.Add(narudzbina);
                _context.SaveChanges();

                return _mapper.Map<NarudzbinaDto>(narudzbina);
            }
            catch (Exception)
            {
                throw new Exception("Nije moguce porucivanje");
            }
            
        }

        public void DeleteNarudzbina(long id)
        {
            Narudzbina narudzbina = _context.Narudzbina.Find(id);
            if (narudzbina != null)
            {

                
                DateTime minimalnoVremeZaOtkazivanjeNarudzbine = narudzbina.DatumPorucivanja.AddHours(1);

                if (DateTime.UtcNow.ToLocalTime() >= minimalnoVremeZaOtkazivanjeNarudzbine)
                {
                    Artikal artikal = _context.Artikal.Find(narudzbina.ArtikalId);

                    if (artikal == null)
                    {
                        throw new Exception("Artikal sa ovim id-jem ne postoji!");
                    }

                    artikal.Kolicina += narudzbina.Kolicina;
                    _context.Narudzbina.Remove(narudzbina);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Ova narudzbina ne moze biti izbrisana.");
                }
            }
            
        }

        public NarudzbinaDto GetById(long id)
        {
            Narudzbina narudzbina = _context.Narudzbina.FirstOrDefault(n => n.Id == id);
            if(narudzbina == null)
            {
                throw new ActionExceptioncs("Ova narudzbina ne postoji!");
            }
            return _mapper.Map<NarudzbinaDto>(narudzbina);
        }

        public List<NarudzbinaDto> GetNarudzbina()
        {
            List<NarudzbinaDto> narudzbine = _mapper.Map<List<NarudzbinaDto>>(_context.Narudzbina.ToList());
            return narudzbine;
        }

        public NarudzbinaDto UpdateNarudzbinak(NarudzbinaDto newNarudzbina, int id)
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
