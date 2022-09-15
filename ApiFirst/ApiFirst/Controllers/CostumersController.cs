using DomainModels.Models;
using DomainServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        [HttpGet("cpf/{cpf}")]
        public Customer? GetByCpf(string cpf)
        {
            return _customerRepository.GetByCpf(cpf);
        }


        [HttpGet]
        public List<Customer> GetAll()
        {
            return _customerRepository.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Customer?> GetById(int id)
        {

            var response = _customerRepository.GetById(id);

            if (response is null)
            {
                return NotFound($"Não foi encontrado Costumer para o Id: {id}");
            }
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Customer model)
        {
            if (_customerRepository.Create(model))
            {
                return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
            }
            return BadRequest("O Email ou CPF já é usado.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_customerRepository.Delete(id))
            {
                return Ok();
            }
            return NotFound($"Usuário não encontrado para o id: {id}");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Customer model)
        {
            int result = _customerRepository.Update(id, model);

            if (result == 1)
            {
                return Ok();
            }
            else if (result == 0)
            {
                return BadRequest("Já existe Customer com estes dados de CPF e Email.");
            }
            return NotFound($"Não foi encontrado Customer para os valores do campo CPF: {model.Cpf}.");
        }

        [HttpPatch("{id}")]
        public IActionResult Modify(int id, Customer model)
        {
            int result = _customerRepository.Modify(id, model);

            if (result == -1)
            {
                return NotFound($"Não foi encontrado Customer para os valores do campo CPF: {model.Cpf}.");
            }
            else if (result == 0)
            {
                return BadRequest("Já existe Customer com estes dados de CPF e Email.");
            }
            return Ok();
        }
    }
}
