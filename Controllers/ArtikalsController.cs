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
    public class ArtikalsController : ControllerBase
    {
        private readonly IArtikalService _artikalService;

        public ArtikalsController(IArtikalService artikalService)
        {
            _artikalService = artikalService;
        }

        // GET: api/Korisniks
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_artikalService.GetArtikal());
        }

        // GET: api/Korisniks/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_artikalService.GetById(id));
        }

        [HttpPut("{id}")]
        public IActionResult ChangeArtikal(int id, [FromBody] ArtikalDto artikal)
        {
            return Ok(_artikalService.UpdateArtikal(id, artikal));
        }


        // POST: api/Korisniks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult CreateArtikal([FromBody] ArtikalDto artikal, int id)
        {
            return Ok(_artikalService.AddArtikal(artikal, id));
        }

        // DELETE: api/Korisniks/5
         /*[HttpDelete("{id}")]
         public ActionResult DeleteKorisnik(int id)
          {
              _artikalService.DeleteArtikal(id);
          }*/
          
    }
}
