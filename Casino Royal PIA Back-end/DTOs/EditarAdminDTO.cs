using System.ComponentModel.DataAnnotations;

namespace Casino_Royal_PIA_Back_end.DTOs
{
    public class EditarAdminDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
