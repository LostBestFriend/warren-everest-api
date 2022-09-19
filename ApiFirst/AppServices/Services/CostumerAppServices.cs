﻿using AppModels.MapperModels;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;

namespace AppServices.Services
{
    public class CustomerAppServices : ICustomerAppServices
    {
        private readonly ICustomerServices _customerServices;
        private readonly IMapper _mapper;

        public CustomerAppServices(ICustomerServices customerServices, IMapper mapper)
        {
            _customerServices = customerServices;
            _mapper = mapper;
        }

        public CustomerResponseDTO? GetByCpf(string cpf)
        {
            var result = _customerServices.GetByCpf(cpf);
            return _mapper.Map<CustomerResponseDTO>(result);
        }

        public long Create(CustomerCreateDTO model)
        {
            var mapped = _mapper.Map<Customer>(model);
            return _customerServices.Create(mapped).Id;
        }

        public bool Delete(int id)
        {
            var response = _customerServices.Delete(id);
            return response;
        }

        public List<CustomerResponseDTO> GetAll()
        {
            var result = _customerServices.GetAll();
            return _mapper.Map<List<CustomerResponseDTO>>(result);
        }

        public CustomerResponseDTO? GetById(int id)
        {
            var result = _customerServices.GetById(id);
            return _mapper.Map<CustomerResponseDTO>(result);
        }
        public void Update(int id, CustomerUpdateDTO model)
        {
            var mapped = _mapper.Map<Customer>(model);
            _customerServices.Update(id, mapped);
        }
        public void Modify(int id, string email)
        {
            _customerServices.Modify(id, email);
        }
    }
}

