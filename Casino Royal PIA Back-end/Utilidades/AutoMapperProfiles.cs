using AutoMapper;
using Casino_Royal_PIA_Back_end.DTOs;
using Casino_Royal_PIA_Back_end.Entidades;

namespace Casino_Royal_PIA_Back_end.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CreacionRifaDTO, Rifa>();
            CreateMap<Rifa, ObtenerRifaDTO>();
            CreateMap<ModificacionRifaDTO, Rifa>();
            //CreateMap<AgregarParticipanteARifaDTO, Participante>();
            CreateMap<RegistrarParticipanteDTO, Participante>()
                .ForMember(participante => participante.RifasParticipantes,
                opciones => opciones.MapFrom(MapRifasParticipantes));
        }

        private List<RifaParticipante> MapRifasParticipantes(RegistrarParticipanteDTO registrarParticipanteDTO,
            Participante participante)
        {
            var resultado = new List<RifaParticipante>();

            if (registrarParticipanteDTO == null) { return resultado; }

            foreach (var rifaId in registrarParticipanteDTO.RifasIds)
            {
                resultado.Add(new RifaParticipante() { RifaId = rifaId });
            }

            return resultado;
        }

    }
}
