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
using System.Net.Mail;

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
            korisnik.Lozinka = BCrypt.Net.BCrypt.HashPassword(korisnik.Lozinka, BCrypt.Net.BCrypt.GenerateSalt());

            Korisnik korisnikEmail = _context.Korisnik.FirstOrDefault(u=> u.Email == registracija.Email);
            if(korisnikEmail != null) {

                throw new Exception("Korisnik sa ovim email vec postoji!");
            }

            Korisnik korisnickoIme = _context.Korisnik.FirstOrDefault(u => u.KorisnickoIme == registracija.KorisnickoIme);
            if(korisnickoIme != null)
            {
                throw new Exception("Korisnik sa ovim korisnickim imenom vec postojiu!");
            }
            try
            {
                MailAddress userMail = new MailAddress(korisnik.Email);
            }
            catch (FormatException)
            {

                throw new Exception("Nepostojeci email");
            }
            _context.Add(korisnik);
            _context.SaveChanges();
            if(registracija.TipKorisnika == TipKorisnika.Prodavac)
            {
                EmailSender mail = new EmailSender();
                mail.SendVerificationEmail(registracija.Email, "Vas nalog je kreiran admin ce potvrditi vasu verifikaciju." +
                                                         "Povratak na aplikaciju: http://localhost:3000/home");
            }
            return _mapper.Map<KorisnikDto>(korisnik);

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

        public string UlogujSe(LoginZahtevDto zahtevDto)
        {
            Korisnik korisnik = _context.Korisnik.FirstOrDefault(u => u.Email == zahtevDto.Email);
            if (korisnik == null || !BCrypt.Net.BCrypt.Verify(zahtevDto.Password, korisnik.Lozinka))
            {
                throw new ActionExceptioncs("Incorrect login credentials!");
            }
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Id", korisnik.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, korisnik.TipKorisnika.ToString()));
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


            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public KorisnikDto ProveriKorisnika(VerifikacijaOdgovorDto verifikacija)
        {
            Korisnik korisnik = _context.Korisnik.Find(verifikacija.Id);
            if (korisnik == null)
                throw new Exception("Korisnik sa ovim id-em ne postoji");

            if (korisnik.TipKorisnika != TipKorisnika.Prodavac)
                throw new Exception("Samo prodavci mogu biti verifikovani");

            korisnik.VerifikacijaStatus = verifikacija.VerifikacijaStatus;
            _context.SaveChanges();
            return _mapper.Map<KorisnikDto>(korisnik);
        }

        public KorisnikDto EmailKorisnik(string email)
        {
            Korisnik korisnik = _context.Korisnik.FirstOrDefault(x => x.Email == email);
            return _mapper.Map<KorisnikDto>(korisnik);
        }
    }
}
