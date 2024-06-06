using _060524_Taco_Fast_Food_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _060524_Taco_Fast_Food_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TacosController : ControllerBase
    {
        FastFoodTacoDbContext dbContext = new FastFoodTacoDbContext();

        [HttpGet()]

        public IActionResult GetAll(bool SoftShell)
        {
            List<Taco> result = dbContext.Tacos.ToList();
            if (SoftShell == true)
            {
                result = result.Where(t => t.SoftShell == true).ToList();
            }
            if(SoftShell == false)
            {
                result = result.Where(t => t.SoftShell == false).ToList();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Taco result = dbContext.Tacos.Find(id);
            if (result == null) { return NotFound("Taco not found"); }
            return Ok(result);
        }

        [HttpPost("{id}")]
        public IActionResult AddTaco([FromBody] Taco newTaco)
        {
            newTaco.Id = 0;
            dbContext.Tacos.Add(newTaco);
            dbContext.SaveChanges();
            return Created($"/api/Tacos/{newTaco.Id}", newTaco);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTaco(int id)
        {
            Taco t = dbContext.Tacos.Find(id);
            if (t == null) { return NotFound(); }
            dbContext.Tacos.Remove(t);
            dbContext.SaveChanges();
            return NoContent();
        }
    }
}
