using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web2projekat.Data;
using web2projekat.Dto;
using web2projekat.Interfaces;
using web2projekat.Models;
using web2projekat.ParametersForQuery;

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
        [HttpGet]
        public IActionResult GetAll([FromQuery] QueryZaArtikal query)
        {
            var artikli = _artikalService.GetArtikal(query);
            return Ok(artikli);
        }

        // GET: api/Korisniks/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_artikalService.GetById(id));
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
            
        }

        [HttpPut("{id}")]
        public IActionResult ChangeArtikal(int id,int idKorisnik, [FromBody] ArtikalDtoAdd artikal)
        {
            return Ok(_artikalService.UpdateArtikal(id,idKorisnik, artikal));
        }


        // POST: api/Korisniks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //[Authorize(Roles = "Prodavac")]
        public IActionResult CreateArtikal([FromBody] ArtikalDtoAdd artikal, int id)
        {
            /*return Ok(_artikalService.AddArtikal(artikal, id));*/
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           // int prodavacId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
            var noviArtikal = _artikalService.AddArtikal(artikal, id);
            return Ok(noviArtikal);

        }

        // DELETE: api/Korisniks/5
        [HttpDelete("{id}")]
        public ActionResult DeleteArtikal(int id, int korisnikId)
         {
             return Ok(_artikalService.DeleteArtikal(id, korisnikId));
         }

    }
}
