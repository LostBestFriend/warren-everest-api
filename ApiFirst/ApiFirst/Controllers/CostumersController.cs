﻿using AppModels.MapperModels;
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
        public async Task<IActionResult> GetByCpfAsync(string cpf)
        {
            try
            {
                var response = await _customerAppServices.GetByCpfAsync(cpf).ConfigureAwait(false);
                return Ok(response);
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(e.Message);
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
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var response = await _customerAppServices.GetByIdAsync(id).ConfigureAwait(false);
                return Ok(response);
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CustomerCreateDTO model)
        {
            try
            {
                long id = await _customerAppServices.CreateAsync(model).ConfigureAwait(false);
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
