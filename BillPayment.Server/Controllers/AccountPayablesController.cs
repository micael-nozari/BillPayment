using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BillPayment.Server.Context;
using BillPayment.Server.Models.EntityModels;
using BillPayment.Server.Service;
using BillPayment.Server.Models.ViewModel;
using NuGet.Protocol;

namespace BillPayment.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountPayablesController : ControllerBase
    {
        private IAccountsPayableService _service;

        public AccountPayablesController(BillPaymentContext context, IAccountsPayableService service)
        {
            _service = service;
        }

        // GET: api/AccountPayables
        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<AccountPayableResult>>> GetAccountsPayable()
        {
            var accountsPayable = await _service.GetAccountsPayable();
            if(accountsPayable.Count() == 0) 
            { 
                return NotFound();
            }

            return Ok(new AccountPayableListResult(accountsPayable.ToList()).ToJson());
        }

        // GET: api/AccountPayables/AccountPayablesByName?name=abc
        [HttpGet("AccountPayablesByName")]
        public async Task<ActionResult<IAsyncEnumerable<AccountPayableResult>>> GetAccountsPayableByName([FromQuery] string? name)
        {
            var accountsPayable = await _service.GetAccountsPayable(name);
            if (accountsPayable.Count() == 0)
            {
                return NotFound();
            }

            return Ok(new AccountPayableListResult(accountsPayable.ToList()).ToJson());
        }

        // GET: api/AccountPayables/AccountPayablesByPaymentDate?paymentDate=2024-03-30
        [HttpGet("AccountPayablesByPaymentDate")]
        public async Task<ActionResult<IAsyncEnumerable<AccountPayableResult>>> GetAccountsPayableByPaymentDate([FromQuery] DateTime? paymentDate)
        {
            var accountsPayable = await _service.GetAccountsPayable(paymentDate);
            if (accountsPayable.Count() == 0)
            {
                return NotFound();
            }

            return Ok(new AccountPayableListResult(accountsPayable.ToList()).ToJson());
        }

        [HttpGet("AccountPayablesByNameAndPaymentDate")]
        public async Task<ActionResult<IAsyncEnumerable<AccountPayableResult>>> GetAccountsPayableByNameAndPaymentDate([FromQuery] string? name, [FromQuery] DateTime? paymentDate)
        {
            var accountsPayable = await _service.GetAccountsPayable(name, paymentDate);
            if (accountsPayable.Count() == 0)
            {
                return NotFound();
            }

            return Ok(new AccountPayableListResult(accountsPayable.ToList()).ToJson());
        }

        // GET: api/AccountPayables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountPayable>> GetAccountPayable(int id)
        {
            var accountPayable = await _service.GetAccountPayable(id);

            if (accountPayable == null)
            {
                return NotFound();
            }

            return Ok(new AccountPayableResult(accountPayable).ToJson());
        }

        // PUT: api/AccountPayables/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountPayable(int id, AccountPayableViewModel vmAccountPayable)
        {
            if (id != vmAccountPayable.Id)
            {
                return BadRequest();
            }

            var viewModelError = await ValidateViewModel(vmAccountPayable);
            if (!string.IsNullOrEmpty(viewModelError))
                return BadRequest(viewModelError);

            var accountPayable = await _service.UpdateAccountPayable(vmAccountPayable);
            if (accountPayable == null)
                return NotFound();

            return Ok(new AccountPayableResult(accountPayable).ToJson());
        }

        // POST: api/AccountPayables
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccountPayable>> PostAccountPayable(AccountPayableViewModel viewModel)
        {
            var viewModelError = await ValidateViewModel(viewModel);
            if (!string.IsNullOrEmpty(viewModelError))
                return BadRequest(viewModelError);

            var accountPayable = await _service.CreateAccountPayable(viewModel);

            return Ok(new AccountPayableResult(accountPayable).ToJson());
        }

        //// DELETE: api/AccountPayables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountPayable(int id)
        {
            if(! await _service.DeleteAccountPayable(id))
            {
                return NotFound();
            }

            return Ok("Account Payable successfully deleted");
        }

        private async Task<string> ValidateViewModel(AccountPayableViewModel viewModel)
        {
            if (viewModel.PaymentDate == DateTime.MinValue)
            {
                return "Invalid payment date";
            }

            if (viewModel.DueDate == DateTime.MinValue)
            {
                return "Invalid due date";
            }

            if (viewModel.OriginalAmount == 0)
            {
                return "Invalid original amount";
            }

            var accountsPayable = await _service.GetAccountsPayable(viewModel.PaymentDate);
            if (accountsPayable != null && accountsPayable.Any())
            {
                return "It's not possible to register two accounts with the same payment date";
            }

            return null;
        }

    }
}
