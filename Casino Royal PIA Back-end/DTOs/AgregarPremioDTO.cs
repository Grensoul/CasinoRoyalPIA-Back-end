using System.ComponentModel.DataAnnotations;

namespace Casino_Royal_PIA_Back_end.DTOs
{
    public class AgregarPremioDTO
    {
        [Required(ErrorMessage = "El siguiente campo es obligatorio: {0}")]
        [StringLength(maximumLength: 75, ErrorMessage = "El campo {0} solo puede tener hasta 75 caracteres")]
        public string NombrePremio { get; set; }
        [Required(ErrorMessage = "El siguiente campo es obligatorio: {0}")]
        public int IdRifa { get; set; }
        [Required(ErrorMessage = "El siguiente campo es obligatorio: {0}")]
        [StringLength(maximumLength: 75, ErrorMessage = "El campo {0} solo puede tener hasta 75 caracteres")]
        public string DescripcionPremio { get; set; }
    }
}
