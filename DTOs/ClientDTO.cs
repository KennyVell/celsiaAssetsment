using System.ComponentModel.DataAnnotations;

namespace celsiaAssetsment.DTOs
{
    public class ClientDTO
    {
        [Required(ErrorMessage = "The name is required.")]
        public string? Name { get; set; }

        public string? Address { get; set; }
        public string? IdentityNumber { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "The email is not in a valid format.")]
        public string? Email { get; set; }
    }
}