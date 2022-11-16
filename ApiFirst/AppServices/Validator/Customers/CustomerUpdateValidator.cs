using AppModels.AppModels.Customers;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Linq;

namespace AppServices.Validator.Customers
{
    public class CustomerUpdateValidator : AbstractValidator<UpdateCustomer>
    {
        public CustomerUpdateValidator()
        {
            RuleFor(x => x.Cpf)
                .NotEmpty()
                .Length(11)
                .Must(IsValidCpf)
                .WithMessage("Please enter a valid CPF, this CPF is not valid");

            RuleFor(x => x.Email)
                .EmailAddress(EmailValidationMode.Net4xRegex)
                .MinimumLength(12)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(x => x.DateOfBirth)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now.AddYears(-18))
                .GreaterThan(DateTime.MinValue);

            RuleFor(x => x.FullName)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(50);

            RuleFor(x => x.Cellphone)
                .NotEmpty()
                .MinimumLength(10);

            RuleFor(x => x.EmailSms)
                .NotNull();

            RuleFor(x => x.Whatsapp)
                .NotNull();

            RuleFor(x => x.Country)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(90)
                .Must(FirstLetterIsUpperCase)
                .WithMessage("Please enter a valid Country name");

            RuleFor(x => x.City)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(190)
                .Must(FirstLetterIsUpperCase)
                .WithMessage("Please enter a valid City name");

            RuleFor(x => x.PostalCode)
                .NotEmpty()
                .MaximumLength(9);

            RuleFor(x => x.Address)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(200);

            RuleFor(x => x.Number)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);

            bool FirstLetterIsUpperCase(string input)
            {
                if (input == null || input == "" || input == " ") return false;

                input = input.Trim();
                var chars = input.First();
                return char.IsUpper(chars);
            }

            bool IsValidCpf(string cpf)
            {

                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                string tempCpf;
                string digito;
                int soma;
                int resto;

                if (cpf.Length != 11) return false;
                if (cpf.All(x => x == cpf.First())) return false;

                tempCpf = cpf.Substring(0, 9);
                soma = 0;

                for (int i = 0; i < 9; i++)
                {
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                }

                resto = soma % 11;
                if (resto < 2) resto = 0;
                else
                {
                    resto = 11 - resto;
                }
                digito = resto.ToString();

                tempCpf = tempCpf + digito;

                soma = 0;
                for (int i = 0; i < 10; i++)
                {
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                }

                resto = soma % 11;
                if (resto < 2) resto = 0;
                else
                {
                    resto = 11 - resto;
                }

                digito += resto.ToString();

                return cpf.EndsWith(digito);
            }
        }
    }
}
