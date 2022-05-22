using Casino_Royal_PIA_Back_end.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Casino_Royal_PIA_Back_end.Controllers
{
    [ApiController]
    [Route("api/tarjetas")]
    public class TarjetasController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public TarjetasController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet("ObtenerIdYNombreDeTarjetas")]
        public async Task<ActionResult<List<Tarjeta>>> GetAll()
        {
            return await dbContext.Tarjetas.ToListAsync();
        }

        //[HttpGet("ObtenerTarjetasDisponiblesEnRifaPorId")]
        //public async Task<ActionResult>
    }
}
