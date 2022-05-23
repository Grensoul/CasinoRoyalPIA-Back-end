using Casino_Royal_PIA_Back_end.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Casino_Royal_PIA_Back_end.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.JsonPatch;

namespace Casino_Royal_PIA_Back_end.Controllers
{
    [ApiController]
    [Route("api/premios")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
    public class PremiosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public PremiosController(ApplicationDbContext context, IMapper mapper)
        {
            this.dbContext = context;
            this.mapper = mapper;
        }

        [HttpGet("ObtenerPremios")]
        [AllowAnonymous]
        public async Task<ActionResult<List<ObtenerPremiosDTO>>> GetAll()
        {
            var premios = await dbContext.Premios.ToListAsync();

            if (premios.Count == 0)
            {
                return NotFound("No hay ningún premio en el sistema");
            }

            return mapper.Map<List<ObtenerPremiosDTO>>(premios);
        }

        [HttpPost("AgregarPremio")]
        public async Task<ActionResult> Post(AgregarPremioDTO agregarPremioDTO)
        {
            var idRifa = agregarPremioDTO.IdRifa;

            if (agregarPremioDTO == null) { return BadRequest("No hay ningún premio que agregar"); }

            var rifaBd = await dbContext.Rifas.Include(rifa => rifa.Premios)
                .FirstOrDefaultAsync(x => x.Id == idRifa);

            if (rifaBd == null) { return NotFound("No existe la rifa"); }

            var premio = mapper.Map<Premio>(agregarPremioDTO);

            premio.Rifa = rifaBd;

            dbContext.Add(premio);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<PremioPatchDTO> patchDocument)
        {
            if (patchDocument == null) { return BadRequest(); }

            var premioDb = await dbContext.Premios.FirstOrDefaultAsync(x => x.Id == id);

            if (premioDb == null) { return NotFound("No se encontró el id el premio"); }

            var premioDTO = mapper.Map<PremioPatchDTO>(premioDb);

            patchDocument.ApplyTo(premioDTO, ModelState);

            var esValido = TryValidateModel(premioDTO);

            if (!esValido) { return BadRequest(ModelState); }

            mapper.Map(premioDTO, premioDb);

            await dbContext.SaveChangesAsync();

            return NoContent();

        }

        [HttpGet("Ganador")]
        public async Task<ActionResult<List<Premio>>> GetWinner(int id)
        {
            //Random random = new Random();
            //var winner = random.Next();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> DeleteById(int id)
        {
            var existe = await dbContext.Premios.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound("No se encontró el premio que buscaba");
            }

            dbContext.Remove(new Premio()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }


    }
}
