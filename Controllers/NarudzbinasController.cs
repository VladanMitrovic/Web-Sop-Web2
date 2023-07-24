using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class NarudzbinasController : ControllerBase
    {
        private readonly INarudzbinaService _narudzbinaService;

        public NarudzbinasController(INarudzbinaService narudzbinaService)
        {
            _narudzbinaService = narudzbinaService;
        }

        // GET: api/Korisniks
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_narudzbinaService.GetNarudzbina());
        }

        // GET: api/Korisniks/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            return Ok(_narudzbinaService.GetById(id));
        }

        [HttpPut("{id}")]
        public IActionResult ChangeNarudzbina(long id, [FromBody] NarudzbinaDto narudzbina)
        {
            return Ok(_narudzbinaService.UpdateNarudzbinak(id, narudzbina));
        }


        // POST: api/Korisniks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult CreateNarudzbina([FromBody] NarudzbinaDto narudzbina, int id)
        {
            return Ok(_narudzbinaService.AddNarudzbina(narudzbina, id));
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
