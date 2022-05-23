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

            CreateMap<AgregarParticipanteARifaDTO, RifaParticipante>();

            CreateMap<RegistrarParticipanteDTO, Participante>();
                //.ForMember(participante => participante.RifasParticipantes,
                //opciones => opciones.MapFrom(MapRifasParticipantes));

            CreateMap<Premio, ObtenerPremiosDTO>();

            CreateMap<AgregarPremioDTO, Premio>();

            CreateMap<PremioPatchDTO, Premio>().ReverseMap();

            CreateMap<Participante, ObtenerParticipantesDTO>();

            //CreateMap<RifaParticipanteDTO, RifaParticipante>().ReverseMap();
        }

        //private List<RifaParticipante> MapRifasParticipantes(RegistrarParticipanteDTO registrarParticipanteDTO,
        //    Participante participante)
        //{
        //    var resultado = new List<RifaParticipante>();

        //    if (registrarParticipanteDTO == null) { return resultado; }

        //    foreach (var rifaId in registrarParticipanteDTO.RifasIds)
        //    {
        //        resultado.Add(new RifaParticipante() { RifaId = rifaId });
        //    }

        //    return resultado;
        //}

        //private List<ObtenerPremiosDTO> MapObtenerRifaDTOPremios(Rifa rifa, ObtenerRifaDTO obtenerRifaDTO)
        //{
        //    var resultado = new List<ObtenerPremiosDTO>();

        //    if (rifa.Premios == null) { return resultado; }

        //    foreach (var premio in rifa.Premios)
        //    {
        //        resultado.Add(new ObtenerPremiosDTO()
        //        {
        //            NombrePremio = premio.NombrePremio,
        //            DescripcionPremio = premio.DescripcionPremio
        //        });
        //    }

        //    return resultado;
        //}

    }
}
