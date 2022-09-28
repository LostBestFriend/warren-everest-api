using AppModels.AppModels;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using System;
using System.Collections.Generic;

namespace AppServices.Services
{
    public class CustomerAppServices : ICustomerAppService
    {
        private readonly ICustomerService _customerServices;
        private readonly IMapper _mapper;

        public CustomerAppServices(ICustomerService customerServices, IMapper mapper)
        {
            _customerServices = customerServices ?? throw new ArgumentNullException(nameof(customerServices));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public CustomerResponseDTO GetByCpf(string cpf)
        {
            var result = _customerServices.GetByCpf(cpf);
            return _mapper.Map<CustomerResponseDTO>(result);
        }

        public long Create(CreateCustomerDTO model)
        {
            var mapped = _mapper.Map<Customer>(model);
            return _customerServices.Create(mapped);
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

        public CustomerResponseDTO GetById(long id)
        {
            var result = _customerServices.GetById(id);
            return _mapper.Map<CustomerResponseDTO>(result);
        }

        public void Update(long id, UpdateCustomerDTO model)
        {
            var mapped = _mapper.Map<Customer>(model);
            mapped.Id = id;
            _customerServices.Update(mapped);
        }

        public void Modify(long id, UpdateCustomerDTO model)
        {
            var mapped = _mapper.Map<Customer>(model);
            mapped.Id = id;
            _customerServices.Modify(mapped);
        }
    }
}
