using System.ComponentModel.DataAnnotations;

namespace Casino_Royal_PIA_Back_end.Entidades
{
    public class RifaParticipante : IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El siguiente campo es obligatorio: {0}")]
        public int RifaId { get; set; }
        [Required(ErrorMessage = "El siguiente campo es obligatorio: {0}")]
        public int ParticipanteId { get; set; }
        public Participante Participante { get; set; }
        [Required(ErrorMessage = "El siguiente campo es obligatorio: {0}")]
        public int NumLoteria { get; set; }
        public Rifa Rifa { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NumLoteria < 1 || NumLoteria > 54)
            {
                yield return new ValidationResult("El número de lotería fuera de rango", new string[]
                {
                    nameof(NumLoteria)
                });
            }
            throw new NotImplementedException();
        }
    }
}
