using System.ComponentModel.DataAnnotations;

namespace Casino_Royal_PIA_Back_end.Entidades
{
    public class Rifa
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El siguiente campo es obligatorio: {0}")]
        [StringLength(maximumLength: 75, ErrorMessage = "El campo {0} solo puede tener hasta 75 caracteres")]
        public string NombreRifa { get; set; }
        public List<RifaParticipante> RifaParticipante { get; set; }
        public List<Premio> Premios { get; set; }   
        public List<Tarjeta> Tarjetas { get; set; }
    }
}
