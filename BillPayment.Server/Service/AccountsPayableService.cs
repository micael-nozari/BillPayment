using BillPayment.Server.Context;
using BillPayment.Server.Models.EntityModels;
using BillPayment.Server.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace BillPayment.Server.Service
{
    public class AccountsPayableService : IAccountsPayableService
    {
        private readonly BillPaymentContext _context;

        public AccountsPayableService(BillPaymentContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AccountPayable>> GetAccountsPayable()
        {
            return await _context.AccountsPayable.ToListAsync();
        }

        public async Task<IEnumerable<AccountPayable>> GetAccountsPayable(string? name)
        {
            if (string.IsNullOrEmpty(name))
                return await GetAccountsPayable();

            return await _context.AccountsPayable.Where(a => a.Name.Contains(name)).ToListAsync();
        }

        public async Task<IEnumerable<AccountPayable>> GetAccountsPayable(DateTime? paymentDate)
        {
            if (!paymentDate.HasValue)
                return await GetAccountsPayable();

            return await _context.AccountsPayable.Where(a => a.PaymentDate.Date == paymentDate.Value.Date).ToListAsync();
        }

        public async Task<IEnumerable<AccountPayable>> GetAccountsPayable(string name, DateTime? paymentDate)
        {
            if (!string.IsNullOrEmpty(name) && paymentDate.HasValue)
                return await _context.AccountsPayable
                    .Where(a => a.Name.Contains(name) && a.PaymentDate.Date == paymentDate.Value.Date).ToListAsync();
            else if (string.IsNullOrEmpty(name))
                return await GetAccountsPayable(paymentDate);
            else if (!paymentDate.HasValue)
                return await GetAccountsPayable(name);
            else
                return await GetAccountsPayable();
        }

        public async Task<AccountPayable> GetAccountPayable(int id)
        {
            return await _context.AccountsPayable.FindAsync(id);
        }

        public async Task<AccountPayable> CreateAccountPayable(AccountPayableViewModel viewModel)
        {
            var accountPayable = new AccountPayable()
            {
                Name = viewModel.Name,
                PaymentDate = viewModel.PaymentDate,
                DueDate = viewModel.DueDate,
                OriginalAmount = viewModel.OriginalAmount,                
            };

            CalculateTotalAmount(accountPayable);

            _context.AccountsPayable.Add(accountPayable);
            await _context.SaveChangesAsync();

            return accountPayable;
        }

        private void CalculateTotalAmount(AccountPayable accountPayable)
        {
            if (accountPayable.PaymentDate > accountPayable.DueDate)
            {
                accountPayable.LateDays = accountPayable.PaymentDate.Subtract(accountPayable.DueDate).Days;
                CalculateFine(accountPayable);
                CalculateInterest(accountPayable);
            }

            accountPayable.TotalAmount = accountPayable.OriginalAmount + accountPayable.FineAmount + accountPayable.InterestAmount;
        }

        private void CalculateFine(AccountPayable accountPayable)
        {
            int lateDays = accountPayable.LateDays;

            if (lateDays <= 3)
                accountPayable.FinePercentage = 2;            
            else if (lateDays <= 10)
                accountPayable.FinePercentage = 3;
            else
                accountPayable.FinePercentage = 5;

            accountPayable.FineAmount = accountPayable.OriginalAmount * (accountPayable.FinePercentage / 100);
        }

        private void CalculateInterest(AccountPayable accountPayable)
        {
            int lateDays = accountPayable.LateDays;

            if (lateDays <= 3)
                accountPayable.InterestPercentage = 0.1M;
            else if (lateDays <= 10)
                accountPayable.InterestPercentage = 0.2M;
            else
                accountPayable.InterestPercentage = 0.3M;

            accountPayable.InterestAmount = accountPayable.OriginalAmount * (accountPayable.InterestPercentage / 100) * lateDays;
        }

        public async Task<AccountPayable> UpdateAccountPayable(AccountPayableViewModel viewModel)
        {
            var accountPayable = await GetAccountPayable(viewModel.Id);
            if(accountPayable == null)
                return null;

            accountPayable.Name = viewModel.Name;
            accountPayable.PaymentDate = viewModel.PaymentDate;
            accountPayable.DueDate = viewModel.DueDate;
            accountPayable.OriginalAmount = viewModel.OriginalAmount;

            CalculateTotalAmount(accountPayable);

            _context.Entry(accountPayable).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return accountPayable;
        }

        public async Task<bool> DeleteAccountPayable(int id)
        {
            var accountPayable = await GetAccountPayable(id);
            if( accountPayable == null) 
                return false;

            _context.AccountsPayable.Remove(accountPayable);
            await _context.SaveChangesAsync();

            return true;
        }        
    }
}
