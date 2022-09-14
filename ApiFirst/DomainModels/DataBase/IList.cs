using DomainModels.Entities;

namespace DomainModels.DataBase
{
    public interface IList
    {

        private static List<Customer> CustomerList { get; set; }
        private static ListCustomer instance;

        public static ListCustomer GetInstance() { return null; }

    }
}
