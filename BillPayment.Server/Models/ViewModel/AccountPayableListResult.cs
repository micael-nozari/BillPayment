using BillPayment.Server.Models.EntityModels;

namespace BillPayment.Server.Models.ViewModel
{
    public class AccountPayableListResult
    {
        public List<AccountPayableResult> AccountsPayable { get; set; }

        public AccountPayableListResult(List<AccountPayable> accountsPayable)
        {
            AccountsPayable = accountsPayable.Select(ap => new AccountPayableResult(ap)).ToList();
        }
    }
}
