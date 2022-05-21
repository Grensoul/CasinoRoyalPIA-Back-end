using Casino_Royal_PIA_Back_end.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Casino_Royal_PIA_Back_end.Controllers
{
    [ApiController]
    [Route("api/participantes")]
    public class ParticipantesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<ParticipantesController> logger;

        public ParticipantesController(ApplicationDbContext context, ILogger<ParticipantesController> logger)
        {
            this.dbContext = context;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Participante>>> GetAll()
        {
            logger.LogInformation("Obteniendo lista de todos los participantes");
            return await dbContext.Participantes.ToListAsync();
        }

        //[Authorize]
        [HttpPost]
        public async Task<ActionResult> Post(Participante participante)
        {
            dbContext.Add(participante);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Se esta agregando un nuevo participante");
            return Ok();
        }

        //[Authorize]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            var existe = await dbContext.Participantes.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            dbContext.Remove(new Participante()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Se esta eliminando un participante");
            return Ok();
        }



    }
}
