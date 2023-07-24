﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web2projekat.Data;
using web2projekat.Dto;
using web2projekat.Interfaces;
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
            return Ok(_korisnikService.GetById(id));
        }

        [HttpPut("{id}")]
        public IActionResult ChangeKorisnik(int id, [FromBody] KorisnikDto korisnik)
        {
            return Ok(_korisnikService.UpdateKorisnik(id, korisnik));
        }


        // POST: api/Korisniks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult CreateKorisnik([FromBody] KorisnikDto korisnik)
        {
            return Ok(_korisnikService.AddKorisnik(korisnik));
        }

        // DELETE: api/Korisniks/5
      /*  [HttpDelete("{id}")]
       public ActionResult DeleteKorisnik(int id)
        {
            _korisnikService.DeleteKorisnik(id);
        }
        */
    }
}