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
    [Route("api/participantes")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ParticipantesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<ParticipantesController> logger;
        private readonly IMapper mapper;

        public ParticipantesController(ApplicationDbContext context, ILogger<ParticipantesController> logger,
            IMapper mapper)
        {
            this.dbContext = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ObtenerParticipantesDTO>>> GetAll()
        {
            var participantes = await dbContext.Participantes
                //.Include(participantesDb => participantesDb.Premios)
                .ToListAsync();
            if (participantes.Count == 0)
            {
                return NotFound("No hay ningún participante en el sistema");
            }

            return mapper.Map<List<ObtenerParticipantesDTO>>(participantes);
        }

        [HttpPost("RegistrarParticipante")]
        public async Task<ActionResult> Post(RegistrarParticipanteDTO registrarParticipanteDTO)
        {
            var existeParticipante = await dbContext.Participantes.AnyAsync(x =>
            x.NombreParticipante == registrarParticipanteDTO.NombreParticipante);

            if (existeParticipante)
            {
                return BadRequest($"Ya existe un participante con el nombre " +
                    $"{registrarParticipanteDTO.NombreParticipante}");
            }

            //if (registrarParticipanteDTO == null)
            //{
            //    return BadRequest("No se puede registrar un participante sin rifas seleccionadas");
            //}

            //var rifasIds = await dbContext.Rifas.Where(rifaBD =>
            //registrarParticipanteDTO.RifasIds.Contains(rifaBD.Id))
            //    .Select(x => x.Id).ToListAsync();

            //if (registrarParticipanteDTO.RifasIds.Count != rifasIds.Count)
            //{
            //    return BadRequest("No existe alguna de las rifas enviadas");
            //}

            var participante = mapper.Map<Participante>(registrarParticipanteDTO);

            dbContext.Add(participante);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Se esta agregando un nuevo participante");
            return Ok();
        }


        //[HttpPost("AgregarParticipanteARifa")]
        //public async Task<ActionResult> Post(AgregarParticipanteARifaDTO agregarParticipanteARifaDTO)
        //{
        //    var existeParticipanteConMismoId = await dbContext.RifaParticipantes.AnyAsync(x => 
        //    x.Id == agregarParticipanteARifaDTO.ParticipanteId);

        //    if (existeParticipanteConMismoId)
        //    {
        //        return BadRequest($"Ya existe un participante en la rifa con el id " +
        //            $"{agregarParticipanteARifaDTO.ParticipanteId}");
        //    }

        //    var numeroDeTarjetaOcupado = await dbContext.RifaParticipantes.AnyAsync(x =>
        //    x.NumLoteria == agregarParticipanteARifaDTO.NumLoteria);

        //    if (numeroDeTarjetaOcupado)
        //    {
        //        return BadRequest($"Ya existe un participante en la rifa con el numero de tarjeta " +
        //            $"{agregarParticipanteARifaDTO.NumLoteria}");
        //    }

        //    var participante = mapper.Map<Participante>(agregarParticipanteARifaDTO);

        //    dbContext.Add(participante);
        //    await dbContext.SaveChangesAsync();
        //    logger.LogInformation("Se esta agregando un nuevo participante");
        //    return Ok();
        //}

        
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
