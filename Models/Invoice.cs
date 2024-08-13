using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace celsiaAssetsment.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The invoice number is required.")]
        public string? Number { get; set; }

        [Required(ErrorMessage = "The invoice period is required.")]
        public string? Period { get; set; }

        [Required(ErrorMessage = "The invoiced amount is required.")]
        public float Billed_Amount { get; set; }

        [Required(ErrorMessage = "The amount paid is required.")]
        public float Paid_Amount { get; set; }

        [Required(ErrorMessage = "The client id is required.")]
        public int ClientId { get; set; }
        public virtual Client? Client { get; set; }

        [JsonIgnore]
        public virtual ICollection<Transaction>? Transactions { get; set; }
    }
}