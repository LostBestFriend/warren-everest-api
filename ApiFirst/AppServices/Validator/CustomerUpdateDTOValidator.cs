using AppModels.MapperModels;
using FluentValidation;

namespace AppServices.Validation
{
    public class CustomerUpdateDTOValidator : AbstractValidator<CustomerUpdateDTO>
    {
        public CustomerUpdateDTOValidator()
        {
            RuleFor(x => x.Cpf).
                NotEmpty().
                NotEqual(" ").
                Length(11).
                Must(IsValidCpf).WithMessage("Please enter a valid CPF, this CPF is not valid");

            RuleFor(x => x.Email).
                EmailAddress().
                NotEqual(" ").
                MinimumLength(3).
                MaximumLength(50).
                NotEmpty();

            RuleFor(x => x.DateOfBirth).
                GreaterThan(DateTime.MinValue).
                LessThanOrEqualTo(DateTime.Now.AddYears(-18)).
                NotEmpty();

            RuleFor(x => x.FullName).
                NotEmpty().NotEqual(" ").
                MinimumLength(2).
                MaximumLength(50);

            RuleFor(x => x.Cellphone).
                NotEmpty().
                NotEqual(" ").
                Length(13);

            RuleFor(x => x.EmailSms).
                NotNull();

            RuleFor(x => x.Whatsapp).
                NotNull();

            RuleFor(x => x.Country).
                NotEmpty().
                NotEqual(" ").
                MinimumLength(3).
                MaximumLength(90).
                Must(FirstLetterIsUpperCase).WithMessage("Please enter a valid Country name");

            RuleFor(x => x.City).
                NotEmpty().
                NotEqual(" ").
                MinimumLength(3).
                MaximumLength(190).
                Must(FirstLetterIsUpperCase).WithMessage("Please enter a valid City name");

            RuleFor(x => x.PostalCode).
                NotEmpty().
                NotEqual(" ").
                Length(8);

            RuleFor(x => x.Address).
                NotEmpty().
                NotEqual(" ").
                MinimumLength(3).
                MaximumLength(200).
                Must(FirstLetterIsUpperCase).WithMessage("Please enter a valid Address");

            RuleFor(x => x.Number).
                NotEmpty().
                GreaterThanOrEqualTo(1);

            bool FirstLetterIsUpperCase(string input)
            {
                input = input.Trim();
                var chars = input.First();
                return Char.IsUpper(chars);
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
