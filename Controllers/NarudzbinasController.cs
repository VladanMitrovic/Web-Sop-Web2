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
        [HttpGet]
        public IActionResult GetAll()
        {
            Console.WriteLine("GetAll method called."); 
            var narudzbine = _narudzbinaService.GetNarudzbina();
            Console.WriteLine($"Returned {narudzbine.Count} narudzbina.");
            return Ok(narudzbine);
            
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
            catch (ActionExceptioncs e)
            {
                return NotFound(e.Message);
            }

        }

        [HttpPut("{id}")]
        public IActionResult ChangeNarudzbina(int id, [FromBody] NarudzbinaDto narudzbina)
        {
            return Ok(_narudzbinaService.UpdateNarudzbinak(narudzbina, id));
        }


        // POST: api/Korisniks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        
        public IActionResult CreateNarudzbina(int userId, [FromBody] NarudzbinaDto narudzbina)
        {
            if (!ModelState.IsValid)
            {
                // Log validacione greške
                return BadRequest(ModelState);
            }
            try
            {
                
                NarudzbinaDto narudzbinaAdd = _narudzbinaService.AddNarudzbina(narudzbina, userId);
                return Ok(narudzbinaAdd);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }

        // DELETE: api/Korisniks/5
        [HttpDelete("{id}")]
        
        public IActionResult DeleteNarudzbina(long id)
         {
            try
            {
            

                _narudzbinaService.DeleteNarudzbina(id);
                return Ok();
            }
            catch (ActionExceptioncs e)
            {
                return BadRequest(e.Message);
            }
            
            catch (Exception)
            {
                return Forbid();
            }
         }
          
    }
}
