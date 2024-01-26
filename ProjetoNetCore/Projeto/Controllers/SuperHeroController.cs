using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto.Controllers.Entities;
using Projeto.Data;

namespace Projeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class SuperHeroController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllSuperHeroes()
        {
            var heroes = await _context.SuperHeroes.ToListAsync();

            return Ok(heroes);
        } 

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetSuperHeroById(int id)
        {
            var heroe = await _context.SuperHeroes.FindAsync(id);
            if( heroe is null)
            {
                return NotFound("Hero not Found.");
            }
            return Ok(heroe);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero([FromBody] SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<SuperHero>> UpdateHero([FromBody] SuperHero updatedHero)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(updatedHero.Id);
            if (dbHero is null)
            {
                return NotFound("Hero not Found.");
            }
            dbHero.Name = updatedHero.Name;
            dbHero.FirstName = updatedHero.FirstName;
            dbHero.LastName = updatedHero.LastName;
            dbHero.Place = updatedHero.Place;

            await _context.SaveChangesAsync();

            return Ok(dbHero);
        }

        [HttpDelete]
        public async Task<ActionResult<SuperHero>> DeleteHero(int Id)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(Id);
            if (dbHero is null)
            {
                return NotFound("Hero not Found.");
            }

            _context.SuperHeroes.Remove(dbHero);
            await _context.SaveChangesAsync();

            return Ok("Hero is deleted.");
        }
    }
}

/*
 using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Projeto.Controllers.Entities;
using Projeto.Data;

namespace Projeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> getAllSuperHeroes()
        {
            var heroes = new List<SuperHero>
            {
                new SuperHero
                {
                    Id = 1,
                    Name = "mATHEUS",
                    FirstName = "Dor",
                    LastName = "ue",
                    Place = "Sao paulo"
                }
            };

            return Ok(heroes);
        }
    }
}

 */