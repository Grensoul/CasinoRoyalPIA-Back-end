﻿using System.ComponentModel.DataAnnotations;

namespace Casino_Royal_PIA_Back_end.Validaciones
{
    public class PrimerLetraMayusAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var primerLetra = value.ToString()[0].ToString();

            if (primerLetra != primerLetra.ToUpper())
            {
                return new ValidationResult("La primer letra debe ser mayúscula");

            }
            return ValidationResult.Success;
        }
    }
}
