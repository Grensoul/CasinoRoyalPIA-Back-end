using System.ComponentModel.DataAnnotations;

namespace Casino_Royal_PIA_Back_end.Entidades
{
    public class RifaParticipante
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El siguiente campo es obligatorio: {0}")]
        public int RifaId { get; set; }
        [Required(ErrorMessage = "El siguiente campo es obligatorio: {0}")]
        public int ParticipanteId { get; set; }
        public Rifa Rifa { get; set; }
        public Participante Participante { get; set; }
        [Required(ErrorMessage = "El siguiente campo es obligatorio: {0}")]
        [Range(1, 54)]
        public int NumLoteria  { get; set; }
        [Required(ErrorMessage = "El siguiente campo es obligatorio: {0}")]
        public int PremioId { get; set; }
    }
}
