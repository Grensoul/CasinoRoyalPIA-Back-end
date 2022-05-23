using AutoMapper;
using Casino_Royal_PIA_Back_end.DTOs;
using Casino_Royal_PIA_Back_end.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Casino_Royal_PIA_Back_end.Controllers
{
    [ApiController]
    [Route("api/rifas")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RifasController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public RifasController(ApplicationDbContext context, IMapper mapper)
        {
            this.dbContext = context;
            this.mapper = mapper;
        }

        [HttpGet("ObtenerRifas")]
        [AllowAnonymous]
        public async Task<ActionResult<List<ObtenerRifaDTO>>> GetAll()
        {
            var rifas = await dbContext.Rifas
                .Include(rifasDb => rifasDb.Premios)
                .ToListAsync();
            if (rifas.Count == 0)
            {
                return NotFound("No hay ninguna rifa en el sistema");
            }

            return mapper.Map<List<ObtenerRifaDTO>>(rifas);
        }

        [HttpGet("ObtenerTarjetasDisponiblesPorIdDeRifa")]
        [AllowAnonymous]
        public async Task<ActionResult<ObtenerRifaDTO>> GetById(int id)
        {
            var rifa = await dbContext.Rifas.FirstOrDefaultAsync(x => x.Id == id);
            if (rifa == null)
            {
                return BadRequest("La rifa no existe.");
            }

            return mapper.Map<ObtenerRifaDTO>(rifa);
        }

        [HttpGet("ObtenerTarjetasDisponiblesPorNombreDeRifa")]
        [AllowAnonymous]
        public async Task<ActionResult<ObtenerRifaDTO>> GetByName(int id)
        {
            var rifa = await dbContext.Rifas.FirstOrDefaultAsync(x => x.Id == id);
            if (rifa == null)
            {
                return BadRequest("La rifa no existe.");
            }

            return mapper.Map<ObtenerRifaDTO>(rifa);
        }

        [HttpPost("NuevaRifa")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> Post(CreacionRifaDTO creacionRifaDTO)
        {
            var existeRifaConMismoNombre = await dbContext.Rifas.AnyAsync(x => x.NombreRifa == creacionRifaDTO.NombreRifa);

            if (existeRifaConMismoNombre)
            {
                return BadRequest($"Ya existe una rifa con el nombre {creacionRifaDTO.NombreRifa}");
            }

            var rifa = mapper.Map<Rifa>(creacionRifaDTO);

            dbContext.Add(rifa);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("ModificarRifaPorID")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> Put(ModificacionRifaDTO modificacionRifaDTO, int id)
        {
            if (modificacionRifaDTO.Id != id)
            {
                return BadRequest("El id de la rifa no coincide con el establecido en la url.");
            }

            var rifa = mapper.Map<Rifa>(modificacionRifaDTO);

            dbContext.Update(rifa);
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("EliminarRifaPorID")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> DeleteById(int id)
        {
            var existe = await dbContext.Rifas.AnyAsync(x => x.Id == id);
            if(!existe)
            {
                return NotFound("No se encontró la rifa que buscaba");
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
