using BillPayment.Server.Context;
using BillPayment.Server.Models.EntityModels;
using BillPayment.Server.Models.ViewModel;
using BillPayment.Server.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;


namespace BillPayment.Test
{
    public class UnitTests
    {
        private readonly HttpClient _httpClient = new() { BaseAddress = new Uri("https://localhost:7062") };

        private BillPaymentContext _context;
        private AccountsPayableService _service;

        public UnitTests()
        {
            var contextOptions = new DbContextOptionsBuilder<BillPaymentContext>()
                    .UseInMemoryDatabase("TestDatabase").Options;

            _context = new BillPaymentContext(contextOptions);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _service = new AccountsPayableService(_context);
        }

        [Fact]
        public void ShowldCreateAccountPayable()
        {
            var name = "name";
            var dueDate = DateTime.Today.AddDays(3);
            var paymentDate = DateTime.Today;
            var originalAmount = 100;
            var totalAmount = 100;

            var accountPayable = new AccountPayable()
            {
                Name = name,
                DueDate = dueDate,
                PaymentDate = paymentDate,
                OriginalAmount = originalAmount,
                TotalAmount = totalAmount
            };

            Assert.NotNull(accountPayable);
            Assert.Equal(name, accountPayable.Name);
            Assert.Equal(dueDate, accountPayable.DueDate);
            Assert.Equal(paymentDate, accountPayable.PaymentDate);
            Assert.Equal(originalAmount, accountPayable.OriginalAmount);
            Assert.Equal(totalAmount, accountPayable.TotalAmount);
        }

        public static TheoryData<string, DateTime, DateTime, decimal, decimal> AccountsPayable =
            new()
            {
                { "title 1", DateTime.Today, DateTime.Today, 100, 100 },
                { "title 2", DateTime.Today.AddDays(-1), DateTime.Today, 100, 102.1M },
                { "title 3", DateTime.Today.AddDays(-2), DateTime.Today, 100, 102.2M },
                { "title 4", DateTime.Today.AddDays(-3), DateTime.Today, 100, 102.3M },
                { "title 5", DateTime.Today.AddDays(-4), DateTime.Today, 100, 103.8M },
                { "title 6", DateTime.Today.AddDays(-5), DateTime.Today, 100, 104M },
                { "title 7", DateTime.Today.AddDays(-6), DateTime.Today, 100, 104.2M },
                { "title 8", DateTime.Today.AddDays(-7), DateTime.Today, 100, 104.4M },
                { "title 9", DateTime.Today.AddDays(-8), DateTime.Today, 100, 104.6M },
                { "title 10", DateTime.Today.AddDays(-9), DateTime.Today, 100, 104.8M },
                { "title 11", DateTime.Today.AddDays(-10), DateTime.Today, 100, 105M },
                { "title 12", DateTime.Today.AddDays(-11), DateTime.Today, 100, 108.3M },
                { "title 13", DateTime.Today.AddDays(-12), DateTime.Today, 100, 108.6M },
                { "title 14", DateTime.Today.AddDays(-13), DateTime.Today, 100, 108.9M },
                { "title 15", DateTime.Today.AddDays(-14), DateTime.Today, 100, 109.2M },
            };

        [Theory, MemberData(nameof(AccountsPayable))]
        public async Task ShowldSaveCreateAccountPayable(string name, DateTime dueDate, DateTime paymentDate, decimal originalAmount, decimal expectedTotalAmount)
        {
            var viewModel = new AccountPayableViewModel()
            {
                Name = name,
                DueDate = dueDate,
                PaymentDate = paymentDate,
                OriginalAmount = originalAmount,
            };

            var accountPayable = await _service.CreateAccountPayable(viewModel);

            Assert.NotNull(accountPayable);
            Assert.True(accountPayable.Id > 0);
            Assert.Equal(viewModel.Name, accountPayable.Name);
            Assert.Equal(viewModel.DueDate, accountPayable.DueDate);
            Assert.Equal(viewModel.PaymentDate, accountPayable.PaymentDate);
            Assert.Equal(viewModel.OriginalAmount, accountPayable.OriginalAmount);
            Assert.Equal(expectedTotalAmount, accountPayable.TotalAmount);
        }

