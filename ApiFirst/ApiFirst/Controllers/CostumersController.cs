using DomainModels.Models;
using DomainServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _service;

        public CustomersController(ICustomerRepository repo)
        {
            _service = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        [HttpGet("get-by-cpf")]
        public Customer? GetByCpf([FromQuery] string cpf)
        {
            return _service.GetByCpf(cpf);
        }


        [HttpGet("get-all")]
        public List<Customer> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet]
        public Customer? GetById(int id)
        {
            return _service.GetById(id);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Customer model)
        {
            if (_service.Create(model))
            {
                return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
            }
            return BadRequest("O Email ou CPF já é usado.");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (_service.Delete(id))
            {
                return Ok();
            }
            return NotFound($"Usuário não encontrado para o id: {id}");
        }

        [HttpPatch]
        public IActionResult Update(string cpf, Customer model)
        {
            int result = _service.Update(cpf, model);

            if (result == 1)
            {
                return Ok();
            }
            else if (result == 0)
            {
                return BadRequest("Já existe Customer com estes dados de CPF e Email.");
            }
            return NotFound($"Não foi encontrado Customer para os valores do campo CPF: {cpf}.");
        }

        [HttpPut]
        public IActionResult Modify(string cpf, Customer model)
        {
            int result = _service.Modify(cpf, model);

            if (result == -1)
            {
                return NotFound($"Não foi encontrado Customer para os valores do campo CPF: {cpf}.");
            }
            else if (result == 0)
            {
                return BadRequest("Já existe Customer com estes dados de CPF e Email.");
            }
            return Ok();
        }
    }
}
