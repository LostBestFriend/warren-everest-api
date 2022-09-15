using DomainModels.DataBase;
using DomainModels.Models;
using DomainServices.Interfaces;

namespace DomainServices.Repositories
{
    public class RepositoryList : ICustomerRepository
    {
        public int Create(Customer model)
        {
            model.Cpf = model.Cpf.Trim().Replace(".", "").Replace("-", "");
            int response = 409;

            ListCustomer list = ListCustomer.GetInstance();

            model.Id = list.GetCustomerList().Count;


            if (!list.GetCustomerList().Any())
            {
                model.Id = 0;
                GetAll().Add(model);
                return 201;
            }

            foreach (Customer item in list.GetCustomerList())
            {
                if (model.Cpf != item.Cpf && model.Email != item.Email)
                {
                    GetAll().Add(model);
                    response = 201;
                }
                return response;
            }

            return 400;
        }

        public int DeleteFromList(string cpf, string email)
        {
            cpf = cpf.Trim().Replace(".", "").Replace("-", "");

            if (!GetAll().Any())
            {
                return 400;
            }

            foreach (Customer item in GetAll())
            {
                if (cpf == item.Cpf && email == item.Email)
                {
                    GetAll().Remove(item);
                    return 200;
                }
            }
            return 404;
        }
        public int Delete(int id)
        {

            if (!GetAll().Any())
            {
                return 400;
            }

            if (GetAll().Count() >= id)
            {
                GetAll().Remove(GetAll().ElementAt(id));
                return 200;
            }

            return 404;
        }

        public List<Customer> GetAll()
        {
            ListCustomer list = ListCustomer.GetInstance();
            return list.GetCustomerList();
        }

        public Customer GetSpecificFromList(string cpf, string email)
        {
            cpf = cpf.Trim().Replace(".", "").Replace("-", "");

            if (!GetAll().Any())
            {
                return null;
            }

            foreach (Customer item in GetAll())
            {
                if (item.Cpf == cpf && item.Email == email)
                {
                    return item;
                }
                return null;
            }
            return null;
        }

        public int Update(Customer model)
        {
            model.Cpf = model.Cpf.Trim().Replace(".", "").Replace("-", "");

            if (!GetAll().Any())
            {
                return 400;
            }

            foreach (Customer item in GetAll())
            {
                if (model.Cpf == item.Cpf && model.Email == item.Email)
                {
                    item.Address = model.Address;
                    item.City = model.City;
                    item.Cpf = model.Cpf;
                    item.EmailSms = model.EmailSms;
                    item.Cellphone = model.Cellphone;
                    item.Country = model.Country;
                    item.Email = model.Email;
                    item.DateOfBirth = model.DateOfBirth;
                    item.EmailConfirmation = model.EmailConfirmation;
                    item.PostalCode = model.PostalCode;
                    item.Number = model.Number;
                    item.Whatsapp = model.Whatsapp;
                    item.FullName = model.FullName;
                    return 200;
                }
                return 404;
            }
            return 400;
        }

        public Customer GetSpecific(int id)
        {
            if (!GetAll().Any())
            {
                return null;
            }
            if (GetAll().Count() >= id)
            {
                return GetAll().ElementAt(id);
            }
            return null;
        }

        public int Modify(Customer model)
        {
            model.Cpf = model.Cpf.Trim().Replace(".", "").Replace("-", "");

            if (!GetAll().Any())
            {
                return 400;
            }

            foreach (Customer item in GetAll())
            {
                if (model.Cpf == item.Cpf && model.Email == item.Email)
                {
                    item.Address = model.Address;
                    item.City = model.City;
                    item.Cpf = model.Cpf;
                    item.EmailSms = model.EmailSms;
                    item.Cellphone = model.Cellphone;
                    item.Country = model.Country;
                    item.Email = model.Email;
                    item.DateOfBirth = model.DateOfBirth;
                    item.EmailConfirmation = model.EmailConfirmation;
                    item.PostalCode = model.PostalCode;
                    item.Number = model.Number;
                    item.Whatsapp = model.Whatsapp;
                    item.FullName = model.FullName;
                    return 200;
                }
                return 404;
            }
            return 400;
        }
    }
}
