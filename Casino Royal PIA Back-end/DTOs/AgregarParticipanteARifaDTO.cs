using System.ComponentModel.DataAnnotations;

namespace Casino_Royal_PIA_Back_end.DTOs
{
    public class AgregarParticipanteARifaDTO
    {
        [Required(ErrorMessage = "El siguiente campo es obligatorio: {0}")]
        public int RifaId { get; set; }
        [Required(ErrorMessage = "El siguiente campo es obligatorio: {0}")]
        public int ParticipanteId { get; set; }
        [Required(ErrorMessage = "El siguiente campo es obligatorio: {0}")]
        [Range(1, 54)]
        public int NumLoteria { get; set; }
    }
}
