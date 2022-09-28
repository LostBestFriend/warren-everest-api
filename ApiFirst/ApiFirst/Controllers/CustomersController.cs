using AppModels.AppModels;
using AppServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerAppService _customerAppServices;

        public CustomersController(ICustomerAppService customerAppServices)
        {
            _customerAppServices = customerAppServices ?? throw new ArgumentNullException(nameof(customerAppServices));
        }

        [HttpGet("cpf/{cpf}")]
        public IActionResult GetByCpf(string cpf)
        {
            try
            {
                var response = _customerAppServices.GetByCpf(cpf);
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
            var response = _customerAppServices.GetAll();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var response = _customerAppServices.GetById(id);
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
        public IActionResult Create([FromBody] CreateCustomerDTO model)
        {
            try
            {
                long id = _customerAppServices.Create(model);
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
        public IActionResult Delete(int id)
        {
            try
            {
                _customerAppServices.Delete(id);
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
        public IActionResult Update(int id, UpdateCustomerDTO model)
        {
            try
            {
                _customerAppServices.Update(id, model);
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

        [HttpPatch("{id}")]
        public IActionResult UpdateEmail(int id, string email)
        {
            try
            {
                _customerAppServices.UpdateEmail(id, email);
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