        private async Task AddDummyAccountsPayable()
        {
            var accountPayable1 = new AccountPayable()
            {
                Name = "Internet",
                DueDate = new DateTime(2024, 4, 10),
                PaymentDate = new DateTime(2024, 4, 8),
                OriginalAmount = 100,
                TotalAmount = 100,
            };
            var accountPayable2 = new AccountPayable()
            {
                Name = "Gym",
                DueDate = new DateTime(2024, 4, 10),
                PaymentDate = new DateTime(2024, 4, 9),
                OriginalAmount = 100,
                TotalAmount = 100,
            };

            _context.AccountsPayable.AddRange(accountPayable1, accountPayable2);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task ShowdGetAccountPayableByName()
        {
            await AddDummyAccountsPayable();

            string name = "Internet";
            var result = await _service.GetAccountsPayable(name);

            var acResult = result.FirstOrDefault();

            Assert.NotNull(result);
            Assert.Single(result);

            Assert.NotNull(acResult);
            Assert.Equal(name, acResult.Name);
        }

        [Fact]
        public async Task ShowdNotGetAccountPayableByName()
        {
            await AddDummyAccountsPayable();

            string name = "Credit Card";
            var result = await _service.GetAccountsPayable(name);

            var acResult = result.FirstOrDefault();

            Assert.NotNull(result);
            Assert.Empty(result);

            Assert.Null(acResult);
        }

        [Fact]
        public async Task ShowdGetAccountPayableByNameAndPaymentDate()
        {
            await AddDummyAccountsPayable();

            var name = "Light";
            var paymentDate = new DateTime(2024, 4, 9);

            var accountPayable = new AccountPayable()
            {
                Name = name,
                DueDate = new DateTime(2024, 4, 10),
                PaymentDate = paymentDate,
                OriginalAmount = 100,
                TotalAmount = 100,
            };

            _context.Add(accountPayable);
            await _context.SaveChangesAsync();

            var result = await _service.GetAccountsPayable(name, paymentDate);

            var acResult = result.FirstOrDefault();

            Assert.NotNull(result);
            Assert.Single(result);

            Assert.NotNull(acResult);
            Assert.Equal(paymentDate, acResult.PaymentDate.Date);
            Assert.Equal(name, acResult.Name);
        }

        [Fact]
        public async Task ShowdGetAccountPayableByPaymentDate()
        {
            await AddDummyAccountsPayable();

            var paymentDate = new DateTime(2024, 4, 9);
            var result = await _service.GetAccountsPayable(paymentDate);

            var acResult = result.FirstOrDefault();

            Assert.NotNull(result);
            Assert.Single(result);

            Assert.NotNull(acResult);
            Assert.Equal(paymentDate, acResult.PaymentDate.Date);
        }

        [Fact]
        public async Task ShowdNotGetAccountPayableByPaymentDate()
        {
            await AddDummyAccountsPayable();

            var paymentDate = new DateTime(2024, 3, 9);
            var result = await _service.GetAccountsPayable(paymentDate);

            var acResult = result.FirstOrDefault();

            Assert.NotNull(result);
            Assert.Empty(result);

            Assert.Null(acResult);
        }

        [Fact]
        public async Task ShowdGetAllAccountPayable()
        {
            await AddDummyAccountsPayable();

            var result = await _service.GetAccountsPayable();

            Assert.NotNull(result);
            Assert.Equal(4, result.Count());
        }

        [Fact]
        public async Task ShowldAlterAccountPayable()
        {
            await AddDummyAccountsPayable();

            var viewModel = new AccountPayableViewModel()
            {
                Id = 2,
                Name = "Test",
                DueDate = new DateTime(2024, 04, 12),
                PaymentDate = new DateTime(2024, 04, 12),
                OriginalAmount = 150
            };

            var accountPayable = await _service.UpdateAccountPayable(viewModel);

            Assert.NotNull(accountPayable);
            Assert.Equal(2, accountPayable.Id);

            accountPayable = await _context.AccountsPayable.FindAsync(viewModel.Id);

            Assert.NotNull(accountPayable);
            Assert.Equal(viewModel.Name, accountPayable.Name);
            Assert.Equal(viewModel.DueDate, accountPayable.DueDate);
            Assert.Equal(viewModel.PaymentDate, accountPayable.PaymentDate);
            Assert.Equal(viewModel.OriginalAmount, accountPayable.OriginalAmount);

        }

        public static TheoryData<int, decimal, decimal, decimal, decimal, decimal, decimal> LateDaysAccountPayable =
            new()
            {
                { 1, 150, 2,  3, 0.1M, 0.15M, 153.15M },
                { 2, 150, 2,  3, 0.1M, 0.30M, 153.30M },
                { 3, 150, 2,  3, 0.1M, 0.45M, 153.45M },
                { 4, 150, 3,  4.5M, 0.2M, 1.2M, 155.7M },
                { 5, 150, 3,  4.5M, 0.2M, 1.5M, 156M },
                { 6, 150, 3,  4.5M, 0.2M, 1.8M, 156.3M },
                { 7, 150, 3,  4.5M, 0.2M, 2.1M, 156.6M },
                { 8, 150, 3,  4.5M, 0.2M, 2.4M, 156.9M },
                { 9, 150, 3,  4.5M, 0.2M, 2.7M, 157.2M },
                { 10, 150, 3,  4.5M, 0.2M, 3, 157.5M },
                { 11, 150, 5,  7.5M, 0.3M, 4.95M, 162.45M },
                { 12, 150, 5,  7.5M, 0.3M, 5.4M, 162.9M },
                { 13, 150, 5,  7.5M, 0.3M, 5.85M, 163.35M },
                { 14, 150, 5,  7.5M, 0.3M, 6.3M, 163.8M },
                { 15, 150, 5,  7.5M, 0.3M, 6.75M, 164.25M },

            };

        [Theory, MemberData(nameof(LateDaysAccountPayable))]
        public async Task ShowldAlterLateAccountPayable(int lateDays, decimal originalAmount, decimal finePercentage, decimal fineAmount, decimal interestPercentage, decimal interestAmount, decimal totalAmount)
        {
            await AddDummyAccountsPayable();

            var dueDate = new DateTime(2024, 04, 12);
            var paymentDate = dueDate.AddDays(lateDays);

            var viewModel = new AccountPayableViewModel()
            {
                Id = 2,
                Name = "Test",
                DueDate = dueDate,
                PaymentDate = paymentDate,
                OriginalAmount = originalAmount
            };

            var accountPayable = await _service.UpdateAccountPayable(viewModel);

            Assert.NotNull(accountPayable);
            Assert.Equal(2, accountPayable.Id);

            accountPayable = await _context.AccountsPayable.FindAsync(viewModel.Id);

            Assert.NotNull(accountPayable);
            Assert.Equal(viewModel.Name, accountPayable.Name);
            Assert.Equal(viewModel.DueDate, accountPayable.DueDate);
            Assert.Equal(viewModel.PaymentDate, accountPayable.PaymentDate);
            Assert.Equal(viewModel.OriginalAmount, accountPayable.OriginalAmount);
            Assert.Equal(lateDays, accountPayable.LateDays);
            Assert.Equal(finePercentage, accountPayable.FinePercentage);
            Assert.Equal(fineAmount, accountPayable.FineAmount);
            Assert.Equal(interestPercentage, accountPayable.InterestPercentage);
            Assert.Equal(interestAmount, accountPayable.InterestAmount);
            Assert.Equal(totalAmount, accountPayable.TotalAmount);
        }

        [Fact]
        public async Task ShowldDeleteAccountPayable()
        {
            bool result = await _service.DeleteAccountPayable(2);

            Assert.True(result);
        }

        [Fact]
        public async Task ShowldNotDeleteAccountPayable()
        {
            bool result = await _service.DeleteAccountPayable(20);

            Assert.False(result);
        }
    }
}