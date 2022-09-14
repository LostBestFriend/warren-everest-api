﻿using DomainModels.Entities;

namespace DomainModels.DataBase
{
    public class ListCustomer : IList
    {
        private static List<Customer> CustomerList { get; set; } = new List<Customer>();
        private static ListCustomer Instance;

        public ListCustomer()
        {

        }
        public static ListCustomer GetInstance()
        {
            if (Instance == null)
            {
                Instance = new();
            }
            return Instance;
        }

        public List<Customer> GetCustomerList()
        {

            return CustomerList;
        }
    }
}
