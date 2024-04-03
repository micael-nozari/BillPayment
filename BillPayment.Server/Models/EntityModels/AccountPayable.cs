using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillPayment.Server.Models.EntityModels
{
    public class AccountPayable
    {
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string Name { get; set; }
        
        [Required]
        public DateTime DueDate { get; set; }
        
        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal OriginalAmount { get; set; }

        public int LateDays { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal FinePercentage { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal FineAmount { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal InterestPercentage { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal InterestAmount { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal TotalAmount { get; set; }
    }
}
