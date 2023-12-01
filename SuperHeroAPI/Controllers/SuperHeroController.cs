using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;
        public SuperHeroController(DataContext Context)
        {
            _context = Context;
        }
        public static List<SuperHero> heroes = new List<SuperHero>
        {
            new SuperHero
            {
                Id= 1,
                Name = "Spider Man",
                FirstName = "Peter",
                LastName = "Parker",
                Place = "New York City"
            },

            new SuperHero
            {
                Id= 2,
                Name = "Iron Man",
                FirstName = "Tony",
                LastName = "Stark",
                Place = "Long Island"
            }
        };

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetHeroes()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<SuperHero>>> GetHero(int id)
        {
            var hero = heroes.Find(hero => hero.Id == id);
            if (hero == null)
                return BadRequest("Hero Not Found");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> PostHero(SuperHero hero)
        {
            heroes.Add(hero);
            return Ok(heroes);
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero hero)
        {
            var newHero = heroes.Find(newHero => newHero.Id == hero.Id);
            if (newHero == null)
                return BadRequest("Hero Not Found");

            newHero.Name = hero.Name;
            newHero.FirstName = hero.FirstName;
            newHero.LastName = hero.LastName;
            newHero.Place = hero.Place;
            return Ok(heroes);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var hero = heroes.Find(hero => hero.Id == id);
            if(hero == null)
            {
                return BadRequest("Hero not found");
            }

            heroes.Remove(hero);
            return Ok(heroes);
        }
    }
}
