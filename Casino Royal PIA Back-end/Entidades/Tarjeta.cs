using Casino_Royal_PIA_Back_end.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace Casino_Royal_PIA_Back_end.Entidades
{
    public class Tarjeta
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El siguiente campo es obligatorio: {0}")]
        [StringLength(maximumLength: 30, ErrorMessage = "El campo {0} solo puede tener hasta 30 caracteres")]
        public string NombreTarjeta { get; set; }

    }
}
