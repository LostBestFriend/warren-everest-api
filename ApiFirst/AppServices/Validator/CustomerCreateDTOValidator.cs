using AppModels.MapperModels;
using FluentValidation;
using System.Globalization;

namespace AppServices.Validation
{
    public class CustomerCreateDTOValidator : AbstractValidator<CustomerCreateDTO>
    {
        public CustomerCreateDTOValidator()
        {
            RuleFor(x => x.Cpf)
                .Must(BeAValidCpf)
                .NotEmpty().NotEqual(" ").Length(11)
                .WithMessage("Please enter a valid CPF");

            RuleFor(x => x.Email)
                .EmailAddress().Equal(x => x.EmailConfirmation).NotEqual(" ").
                MinimumLength(3).NotEmpty();

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().GreaterThan(DateTime.MinValue)
                .WithMessage("A valid DateOfBirth is required");

            RuleFor(x => x.FullName)
                .NotEmpty().NotEqual(" ").
                Must(IsAName).MinimumLength(2).
                WithMessage("FullName is required");

            RuleFor(x => x.Cellphone)
                .NotEmpty().NotEqual(" ").Length(13).
                WithMessage("Cellphone is required");

            RuleFor(x => x.EmailSms)
                .NotEmpty().
                WithMessage("EmailSms is required");

            RuleFor(x => x.Whatsapp)
                .NotEmpty().
                WithMessage("WhatsApp is required");

            RuleFor(x => x.Country)
                .NotEmpty().NotEqual(" ").MinimumLength(3).
                Must(FirstUpperCase).
                WithMessage("Country is required");

            RuleFor(x => x.City)
                .NotEmpty().NotEqual(" ").MinimumLength(3).
                Must(FirstUpperCase).
                WithMessage("City is required");

            RuleFor(x => x.PostalCode)
                .NotEmpty().NotEqual(" ").Length(8).
                WithMessage("PostalCode is required");

            RuleFor(x => x.Address)
                .NotEmpty().NotEqual(" ").MinimumLength(3).
                Must(FirstUpperCase).
                WithMessage("Address is required");

            RuleFor(x => x.Number)
                .NotEmpty().GreaterThanOrEqualTo(0).
                WithMessage("Number is required");

            bool IsAName(string fullname)
            {
                if (fullname == CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fullname.ToLower()))
                {
                    return true;
                }
                return false;
            }

            bool FirstUpperCase(string input)
            {

                input = input.Trim();
                char chars = input[0];
                if (Char.IsUpper(chars))
                {
                    return true;
                }
                return false;
            }

            bool BeAValidCpf(string cpf)
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
                if (resto < 2)
                {
                    resto = 0;
                }
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
                if (resto < 2)
                {
                    resto = 0;
                }
                else
                {
                    resto = 11 - resto;
                }

                digito = digito + resto.ToString();

                return cpf.EndsWith(digito);
            }
        }
    }
}
