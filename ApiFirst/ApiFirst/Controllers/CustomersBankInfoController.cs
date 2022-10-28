using AppServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersBankInfoController : ControllerBase
    {
        private readonly ICustomerBankInfoAppService _customerBankInfoAppService;

        public CustomersBankInfoController(ICustomerBankInfoAppService customerBankInfoAppServices)
        {
            _customerBankInfoAppService = customerBankInfoAppServices ?? throw new ArgumentNullException(nameof(customerBankInfoAppServices));
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetAccountBalance(long customerId)
        {
            try
            {
                decimal accountBalance = await _customerBankInfoAppService.GetBalanceAsync(customerId).ConfigureAwait(false);
                return Ok(accountBalance);
            }
            catch (ArgumentNullException exception)
            {
                return NotFound(exception.Message);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                return Problem(exception.Message);
            }
        }

        [HttpPatch("{customerId}/deposit")]
        public async Task<IActionResult> DepositAsync(long customerId, decimal amount)
        {
            try
            {
                await _customerBankInfoAppService.DepositAsync(customerId, amount).ConfigureAwait(false);
                return Ok();
            }
            catch (ArgumentNullException exception)
            {
                return NotFound(exception.Message);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                return Problem(exception.Message);
            }
        }

        [HttpPatch("{customerId}/withdraw")]
        public async Task<IActionResult> WithdrawAsync(long customerId, decimal amount)
        {
            try
            {
                await _customerBankInfoAppService.WithdrawAsync(customerId, amount).ConfigureAwait(false);
                return Ok();
            }
            catch (ArgumentNullException exception)
            {
                return NotFound(exception.Message);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                return Problem(exception.Message);
            }
        }
    }
}
