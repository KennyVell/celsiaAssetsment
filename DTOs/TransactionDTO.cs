using System.ComponentModel.DataAnnotations;

namespace celsiaAssetsment.DTOs
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "The date and time of the transaction is required.")]
        public DateTime Date_Time { get; set; }

        [Required(ErrorMessage = "The transaction amount is required.")]
        public float Amount { get; set; }

        [Required(ErrorMessage = "The transaction status is required.")]
        public string? Status { get; set; }

        public string Type { get; set; } = "Pago de Factura";
        

        [Required(ErrorMessage = "The client id is required.")]
        public int ClientId { get; set; }


        [Required(ErrorMessage = "The platform id is required.")]
        public int PlatformId { get; set; }


        [Required(ErrorMessage = "Invoice id is required.")]
        public int InvoiceId { get; set; }
    }
}