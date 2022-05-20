using System.ComponentModel.DataAnnotations;

namespace Casino_Royal_PIA_Back_end.Entidades
{
    public class Participante
    {
        [Required]
        public int Id { get; set; }
        public string NombreParticipante { get; set; }
    }
}
