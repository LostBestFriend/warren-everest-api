using AppServices.Interfaces;
using DomainModels.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
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
        public Customer? GetByCpf(string cpf)
        {
            return _customerAppServices.GetByCpf(cpf);
        }


        [HttpGet]
        public List<Customer> GetAll()
        {
            return _customerAppServices.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Customer?> GetById(int id)
        {

            var response = _customerAppServices.GetById(id);

            if (response is null)
            {
                return NotFound($"Não foi encontrado Costumer para o Id: {id}");
            }
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Customer model)
        {
            if (_customerAppServices.Create(model))
            {
                return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
            }
            return BadRequest("O Email ou CPF já é usado.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_customerAppServices.Delete(id))
            {
                return Ok();
            }
            return NotFound($"Usuário não encontrado para o id: {id}");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Customer model)
        {
            int result = _customerAppServices.Update(id, model);

            if (result == 1)
            {
                return Ok();
            }
            else if (result == 0)
            {
                return BadRequest("Já existe Customer com estes dados de CPF e Email.");
            }
            return NotFound($"Usuário não encontrado para o id: {id}");
        }

        [HttpPatch("{id}")]
        public IActionResult Modify(int id, Customer model)
        {
            int result = _customerAppServices.Modify(id, model);

            if (result == -1)
            {
                return NotFound($"Usuário não encontrado para o id: {id}");
            }
            else if (result == 0)
            {
                return BadRequest("Já existe Customer com estes dados de CPF e Email.");
            }
            return Ok();
        }
    }
}
