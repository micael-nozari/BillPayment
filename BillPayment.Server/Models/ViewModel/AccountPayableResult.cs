using BillPayment.Server.Models.EntityModels;

namespace BillPayment.Server.Models.ViewModel
{
    public class AccountPayableResult
    {
        public string Name { get; set; }
        public decimal OrigialAmount { get; set; }
        public decimal CorrectedAmount { get; set; }
        public int LateDays { get; set; }
        public DateTime PaymentDate { get; set; }

        public AccountPayableResult(AccountPayable accountPayable)
        {
            Name = accountPayable.Name;
            OrigialAmount = accountPayable.OriginalAmount;
            CorrectedAmount = accountPayable.TotalAmount;
            LateDays = accountPayable.LateDays;
            PaymentDate = accountPayable.PaymentDate;
        }
    }
}
