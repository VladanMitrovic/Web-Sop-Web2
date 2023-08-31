using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web2projekat.Data;
using web2projekat.Dto;
using web2projekat.Interfaces;
using web2projekat.Izuzeci;
using web2projekat.Models;
using web2projekat.ParametersForQuery;

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
        public IActionResult GetAll([FromQuery] QueryZaNarudzbinu query)
        {
            return Ok(_narudzbinaService.GetNarudzbina(query));
        }

        // GET: api/Korisniks/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                NarudzbinaDto narudzbina = _narudzbinaService.GetById(id);
                return Ok(narudzbina);
            }
            catch(ActionExceptioncs e)
            {
                return NotFound(e.Message);
            }
            
        }

        [HttpPut("{id}")]
        public IActionResult ChangeNarudzbina(long id, [FromBody] NarudzbinaDto narudzbina)
        {
            return Ok(_narudzbinaService.UpdateNarudzbinak(id, narudzbina));
        }


        // POST: api/Korisniks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Kupac")]
        public IActionResult CreateNarudzbina([FromBody] NarudzbinaAddDto narudzbina, int id)
        {
            try
            {
                //long userId = long.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                NarudzbinaDto narudzbinaAdd = _narudzbinaService.AddNarudzbina(narudzbina, id);
                return Ok(narudzbinaAdd);
            }
            catch (ActionExceptioncs e)
            {
                return NotFound(e.Message);
            }
            
        }

        // DELETE: api/Korisniks/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Kupac")]
        public IActionResult DeleteNarudzbina(long id, int korisnikId)
         {
            try
            {
                //long userId = long.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

                DeleteNarudzbinaDto responseDto = _narudzbinaService.DeleteNarudzbina(id, korisnikId);

                return Ok(responseDto);
            }
            catch (ActionExceptioncs e)
            {
                return NotFound(e.Message);
            }
            
            catch (Exception)
            {
                return Forbid();
            }
         }
          
    }
}
