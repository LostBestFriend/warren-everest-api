using AppModels.AppModels.Products;
using AppServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductAppService _productAppService;

        public ProductsController(IProductAppService repository)
        {
            _productAppService = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var portolios = _productAppService.GetAll();
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
                var customer = await _productAppService.GetByIdAsync(id).ConfigureAwait(false);
                return Ok(customer);
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
        public async Task<IActionResult> CreateAsync(CreateProduct model)
        {
            try
            {
                long productId = await _productAppService.CreateAsync(model);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = productId }, productId);
            }
            catch (Exception exception)
            {
                return Problem(exception.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, UpdateProduct model)
        {
            try
            {
                _productAppService.Update(id, model);
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

        [HttpDelete]
        public IActionResult Delete(long id)
        {
            try
            {
                _productAppService.Delete(id);
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
