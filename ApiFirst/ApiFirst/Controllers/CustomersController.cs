using AppModels.AppModels.Customer;
using AppServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerAppService _customerAppService;

        public CustomersController(ICustomerAppService customerAppServices)
        {
            _customerAppService = customerAppServices ?? throw new ArgumentNullException(nameof(customerAppServices));
        }

        [HttpGet("cpf/{cpf}")]
        public async Task<IActionResult> GetByCpfAsync(string cpf)
        {
            try
            {
                var response = await _customerAppService.GetByCpfAsync(cpf).ConfigureAwait(false);
                return Ok(response);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _customerAppService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            try
            {
                var response = await _customerAppService.GetByIdAsync(id).ConfigureAwait(false);
                return Ok(response);
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCustomer model)
        {
            try
            {
                long id = await _customerAppService.CreateAsync(model).ConfigureAwait(false);
                return Created("", id);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                _customerAppService.Delete(id);
                return NoContent();
            }
            catch (ArgumentNullException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(UpdateCustomer model)
        {
            try
            {
                _customerAppService.Update(model);
                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
