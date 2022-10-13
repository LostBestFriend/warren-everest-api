using Infrastructure.CrossCutting.ExtensionMethods;
using System;

namespace DomainModels.Models
{
    public class Customer : BaseModel
    {
        public Customer(
            string fullName,
            string email,
            string cpf,
            string cellphone,
            DateTime dateOfBirth,
            bool emailSms,
            bool whatsapp,
            string country,
            string city,
            string postalCode,
            string address,
            int number
        )
        {
            FullName = fullName;
            Email = email;
            Cpf = cpf.FormatString();
            Cellphone = cellphone;
            DateOfBirth = dateOfBirth;
            EmailSms = emailSms;
            Whatsapp = whatsapp;
            Country = country;
            City = city;
            PostalCode = postalCode.FormatString();
            Address = address;
            Number = number;
        }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Cellphone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool EmailSms { get; set; }
        public bool Whatsapp { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public int Number { get; set; }
    }
}
