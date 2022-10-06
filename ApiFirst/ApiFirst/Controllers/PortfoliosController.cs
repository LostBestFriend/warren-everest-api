using AppModels.AppModels.Portfolio;
using AppServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfoliosController : ControllerBase
    {
        private readonly IPortfolioAppService _portfolioAppService;

        public PortfoliosController(IPortfolioAppService repository)
        {
            _portfolioAppService = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var portolios = _portfolioAppService.GetAll();
                return Ok(portolios);
            }
            catch (Exception exception)
            {
                return Problem(exception.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            try
            {
                var portfolio = await _portfolioAppService.GetByIdAsync(id).ConfigureAwait(false);
                return Ok(portfolio);
            }
            catch (ArgumentNullException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception exception)
            {
                return Problem(exception.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(CreatePortfolio model)
        {
            try
            {
                long portfolioId = await _portfolioAppService.CreateAsync(model);
                return Ok(portfolioId);
            }
            catch (Exception exception)
            {
                return Problem(exception.Message);
            }
        }

        [HttpPatch("deposit")]
        public IActionResult Deposit(decimal amount, long customerId, long portfolioId)
        {
            try
            {
                _portfolioAppService.Deposit(amount, customerId, portfolioId);
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

        [HttpPatch("withdraw")]
        public IActionResult Withdraw(decimal amount, long customerId, long portfolioId)
        {
            try
            {
                _portfolioAppService.Withdraw(amount, customerId, portfolioId);
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

        [HttpPatch("invest")]
        public async Task<IActionResult> InvestAsync(int quotes, DateTime liquidateAt, long productId, long portfolioId)
        {
            try
            {
                await _portfolioAppService.InvestAsync(quotes, liquidateAt, productId, portfolioId);
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

        [HttpPatch("withdraw/{productid}")]
        public async Task<IActionResult> UninvestAsync(int quotes, DateTime liquidateAt, long productId, long portfolioId)
        {
            try
            {
                await _portfolioAppService.WithdrawProduct(quotes, liquidateAt, productId, portfolioId);
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

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                _portfolioAppService.Delete(id);
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
