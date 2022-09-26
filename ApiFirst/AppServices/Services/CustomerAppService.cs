using AppModels.AppModels;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels.Models;
using DomainServices.Interfaces;

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
            return _customerServices.Create(mapped).Id;
        }

        public void Delete(int id)
        {
            _customerServices.Delete(id);
        }

        public List<CustomerResponseDTO> GetAll()
        {
            var result = _customerServices.GetAll();
            return _mapper.Map<List<CustomerResponseDTO>>(result);
        }

        public CustomerResponseDTO GetById(int id)
        {
            var result = _customerServices.GetById(id);
            return _mapper.Map<CustomerResponseDTO>(result);
        }
        public void Update(int id, UpdateCustomerDTO model)
        {
            var mapped = _mapper.Map<Customer>(model);
            _customerServices.Update(id, mapped);
        }
        public void ChangeEmail(int id, string email)
        {
            _customerServices.ChangeEmail(id, email);
        }
    }
}
