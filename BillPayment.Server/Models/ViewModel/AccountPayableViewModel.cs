using System.ComponentModel.DataAnnotations;

namespace BillPayment.Server.Models.ViewModel
{
    public class AccountPayableViewModel
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
        [Required]
        public decimal OriginalAmount { get; set; }
    }
}
