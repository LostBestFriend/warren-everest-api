namespace DomainModels.Models
{
    public class Customer : BaseModel
    {
        public string FullName;
        public string Email;
        public string EmailConfirmation;
        public string Cpf;
        public string Cellphone;
        public DateTime DateOfBirth;
        public bool EmailSms;
        public bool Whatsapp;
        public string Country;
        public string City;
        public string PostalCode;
        public string Address;
        public int Number;
        public Customer
        (
        string fullName,
        string email,
        string emailConfirmation,
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
            EmailConfirmation = emailConfirmation;
            Cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            Cellphone = cellphone;
            DateOfBirth = dateOfBirth;
            EmailSms = emailSms;
            Whatsapp = whatsapp;
            Country = country;
            City = city;
            PostalCode = postalCode;
            Address = address;
            Number = number;
        }
    }
}
