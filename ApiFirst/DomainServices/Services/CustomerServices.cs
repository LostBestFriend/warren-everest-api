using DomainModels.Models;
using DomainServices.Interfaces;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DomainServices.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly WarrenContext _context;
        private readonly DbSet<Customer> _customers;

        public CustomerServices(WarrenContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _customers = _context.Set<Customer>();
        }

        public long Create(Customer model)
        {
            model.Id = _customers.LastOrDefault()?.Id + 1 ?? 0;
            if (Exists(model))
            {
                throw new ArgumentException("O CPF ou Email já estão sendo usados.");
            }

            _customers.Add(model);
            _context.SaveChanges();
            return model.Id;
        }

        public void Delete(int id)
        {
            Customer? customer = _customers.FirstOrDefault(customer => customer.Id == id);

            if (customer is null) throw new ArgumentNullException($"Cliente não encontrado para o id: {id}");

            _customers.Remove(customer).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();
        }

        public IEnumerable<Customer> GetAll()
        {
            return _customers;
        }

        public Customer? GetByCpf(string cpf)
        {
            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            var response = _customers.FirstOrDefault(customer => customer.Cpf == cpf);
            return response;
        }

        public void Update(Customer model)
        {

            int index = _customers.ToList().FindIndex(customer => customer.Id == model.Id);
            if (index == -1) throw new ArgumentNullException($"$Não foi encontrado Customer para o Id: {model.Id}");
            if (ExistsUpdate(_customers.ToList()[index].Id, model)) throw new ArgumentException($"Já existe usuário com o E-mail ou CPF digitados");

            _customers.Update(model);
            _context.SaveChanges();
        }

        public bool ExistsUpdate(long id, Customer model)
        {
            var response = _customers.Any(customer => (customer.Cpf == model.Cpf || customer.Email == model.Email) && customer.Id != id);
            return response;
        }

        public bool Exists(Customer model)
        {
            var response = _customers.Any(customer => customer.Cpf == model.Cpf || customer.Email == model.Email);
            return response;
        }

        public Customer? GetById(int id)
        {
            var response = _customers.FirstOrDefault(x => x.Id == id);
            return response;
        }

        public void Modify(Customer model)
        {
            int index = _customers.ToList().FindIndex(customer => customer.Id == model.Id);
            if (index == -1) throw new ArgumentNullException($"Não foi encontrado Customer para o Id: {model.Id}");

            if (ExistsUpdate(_customers.ToList()[index].Id, model)) throw new ArgumentException($"Já existe usuário com o E-mail ou CPF digitados");

            model.Id = _customers.ToList()[index].Id;
            _context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
