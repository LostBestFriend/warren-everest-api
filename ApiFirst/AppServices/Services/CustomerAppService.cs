using AppModels.AppModels;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServices.Services
{
    public class CustomerAppService : ICustomerAppService
    {
        private readonly ICustomerService _customerServices;
        private readonly IMapper _mapper;

        public CustomerAppService(ICustomerService customerServices, IMapper mapper)
        {
            _customerServices = customerServices ?? throw new ArgumentNullException(nameof(customerServices));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CustomerResponseDTO> GetByCpfAsync(string cpf)
        {
            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            var result = await _customerServices.GetByCpfAsync(cpf);
            return _mapper.Map<CustomerResponseDTO>(result);
        }

        public async Task<long> CreateAsync(CreateCustomerDTO model)
        {
            var mapped = _mapper.Map<Customer>(model);
            return await _customerServices.CreateAsync(mapped);
        }

        public void Delete(long id)
        {
            _customerServices.Delete(id);
        }

        public IEnumerable<CustomerResponseDTO> GetAll()
        {
            var result = _customerServices.GetAll();
            return _mapper.Map<IEnumerable<CustomerResponseDTO>>(result);
        }

        public async Task<CustomerResponseDTO> GetByIdAsync(long id)
        {
            var result = await _customerServices.GetByIdAsync(id);
            return _mapper.Map<CustomerResponseDTO>(result);
        }
        public void Update(UpdateCustomerDTO model)
        {
            var mapped = _mapper.Map<Customer>(model);
            _customerServices.Update(mapped);
        }
        public void Modify(UpdateCustomerDTO model)
        {
            var mapped = _mapper.Map<Customer>(model);
            _customerServices.Modify(mapped);
        }
    }
}
