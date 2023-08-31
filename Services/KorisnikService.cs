using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using web2projekat.Data;
using web2projekat.Dto;
using web2projekat.Interfaces;
using web2projekat.Models;
using System.Text;
using System.Security.Claims;
using System.Linq;
using System.Collections.Generic;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using web2projekat.Izuzeci;
using System.Security.Authentication;
using EntityFramework.Exceptions.Common;

namespace web2projekat.Services
{
    public class KorisnikService : IKorisnikService
    {
        private readonly IConfigurationSection _token;
        private readonly IMapper _mapper;
        private readonly web2projekatContext _context;
        public KorisnikService(IConfiguration config, IMapper mapper, web2projekatContext context)
        {
            _token = config.GetSection("SecretKey");
            _mapper = mapper;
            _context = context;
        }
        

        public KorisnikDto AddKorisnik(KorisnikRegistracija registracija)
        {
            Korisnik korisnik = _mapper.Map<Korisnik>(registracija);
            try
            {
                korisnik.Lozinka = BCrypt.Net.BCrypt.HashPassword(korisnik.Lozinka, BCrypt.Net.BCrypt.GenerateSalt());
                _context.Korisnik.Add(korisnik);
                _context.SaveChanges();
                return _mapper.Map<KorisnikDto>(korisnik);
            }
            catch (UniqueConstraintException)
            {
                throw new ActionExceptioncs("Ovaj korisnik vec postoji");
            }
            catch(CannotInsertNullException)
            {
                throw new ActionExceptioncs("Sva polja moraju biti popunjena");
            }
           
        }

        public void DeleteKorisnik(int id)
        {
            Korisnik korisnik = _context.Korisnik.Find(id);
            _context.Korisnik.Remove(korisnik);
            _context.SaveChanges();
        }

        public KorisnikDto GetById(int id)
        {
            KorisnikDto korisnik = _mapper.Map<KorisnikDto>(_context.Korisnik.Find(id));
            if(korisnik == null)
            {
               throw new ActionExceptioncs("Korisnik sa ovim id ne postoji");
            }
            return korisnik;
        }

        public List<KorisnikDto> GetKorisnik()
        {
            return _mapper.Map<List<KorisnikDto>>(_context.Korisnik.ToList());
        }

        public KorisnikDto UpdateKorisnik(int id, KorisnikUpdateDto newKorisnik)
        {
            Korisnik korisnik = _context.Korisnik.Find(id);
            if(korisnik == null)
            {
                throw new ActionExceptioncs("Ovaj korisnik ne postoji");
            }
            korisnik.KorisnickoIme = newKorisnik.KorisnickoIme;
            korisnik.Email = newKorisnik.Email;
            korisnik.Ime = newKorisnik.Ime;
            korisnik.Prezime = newKorisnik.Prezime;
            korisnik.DatumRodjenja = newKorisnik.DatumRodjenja;
            korisnik.Adresa = newKorisnik.Adresa;
            try
            {
                _context.SaveChanges();
            }
            catch(UniqueConstraintException)
            {
                throw new ActionExceptioncs("Ovakav korisnik ne postoji!");
            }
            catch (CannotInsertNullException)
            {
                throw new ActionExceptioncs("Sva polja moraju biti popunjena!");
            }


            return _mapper.Map<KorisnikDto>(korisnik);
            
        }

        public LoginOdgovorDto UlogujSe(LoginZahtevDto zahtevDto)
        {
            Korisnik korisnik = _context.Korisnik.FirstOrDefault(u => u.Email == zahtevDto.Email);
            if (korisnik == null || !BCrypt.Net.BCrypt.Verify(zahtevDto.Password, korisnik.Lozinka))
            {
                throw new ActionExceptioncs("Incorrect login credentials!");
            }
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Id", korisnik.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, korisnik.TipKorisnika.ToString()));
           /* if(korisnik.TipKorisnika == TipKorisnika.Prodavac)
            {

            }*/
            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_token.Value));

            // Kreira potpisne kredencijale za potpisivanje JWT tokena
            SigningCredentials signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            // Kreira JWT token sa odgovarajućim parametrima
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: "http://localhost:44319",
                claims: claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: signingCredentials
            );

            // Kreira odgovor koji sadrži Id korisnika i generisani JWT token
            LoginOdgovorDto responseDto = new LoginOdgovorDto()
            {
                Id = korisnik.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken)
            };

            return responseDto;
        }

        public VerifikacijaOdgovorDto ProveriKorisnika(int id, VerifikacijaZahtevDto verifikacijaZahtev)
        {
            Korisnik korisnik = _context.Korisnik.Find(id);
            if (korisnik == null)
                throw new Exception("Korisnik sa ovim id-em ne postoji");

            if (korisnik.TipKorisnika != TipKorisnika.Prodavac)
                throw new Exception("Samo prodavci mogu biti verifikovani");

            _mapper.Map(verifikacijaZahtev, korisnik);
            _context.SaveChanges();
            return _mapper.Map<VerifikacijaOdgovorDto>(korisnik);
        }
    }
}
