using System.ComponentModel.DataAnnotations;

namespace celsiaAssetsment.DTOs
{  
    public class PlatformDTO
    {
        [Required(ErrorMessage = "The platform name is required.")]
        public string? Name { get; set; }
    }
}