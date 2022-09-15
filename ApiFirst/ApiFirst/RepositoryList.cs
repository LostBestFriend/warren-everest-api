using DomainModels.Models;
using DomainServices.Interfaces;

namespace DomainServices.Repositories
{
    public class RepositoryList : ICustomerRepository
    {
        private readonly List<Customer> List = new();

        public bool Create(Customer model)
        {
            model.Id = GetAll().LastOrDefault()?.Id + 1 ?? 0;

            bool exist = GetAll().Any(customer => customer.Cpf == model.Cpf || customer.Email == model.Email);
            if (exist)
            {
                return false;
            }
            else
            {
                GetAll().Add(model);
                return true;
            }
        }


        public bool Delete(int id)
        {
            int index = GetAll().FindIndex(customer => customer.Id == id);
            if (index == -1) return false;
            GetAll().RemoveAt(index);
            return true;
        }

        public List<Customer> GetAll()
        {
            return List;
        }

        public Customer? GetByCpf(string cpf)
        {
            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            return GetAll().FirstOrDefault(customer => customer.Cpf == cpf);
        }

        public int Update(int id, Customer model)
        {

            int index = GetAll().FindIndex(customer => customer.Id == id);
            if (index == -1) return -1;

            bool exist = GetAll().Any(customer => (customer.Cpf == model.Cpf || customer.Email == model.Email) && customer.Id != GetAll()[index].Id);

            if (exist) return 0;
            else
            {
                model.Id = GetAll()[index].Id;
                GetAll()[index] = model;
                return 1;
            }
        }

        public Customer? GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public int Modify(int id, Customer model)
        {
            int index = GetAll().FindIndex(customer => customer.Id == id);
            if (index == -1) return -1;

            bool exist = GetAll().Any(customer => (customer.Cpf == model.Cpf || customer.Email == model.Email) && customer.Id != GetAll()[index].Id);
            if (exist) return 0;
            else
            {
                model.Id = GetAll()[index].Id;

                if (GetAll()[index].FullName != model.FullName) GetAll()[index].FullName = model.FullName;

                if (GetAll()[index].Email != model.Email) GetAll()[index].Email = model.Email;

                if (GetAll()[index].EmailConfirmation != model.EmailConfirmation) GetAll()[index].EmailConfirmation = model.EmailConfirmation;

                if (GetAll()[index].Cpf != model.Cpf) GetAll()[index].Cpf = model.Cpf;

                if (GetAll()[index].Cellphone != model.Cellphone) GetAll()[index].Cellphone = model.Cellphone;

                if (GetAll()[index].DateOfBirth != model.DateOfBirth) GetAll()[index].DateOfBirth = model.DateOfBirth;

                if (GetAll()[index].EmailSms != model.EmailSms) GetAll()[index].EmailSms = model.EmailSms;

                if (GetAll()[index].Whatsapp != model.Whatsapp) GetAll()[index].Whatsapp = model.Whatsapp;

                if (GetAll()[index].Country != model.Country) GetAll()[index].Country = model.Country;

                if (GetAll()[index].City != model.City) GetAll()[index].City = model.City;

                if (GetAll()[index].PostalCode != model.PostalCode) GetAll()[index].PostalCode = model.PostalCode;

                if (GetAll()[index].Address != model.Address) GetAll()[index].Address = model.Address;

                if (GetAll()[index].Number != model.Number) GetAll()[index].Number = model.Number;

                return 1;
            }
        }
    }
}
