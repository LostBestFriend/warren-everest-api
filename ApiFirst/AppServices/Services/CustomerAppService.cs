using AppModels.AppModels;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using Infrastructure.CrossCutting.ExtensionMethods;
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

        public CustomerResponse GetByCpf(string cpf)
        {
            cpf = FormatStringExtension.FormatString(cpf);
            var result = _customerServices.GetByCpf(cpf);
            return _mapper.Map<CustomerResponse>(result);
        }

        public long Create(CreateCustomer model)
        {
            var mapped = _mapper.Map<Customer>(model);
            return _customerServices.Create(mapped);
        }

        public void Delete(long id)
        {
            _customerServices.Delete(id);
        }

        public IEnumerable<CustomerResponse> GetAll()
        {
            var result = _customerServices.GetAll();
            return _mapper.Map<IEnumerable<CustomerResponse>>(result);
        }

        public CustomerResponse GetById(long id)
        {
            var result = _customerServices.GetById(id);
            return _mapper.Map<CustomerResponse>(result);
        }

        public void Update(UpdateCustomer model)
        {
            var mapped = _mapper.Map<Customer>(model);
            _customerServices.Update(mapped);
        }

        public void Modify(UpdateCustomer model)
        {
            var mapped = _mapper.Map<Customer>(model);
            _customerServices.Modify(mapped);
        }
    }
}
