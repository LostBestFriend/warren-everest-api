using AppServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public IActionResult GetAccountBalance(long customerId)
        {
            try
            {
                decimal accountBalance = _customerBankInfoAppService.GetBalance(customerId);
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
        public IActionResult Deposit(long customerId, decimal amount)
        {
            try
            {
                _customerBankInfoAppService.Deposit(customerId, amount);
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
        public IActionResult Withdraw(long customerId, decimal amount)
        {
            try
            {
                _customerBankInfoAppService.Withdraw(customerId, amount);
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
