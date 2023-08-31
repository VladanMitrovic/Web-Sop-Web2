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
        public NarudzbinaDto AddNarudzbina(NarudzbinaAddDto newNarudzbina, int korisnikId)
        {
            Artikal artikal = _context.Artikal.FirstOrDefault(x => x.Id == newNarudzbina.ArtikalId);
            if (artikal == null || artikal.Kolicina < newNarudzbina.Kolicina)
            {
                throw new ActionExceptioncs(artikal == null ? "Ovaj artikal ne postoji!" : "Nema dovoljno artikala!");
            }
            /*if (article == null)
            {
                throw new ResourceNotFoundException("Article with specified id doesn't exist!");
            }

            if (article.Quantity < requestDto.Quantity)
            {
                throw new InvalidFieldsException("There are not enough articles in stock!");
            }*/

            artikal.Kolicina -= newNarudzbina.Kolicina;
            Narudzbina narudzbina = new Narudzbina
            {
                KupacId =  korisnikId,
                ArtikalId = artikal.Id,
                Status = StanjeArtikla.Isporuka,
                //Cena = artikal.Cena * newNarudzbina.Kolicina,
                DatumPorucivanja = DateTime.UtcNow,
                Adresa = newNarudzbina.Adresa
                
                //DatumStizanja = new Random().Next(1, 25)

            };
            _context.Narudzbina.Add(narudzbina);
            try
            {
                _context.SaveChanges();
            }
            catch(CannotInsertNullException)
            {
                throw new ActionExceptioncs("Narudzbina sa ovim Id-jem vec postoji");
            }
            catch (Exception)
            {
                throw;
            }
            return _mapper.Map<NarudzbinaDto>(narudzbina);
        }

        public DeleteNarudzbinaDto DeleteNarudzbina(long id, int korisnikId)
        {
            var narudzbina = _context.Narudzbina
                                     .Include(n => n.Artikal)
                                     .FirstOrDefault(n => n.Id == id);
            if (narudzbina == null || narudzbina.KupacId != korisnikId)
            {
                throw new ActionExceptioncs(narudzbina == null ? "Ova narudzbina ne postoji!" : "Prodavci mogu da otkazu samo sopstvene narudzbine!");
            }
            /*if (order == null)
            {
                throw new ResourceNotFoundException("Order with specified id doesn't exist!");
            }

            if (order.BuyerId != userId)
            {
                throw new ForbiddenActionException("Buyers can only cancel their own orders!");
            }*/
            var vremeOdKreiranja = (DateTime.UtcNow - narudzbina.DatumPorucivanja).TotalHours;
            if(vremeOdKreiranja > 1)
            {
                throw new ActionExceptioncs("Narudzbine mogu biti otkazane tokom prvog sata!");
            }
            narudzbina.Artikal.Kolicina += narudzbina.Kolicina;
            _context.Narudzbina.Remove(narudzbina);
            _context.SaveChanges();

            return _mapper.Map<DeleteNarudzbinaDto>(narudzbina);
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

        public List<NarudzbinaDto> GetNarudzbina(QueryZaNarudzbinu queryParametri)
        {
            IQueryable<Narudzbina> query = _context.Narudzbina;
            if(queryParametri.IdKupca>0)
            {
                query = query.Where(x => x.Kupac.Id == queryParametri.IdKupca);
            }
            else if(queryParametri.IdProdavca > 0)
            {
                query = query.Where(x => x.Artikal.ProdavacId == queryParametri.IdProdavca);
            }
            List<Narudzbina> narudzbine = query.ToList();
            return _mapper.Map<List<NarudzbinaDto>>(narudzbine);
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
