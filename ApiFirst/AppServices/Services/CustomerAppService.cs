using AppModels.AppModels.Customers;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;
using Infrastructure.CrossCutting.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServices.Services
{
    public class CustomerAppService : ICustomerAppService
    {
        private readonly ICustomerService _customerServices;
        private readonly IMapper _mapper;
        private readonly ICustomerBankInfoService _customerBankInfoAppService;

        public CustomerAppService(ICustomerBankInfoService bankInfoAppServices, ICustomerService customerServices, IMapper mapper)
        {
            _customerServices = customerServices ??
                throw new ArgumentNullException(nameof(customerServices));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _customerBankInfoAppService = bankInfoAppServices ??
                throw new ArgumentNullException(nameof(bankInfoAppServices));
        }

        public async Task<CustomerResponse> GetByCpfAsync(string cpf)
        {
            cpf = cpf.FormatString();
            var result = await _customerServices.GetByCpfAsync(cpf).ConfigureAwait(false);
            return _mapper.Map<CustomerResponse>(result);
        }

        public async Task<long> CreateAsync(CreateCustomer model)
        {
            var mapped = _mapper.Map<Customer>(model);
            long customerId = await _customerServices.CreateAsync(mapped).ConfigureAwait(false);
            _customerBankInfoAppService.Create(customerId);
            return customerId;
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

        public async Task<CustomerResponse> GetByIdAsync(long id)
        {
            var result = await _customerServices.GetByIdAsync(id).ConfigureAwait(false);
            return _mapper.Map<CustomerResponse>(result);
        }
        public void Update(UpdateCustomer model)
        {
            var mapped = _mapper.Map<Customer>(model);
            _customerServices.Update(mapped);
        }
    }
}
