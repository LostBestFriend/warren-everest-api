using AppServices.DTOs;
using AppServices.Interfaces;
using AutoMapper;
using DomainModels.Entities;
using DomainServices.Interfaces;

namespace AppServices.Services
{
    public class CustomerService : ICustomerService
    {
        ICustomerRepository _repo;
        protected readonly IMapper _mapper;

        public CustomerService(ICustomerRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public int DeleteFromList(string cpf, string email)
        {
            return _repo.DeleteFromList(cpf, email);
        }

        public CustomerDTO GetSpecificFromList(string cpf, string email)
        {
            return _mapper.Map<CustomerDTO>(_repo.GetSpecificFromList(cpf, email));
        }

        public int Create(CustomerDTO model)
        {
            var mapCustomer = _mapper.Map<Customer>(model);
            return _repo.Create(mapCustomer);
        }

        public int Delete(int id)
        {
            return _repo.Delete(id);
        }

        public List<CustomerDTO> GetAll()
        {
            return _mapper.Map<List<CustomerDTO>>(_repo.GetAll());
        }

        public CustomerDTO GetSpecific(int id)
        {
            return _mapper.Map<CustomerDTO>(_repo.GetSpecific(id));
        }
        public int Update(CustomerDTO model)
        {
            var mapCustomer = _mapper.Map<Customer>(model);
            return _repo.Update(mapCustomer);
        }
        public int Modify(CustomerDTO model)
        {
            var mapCustomer = _mapper.Map<Customer>(model);
            return _repo.Modify(mapCustomer);
        }
    }
}

