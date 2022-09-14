using AppServices.DTOs;
using FluentValidation;

namespace AppServices.Validation
{
    public class CustomersValidator : AbstractValidator<CustomerDTO>
    {
        public CustomersValidator()
        {
            RuleFor(x => x.Cpf.Trim().Replace(".", "").Replace("-", "")).MinimumLength(10).MaximumLength(14).Must(BeAValidCpf).
                NotNull().NotEmpty().WithMessage("Please enter a valid CPF");

            RuleFor(x => x.Email).EmailAddress().Equal(x => x.EmailConfirmation).NotNull().NotEmpty();

            RuleFor(x => x.DateOfBirth).NotEmpty().GreaterThan(DateTime.MinValue).WithMessage("This is required");
            RuleFor(x => x.FullName).NotEmpty().WithMessage("This is required");
            RuleFor(x => x.Cellphone).NotEmpty().WithMessage("This is required");
            RuleFor(x => x.EmailSms).NotEmpty().WithMessage("This is required");
            RuleFor(x => x.Whatsapp).NotEmpty().WithMessage("This is required");
            RuleFor(x => x.Country).NotEmpty().WithMessage("This is required");
            RuleFor(x => x.City).NotEmpty().WithMessage("This is required");
            RuleFor(x => x.PostalCode).NotEmpty().WithMessage("This is required");
            RuleFor(x => x.Address).NotEmpty().WithMessage("This is required");
            RuleFor(x => x.Number).NotEmpty().WithMessage("This is required");

            bool BeAValidCpf(string cpf)
            {

                int num1 = int.Parse(cpf[0].ToString());
                int num2 = int.Parse(cpf[1].ToString());
                int num3 = int.Parse(cpf[2].ToString());
                int num4 = int.Parse(cpf[3].ToString());
                int num5 = int.Parse(cpf[4].ToString());
                int num6 = int.Parse(cpf[5].ToString());
                int num7 = int.Parse(cpf[6].ToString());
                int num8 = int.Parse(cpf[7].ToString());
                int num9 = int.Parse(cpf[8].ToString());
                int num10 = int.Parse(cpf[9].ToString());
                int num11 = int.Parse(cpf[10].ToString());

                if (num1 == num2 && num2 == num3 && num3 == num4 && num4 == num5 && num5 == num6 && num6 == num7 && num7 == num8 &&
                    num8 == num9 && num9 == num10 && num10 == num11)
                {
                    return false;
                }
                else
                {
                    int sum1 = num1 * 10 + num2 * 9 + num3 * 8 + num4 * 7 + num5 * 6 + num6 * 5 + num7 * 4 + num8 * 3 + num9 * 2;

                    int resto1 = (sum1 * 10) % 11;

                    if (resto1 >= 10)
                    {
                        resto1 = 0;
                    }

                    int sum2 = num1 * 11 + num2 * 10 + num3 * 9 + num4 * 8 + num5 * 7 + num6 * 6 + num7 * 5 + num8 * 4 + num9 * 3 + num10 * 2;

                    int resto2 = (sum2 * 10) % 11;

                    if (resto2 >= 10)
                    {
                        resto2 = 0;
                    }
                    if (resto1 == num10 && resto2 == num11)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}
