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

        [HttpDelete("DeleteFromList")]
        public ActionResult DeleteFromList([FromQuery] string cpf, string email)
        {
            int result = _service.DeleteFromList(cpf, email);

            if (result == 404)
            {
                return NotFound("Não foi encontrado Customer para os valores do campo E-mail: " + email + " e CPF: " + cpf + ".");
            }
            return StatusCode(result);
        }

        [HttpGet("GetSpecificFromList")]
        public Customer GetSpecificFromList([FromQuery] string cpf, string email)
        {
            return _service.GetSpecificFromList(cpf, email);
        }


        [HttpGet("GetAll")]
        public virtual List<Customer> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet("GetSpecific")]
        public virtual Customer GetSpecific(int id)
        {
            return _service.GetSpecific(id);
        }

        [HttpPost]
        public virtual ActionResult Create([FromBody] Customer model)
        {
            model.Cpf = model.Cpf.Trim().Replace(".", "").Replace("-", "");
            return StatusCode(_service.Create(model));
        }

        [HttpDelete]
        public virtual ActionResult Delete(int id)
        {
            return StatusCode(_service.Delete(id));
        }

        [HttpPatch]
        public virtual ActionResult Update(Customer model)
        {
            model.Cpf = model.Cpf.Trim().Replace(".", "").Replace("-", "");
            int result = _service.Update(model);

            if (result == 404)
            {
                return NotFound("Não foi encontrado Customer para os valores do campo E-mail e CPF.");
            }
            return StatusCode(result);
        }

        [HttpPut]
        public virtual ActionResult Modify(Customer model)
        {
            model.Cpf = model.Cpf.Trim().Replace(".", "").Replace("-", "");

            int result = _service.Modify(model);

            if (result == 404)
            {
                return NotFound("Não foi encontrado Customer para os valores do campo E-mail e CPF.");
            }
            return StatusCode(result);
        }
    }
}
