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
            var response = _customerAppServices.GetByCpf(cpf);
            return response;
        }
        [HttpGet]
        public IList<Customer> GetAll()
        {
            var response = _customerAppServices.GetAll();
            return response;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var response = _customerAppServices.GetById(id);
            return response is null
                ? NotFound($"Não foi encontrado Customer para o Id: {id}")
                : Ok(response);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Customer model)
        {
            var response = _customerAppServices.Create(model);
            return response
                ? Created("", model.Id)
                : BadRequest("O Email ou CPF já existem.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = _customerAppServices.Delete(id);
            return response
                ? Ok()
                : NotFound($"Usuário não encontrado para o id: {id}");
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
        public IActionResult Modify(int id, string email)
        {
            int result = _customerAppServices.Modify(id, email);

            if (result == -1)
            {
                return NotFound($"Usuário não encontrado para o id: {id}");
            }
            else if (result == 0)
            {
                return BadRequest("Já existe Customer com estes dados de Email.");
            }
            return Ok();
        }
    }
}
