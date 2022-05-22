using Casino_Royal_PIA_Back_end.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace Casino_Royal_PIA_Back_end.DTOs
{
    public class RegistrarParticipanteDTO
    {
        [Required(ErrorMessage = "El siguiente campo es obligatorio: {0}")]
        [StringLength(maximumLength: 75, ErrorMessage = "El campo {0} solo puede tener hasta 75 caracteres")]
        [PrimerLetraMayus]
        public string NombreParticipante { get; set; }
        [Required(ErrorMessage = "El siguiente campo es obligatorio: {0}")]
        [Range(18, 99)]
        public int EdadParticipante { get; set; }

        public List<int> RifasIds { get; set; }
    }
}
