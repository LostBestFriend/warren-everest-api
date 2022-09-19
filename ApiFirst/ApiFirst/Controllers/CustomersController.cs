using ApiFirst;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }

        [HttpGet("cpf/{cpf}")]
        public Customer? GetByCpf(string cpf)
        {
            var response = _customerService.GetByCpf(cpf);

            return response;
        }


        [HttpGet]
        public IList<Customer> GetAll()
        {
            var response = _customerService.GetAll();

            return response;
        }

        [HttpGet("{id}")]
        public ActionResult<Customer?> GetById(int id)
        {
            var response = _customerService.GetById(id);

            return response is null
                ? NotFound($"Não foi encontrado Customer para o Id: {id}")
                : Ok(response);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Customer model)
        {
            var response = _customerService.Create(model);

            return response
                ? Created("", model.Id)
                : BadRequest("O Email ou CPF já existem.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = _customerService.Delete(id);

            return response
                ? Ok()
                : NotFound($"Usuário não encontrado para o id: {id}");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Customer model)
        {
            var result = _customerService.Update(id, model);

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
        public IActionResult Modify(int id, string email)
        {
            var result = _customerService.Modify(id, email);

            if (result == -1)
            {
                return NotFound($"Não foi encontrado Customer para os valores do campo id: {id}.");
            }
            else if (result == 0)
            {
                return BadRequest("Já existe Customer com estes dados de Email.");
            }
            return Ok();
        }
    }
}
