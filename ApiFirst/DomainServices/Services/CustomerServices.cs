using DomainModels.Models;
using DomainServices.Interfaces;
using Infrastructure.Data.Context;

namespace DomainServices.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly WarrenContext _context;

        public CustomerServices(WarrenContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public long Create(Customer model)
        {
            model.Id = _context.Set<Customer>().ToList().LastOrDefault()?.Id + 1 ?? 0;
            if (Exists(model))
            {
                throw new ArgumentException("O CPF ou Email já estão sendo usados.");
            }
            else
            {
                _context.Set<Customer>().Add(model);
                _context.SaveChanges();
                return model.Id;
            }
        }

        public void Delete(int id)
        {
            Customer? customer = _context.Set<Customer>().FirstOrDefault(customer => customer.Id == id);
            if (customer is null) throw new ArgumentNullException($"Cliente não encontrado para o id: {id}");
            _context.Set<Customer>().Remove(customer).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();
        }

        public List<Customer> GetAll()
        {
            return _context.Set<Customer>().ToList();
        }

        public Customer? GetByCpf(string cpf)
        {
            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            return _context.Set<Customer>().FirstOrDefault(customer => customer.Cpf == cpf);
        }

        public void Update(Customer model)
        {

            int index = _context.Set<Customer>().ToList().FindIndex(customer => customer.Id == model.Id);
            if (index == -1) throw new ArgumentNullException($"$Não foi encontrado Customer para o Id: {model.Id}");
            if (ExistsUpdate(_context.Set<Customer>().ToList()[index].Id, model)) throw new ArgumentException($"Já existe usuário com o E-mail ou CPF digitados");
            else
            {
                _context.Set<Customer>().Update(model);
                _context.SaveChanges();
            }
        }

        public bool ExistsUpdate(long id, Customer model)
        {
            return _context.Set<Customer>().ToList().Any(customer => (customer.Cpf == model.Cpf || customer.Email == model.Email) && customer.Id != id);
        }

        public bool Exists(Customer model)
        {
            return _context.Set<Customer>().ToList().Any(customer => customer.Cpf == model.Cpf || customer.Email == model.Email);
        }

        public Customer? GetById(int id)
        {
            return _context.Set<Customer>().FirstOrDefault(x => x.Id == id);
        }

        public void Modify(Customer model)
        {
            int index = _context.Set<Customer>().ToList().FindIndex(customer => customer.Id == model.Id);
            if (index == -1) throw new ArgumentNullException($"$Não foi encontrado Customer para o Id: {model.Id}");

            if (ExistsUpdate(_context.Set<Customer>().ToList()[index].Id, model)) throw new ArgumentException($"Já existe usuário com o E-mail ou CPF digitados");
            else
            {
                model.Id = _context.Set<Customer>().ToList()[index].Id;
                _context.Entry<Customer>(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
        }
    }
}
