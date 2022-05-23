using Casino_Royal_PIA_Back_end.Entidades;
using System.ComponentModel.DataAnnotations;

namespace Casino_Royal_PIA_Back_end.DTOs
{
    public class RifaParticipanteDTO
    {
        [Required(ErrorMessage = "El siguiente campo es obligatorio: {0}")]
        public int RifaId { get; set; }
        public int NumLoteria { get; set; }
    }
}
