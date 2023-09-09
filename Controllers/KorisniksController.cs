using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web2projekat.Data;
using web2projekat.Dto;
using web2projekat.Interfaces;
using web2projekat.Izuzeci;
using web2projekat.Models;

namespace web2projekat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KorisniksController : ControllerBase
    {
        private readonly IKorisnikService _korisnikService;

        public KorisniksController(IKorisnikService korisnikService)
        {
            _korisnikService = korisnikService; 
        }

        // GET: api/Korisniks
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_korisnikService.GetKorisnik());
        }

        // GET: api/Korisniks/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            /*return Ok(_korisnikService.GetById(id));*/
            KorisnikDto korisnik = _korisnikService.GetById(id);
            if(korisnik == null)
            {
                return NotFound("Korisnik nije pronadjen");
            }
            return Ok(korisnik);
        }

        [HttpPut("{id}")]
        public IActionResult ChangeKorisnik(int id, [FromBody] KorisnikUpdateDto korisnik)
        {
            try
            {
                return Ok(_korisnikService.UpdateKorisnik(id, korisnik));
            }
            catch(ActionExceptioncs e)
            {
                return NotFound(e.Message);
            }
            
        }


        // POST: api/Korisniks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult RegistrujKorisnika([FromBody] KorisnikRegistracija noviKorisnik)
        {
            if (!ModelState.IsValid)
            {
                // Ako ModelState nije validan, vratite odgovarajući odgovor
                return BadRequest(ModelState);
            }
            try
            {
                KorisnikDto korisnik;
                korisnik = _korisnikService.AddKorisnik(noviKorisnik);
                return Ok(korisnik);
            }
            catch(ActionExceptioncs e)
            {
                return Conflict(e.Message);
            }
            
        }
        [HttpPost("login")]
        public IActionResult UlogujKorisnika([FromBody] LoginZahtevDto loginZahtev)
        {
            try
            {
                var odgovorDto = _korisnikService.UlogujSe(loginZahtev);
                return Ok(odgovorDto);
            }
            catch(ActionExceptioncs e)
            {
                return Unauthorized(e.Message);
            }
            
        }
        [HttpPost("verify/{id}")]
        [Authorize(Roles = "Administrator")]

        public IActionResult VerifikujKorisnika(int id, [FromBody] VerifikacijaZahtevDto verifikacijaDto)
        {
            try
            {
                var korisnik = _korisnikService.ProveriKorisnika(id, verifikacijaDto);
                return Ok(korisnik);
            }
            catch(Exception e) when (e is Exception)
            {
                return e switch
                {
                    Exception => NotFound(e.Message),
                    //Exception => BadRequest(e.Message),
                    _=> StatusCode(500, "Greska prilikom verifikacije")
                };
            }
        }
    }
}
