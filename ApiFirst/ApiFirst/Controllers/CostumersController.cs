using AppModels.MapperModels;
using AppServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerAppServices _customerAppServices;

        public CustomersController(ICustomerAppServices customerAppServices)
        {
            _customerAppServices = customerAppServices ?? throw new ArgumentNullException(nameof(customerAppServices));
        }

        [HttpGet("cpf/{cpf}")]
        public IActionResult GetByCpf(string cpf)
        {
            var response = _customerAppServices.GetByCpf(cpf);
            return response is null
                ? NotFound($"Não foi encontrado Customer para o CPF: {cpf}")
                : Ok(response);
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_customerAppServices.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var response = _customerAppServices.GetById(id);
            return response is null
                ? NotFound($"Não foi encontrado Costumer para o Id: {id}")
                : Ok(response);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CustomerCreateDTO model)
        {
            try
            {
                long id = _customerAppServices.Create(model);
                return CreatedAtAction(nameof(GetById), new { id }, id);
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
                return Ok();
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
        public IActionResult Update(int id, CustomerUpdateDTO model)
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
        public IActionResult Modify(int id, CustomerUpdateDTO model)
        {
            try
            {
                _customerAppServices.Modify(id, model);
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
