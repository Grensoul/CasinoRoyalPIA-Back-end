using Casino_Royal_PIA_Back_end.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Casino_Royal_PIA_Back_end.Controllers
{
    [ApiController]
    [Route("api/premios")]
    public class PremiosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public PremiosController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Premio>>> GetAll()
        {
            return await dbContext.Premios.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Premio premio)
        {
            dbContext.Add(premio);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("Ganador")]
        public async Task<ActionResult<List<Premio>>> GetWinner()
        {
            return Ok();
        }


    }
}
