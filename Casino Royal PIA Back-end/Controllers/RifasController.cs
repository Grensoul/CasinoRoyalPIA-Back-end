using Casino_Royal_PIA_Back_end.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Casino_Royal_PIA_Back_end.Controllers
{
    [ApiController]
    [Route("api/rifas")]
    public class RifasController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public RifasController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Rifa>>> Get()
        {
            return await dbContext.Rifas.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Rifa rifa)
        {
            dbContext.Add(rifa);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(Rifa rifa, int id)
        {
            if (rifa.Id != id)
            {
                return BadRequest("El id de la rifa no coincide con el establecido en la url.");
            }

            dbContext.Update(rifa);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await dbContext.Rifas.AnyAsync(x => x.Id == id);
            if(!existe)
            {
                return NotFound();
            }

            dbContext.Remove(new Rifa()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
