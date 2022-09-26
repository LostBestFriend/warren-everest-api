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

        public async Task<CustomerResponseDTO> GetByCpfAsync(string cpf)
        {
            var result = await _customerServices.GetByCpfAsync(cpf);
            return _mapper.Map<CustomerResponseDTO>(result);
        }

        public async Task<long> CreateAsync(CreateCustomerDTO model)
        {
            var mapped = _mapper.Map<Customer>(model);
            return await _customerServices.CreateAsync(mapped);
        }

        public void Delete(int id)
        {
            _customerServices.Delete(id);
        }

        public IEnumerable<CustomerResponseDTO> GetAll()
        {
            var result = _customerServices.GetAll();
            return _mapper.Map<List<CustomerResponseDTO>>(result);
        }

        public async Task<CustomerResponseDTO> GetByIdAsync(int id)
        {
            var result = await _customerServices.GetByIdAsync(id);
            return _mapper.Map<CustomerResponseDTO>(result);
        }
        public void Update(int id, UpdateCustomerDTO model)
        {
            var mapped = _mapper.Map<Customer>(model);
            mapped.Id = id;
            _customerServices.Update(mapped);
        }
        public void Modify(int id, UpdateCustomerDTO model)
        {
            var mapped = _mapper.Map<Customer>(model);
            mapped.Id = id;
            _customerServices.Modify(mapped);
        }
    }
}
