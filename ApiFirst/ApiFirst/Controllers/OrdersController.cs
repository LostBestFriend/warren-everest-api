using AppModels.AppModels.Orders;
using AppServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderAppService _orderAppService;

        public OrdersController(IOrderAppService repository)
        {
            _orderAppService = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var orders = await _orderAppService.GetAllAsync().ConfigureAwait(false);
                return Ok(orders);
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
                var order = await _orderAppService.GetByIdAsync(id).ConfigureAwait(false);
                return Ok(order);
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
        public async Task<IActionResult> CreateAsync(CreateOrder model)
        {
            try
            {
                long orderId = await _orderAppService.CreateAsync(model).ConfigureAwait(false);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = orderId }, orderId);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, UpdateOrder model)
        {
            try
            {
                _orderAppService.Update(id, model);
                return Ok();
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
    }
}
