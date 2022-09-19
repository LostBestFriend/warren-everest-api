namespace ApiFirst
{
    public class CustomerService : ICustomerService
    {
        private readonly IList<Customer> _customers;

        public CustomerService(IList<Customer> customers)
        {
            _customers = customers;
        }

        public bool Create(Customer model)
        {
            model.Id = _customers.LastOrDefault()?.Id + 1 ?? 1;

            if (Exists(model))
            {
                return false;
            }
            else
            {
                _customers.Add(model);
                return true;
            }
        }


        public bool Delete(int id)
        {
            var response = _customers.FirstOrDefault(customer => customer.Id == id);

            if (response is null) return false;

            int index = _customers.IndexOf(response);

            if (index == -1) return false;

            _customers.RemoveAt(index);
            return true;
        }

        public IList<Customer> GetAll()
        {
            return _customers;
        }

        public Customer? GetByCpf(string cpf)
        {
            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            return _customers.FirstOrDefault(customer => customer.Cpf == cpf);
        }

        public bool ExistsUpdate(long id, Customer model)
        {
            return _customers.Any(customer => (customer.Cpf == model.Cpf || customer.Email == model.Email) && customer.Id != id);
        }

        public bool Exists(Customer model)
        {
            return _customers.Any(customer => customer.Cpf == model.Cpf || customer.Email == model.Email);
        }

        public int Update(int id, Customer model)
        {
            var response = _customers.FirstOrDefault(customer => customer.Id == id);

            if (response is null) return -1;

            int index = _customers.IndexOf(response);

            if (!ExistsUpdate(id, model)) return 0;
            else
            {
                model.Id = _customers[index].Id;
                _customers[index] = model;
                return 1;
            }
        }

        public Customer? GetById(int id)
        {
            return _customers.FirstOrDefault(x => x.Id == id);
        }

        public int Modify(int id, string email)
        {
            Customer? model = _customers.FirstOrDefault(customer => customer.Id == id);
            if (model is null) return -1;

            else if (_customers.Any(customer => customer.Email == model.Email)) return 0;
            else
            {
                model.Id = id;
                model.Email = email;
                return 1;
            }
        }
    }
}
