using _060524_Taco_Fast_Food_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _060524_Taco_Fast_Food_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        FastFoodTacoDbContext dbContext = new FastFoodTacoDbContext();
        
        [HttpGet()]
        public IActionResult GetAll(string? SortByCost = null)
        {
            List<Drink> result = dbContext.Drinks.ToList();
            if (SortByCost.ToLower() == "ascending")
            {
                result = result.OrderBy(d => d.Cost).ToList();
            }
            if (SortByCost.ToLower() == "descending")
            {
                result = result.OrderByDescending(d => d.Cost).ToList();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Drink result = dbContext.Drinks.Find(id);
            if (result == null) { return NotFound("Drink not found"); }
            return Ok(result);
        }

        [HttpPost()]
        public IActionResult AddDrink([FromBody] Drink newDrink)
        {
            newDrink.Id = 0;
            dbContext.Drinks.Add(newDrink);
            dbContext.SaveChanges();
            return Created($"/api/Drinks/{newDrink.Id}", newDrink);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDrink([FromBody] Drink targetDrink, int id)
        {
            if (id != targetDrink.Id) { return BadRequest(); }
            if (!dbContext.Drinks.Any(d => d.Id == id)) { return NotFound(); }
            dbContext.Drinks.Update(targetDrink);
            dbContext.SaveChanges();
            return NoContent();
        }

    }
}
