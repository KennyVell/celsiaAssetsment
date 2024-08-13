using System.ComponentModel.DataAnnotations;

namespace celsiaAssetsment.Models
{
    public class Admin
    {
        public int? Id { get; set; }
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "The email is not in a valid format.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}