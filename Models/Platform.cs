using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace celsiaAssetsment.Models
{  
    public class Platform
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The platform name is required.")]
        public string? Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Transaction>? Transactions { get; set; }
    }
}