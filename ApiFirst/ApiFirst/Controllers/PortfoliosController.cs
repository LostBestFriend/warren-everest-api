﻿using AppModels.AppModels.Portfolios;
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
        public async Task<IActionResult> GetAllAsync()
        {
            var portolios = await _portfolioAppService.GetAllAsync().ConfigureAwait(false);
            return Ok(portolios);
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
        public async Task<IActionResult> CreateAsync(CreatePortfolio model)
        {
            try
            {
                long portfolioId = await _portfolioAppService.CreateAsync(model).ConfigureAwait(false);
                return Ok(portfolioId);
            }
            catch (Exception exception)
            {
                return Problem(exception.Message);
            }
        }

        [HttpPatch("deposit")]
        public async Task<IActionResult> DepositAsync(decimal amount, long customerId, long portfolioId)
        {
            try
            {
                await _portfolioAppService.DepositAsync(amount, customerId, portfolioId).ConfigureAwait(false);
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
        public async Task<IActionResult> WithdrawAsync(decimal amount, long customerId, long portfolioId)
        {
            try
            {
                await _portfolioAppService.WithdrawAsync(amount, customerId, portfolioId).ConfigureAwait(false);
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
                await _portfolioAppService.InvestAsync(quotes, liquidateAt, productId, portfolioId).ConfigureAwait(false);
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

        [HttpPatch("withdraw/{productId}")]
        public async Task<IActionResult> RemoveMoneyAsync(int quotes, DateTime liquidateAt, long productId, long portfolioId)
        {
            try
            {
                await _portfolioAppService.WithdrawProductAsync(quotes, liquidateAt, productId, portfolioId).ConfigureAwait(false);
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
        public async Task<IActionResult> DeleteAsync(long id)
        {
            try
            {
                await _portfolioAppService.DeleteAsync(id).ConfigureAwait(false);
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
