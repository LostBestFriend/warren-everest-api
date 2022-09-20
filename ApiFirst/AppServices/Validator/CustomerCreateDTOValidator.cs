using AppModels.MapperModels;
using FluentValidation;

namespace AppServices.Validator
{
    public class CustomerCreateDTOValidator : AbstractValidator<CustomerCreateDTO>
    {
        public CustomerCreateDTOValidator()
        {
            RuleFor(x => x.Cpf).NotEmpty().NotEqual(" ").Length(11).Must(BeAValidCpf).WithMessage("Please enter a valid CPF, this CPF is not valid");

            RuleFor(x => x.Email).EmailAddress().Equal(x => x.EmailConfirmation).NotEqual(" ").MinimumLength(3).MaximumLength(60).NotEmpty();

            RuleFor(x => x.DateOfBirth).NotEmpty().GreaterThan(DateTime.MinValue);

            RuleFor(x => x.FullName).NotEmpty().NotEqual(" ").MinimumLength(2).MaximumLength(250);

            RuleFor(x => x.Cellphone).NotEmpty().NotEqual(" ").Length(20);

            RuleFor(x => x.EmailSms).NotEmpty();

            RuleFor(x => x.Whatsapp).NotEmpty();

            RuleFor(x => x.Country).NotEmpty().NotEqual(" ").MinimumLength(3).MaximumLength(30).Must(FirstLetterUpperCase).WithMessage("Please enter a valid Country name");

            RuleFor(x => x.City).NotEmpty().NotEqual(" ").MinimumLength(3).MaximumLength(30).Must(FirstLetterUpperCase).WithMessage("Please enter a valid City name");

            RuleFor(x => x.PostalCode).NotEmpty().NotEqual(" ").Length(11);

            RuleFor(x => x.Address).NotEmpty().NotEqual(" ").MinimumLength(3).MaximumLength(60).Must(FirstLetterUpperCase).WithMessage("Please enter a valid Address");

            RuleFor(x => x.Number).NotEmpty().GreaterThanOrEqualTo(1);

            bool FirstLetterUpperCase(string input)
            {
                input = input.Trim();
                char chars = input[0];
                return Char.IsUpper(chars);
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
