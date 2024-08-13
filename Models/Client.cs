using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace celsiaAssetsment.Models
{
    public class Client
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "The name of the client must be provided.")]
        public string? Name { get; set; }

        public string? Address { get; set; }
        public string? IdentityNumber { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "The email is not in a valid format.")]
        public string? Email { get; set; }

        [JsonIgnore]
        public virtual ICollection<Invoice>? Invoices { get; set; }

        [JsonIgnore]
        public virtual ICollection<Transaction>? Transactions { get; set; }
    }
}