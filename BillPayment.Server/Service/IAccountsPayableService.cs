using BillPayment.Server.Models.EntityModels;
using BillPayment.Server.Models.ViewModel;

namespace BillPayment.Server.Service
{
    public interface IAccountsPayableService
    {
        Task<IEnumerable<AccountPayable>> GetAccountsPayable();
        Task<IEnumerable<AccountPayable>> GetAccountsPayable(string? name);
        Task<IEnumerable<AccountPayable>> GetAccountsPayable(DateTime? paymentDate);
        Task<IEnumerable<AccountPayable>> GetAccountsPayable(string name, DateTime? paymentDate);
        Task<AccountPayable> GetAccountPayable(int id);
        Task<AccountPayable> CreateAccountPayable (AccountPayableViewModel accountPayable);
        Task<AccountPayable> UpdateAccountPayable (AccountPayableViewModel accountPayable);
        Task<bool> DeleteAccountPayable (int id);
    }
}
