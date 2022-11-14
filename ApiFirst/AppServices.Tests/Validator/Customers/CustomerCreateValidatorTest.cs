using ApiFirst.Tests.Fixtures.AppServices.Customer;
using AppServices.Validator.Customers;
using FluentValidation.TestHelper;
using System;
using Xunit;

namespace ApiFirst.Tests.Validator.Customers
{
    public class CustomerCreateValidatorTest
    {
        public readonly CustomerCreateValidator validatorCreate;

        public CustomerCreateValidatorTest()
        {
            validatorCreate = new CustomerCreateValidator();
        }

        [Fact]
        public void Should_Validate_CreateCustomer_Sucessfully()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_FullName_IsNull()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.FullName = null;

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.FullName);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_FullName_Has_Less_Than_Five_Chars()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.FullName = "Ana";

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.FullName);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_FullName_Has_More_Than_Fifty_Chars()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.FullName = "Anaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.FullName);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Email_Isnt_EmailAddress()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.Email = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Email_Isnt_Equal_To_EmailConfirmation()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.Email = "aaaaaaaaaaaaa@email";
            createCustomer.EmailConfirmation = "oooooooooooooo@email";

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Email_Has_Less_Than_Twelve_Chars()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.Email = "aa@email";

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Email_Has_More_Than_Fifty_Chars()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.Email = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa@email";

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Email_Is_Empty()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.Email = "";

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Cpf_Is_Empty()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.Cpf = "";

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Cpf);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Cpf_Has_Not_Eleven_Chars()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.Cpf = "1234567";

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Cpf);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Cpf_Isnt_A_ValidCpf()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.Cpf = "11111111111";

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Cpf);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Cellphone_Is_Empty()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.Cellphone = "";

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Cellphone);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Cellphone_Has_Less_Than_Ten_Chars()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.Cellphone = "123456789";

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Cellphone);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_DateOfBirth_Is_Empty()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.DateOfBirth = new DateTime();

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.DateOfBirth);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_DateOfBirth_Is_Less_Than_Adult_Age()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.DateOfBirth = DateTime.Now;

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.DateOfBirth);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_DateOfBirth_Is_Less_Than_Min_Value()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.DateOfBirth = DateTime.MinValue;

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.DateOfBirth);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Country_Is_Empty()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.Country = "";

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Country);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Country_Has_Less_Than_Three_Chars()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.Country = "BR";


            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Country);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Country_Has_More_Than_Ninety_Chars()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.Country = "Aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Country);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Country_Hasnt_FirstLetterUpperCase()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.Country = "brazil";

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Country);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_City_Is_Empty()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.City = "";


            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.City);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_City_Has_Less_Than_Three_Chars()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.City = "Sp";


            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.City);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_City_Has_More_Than_One_Hundred_And_Ninety_Chars()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.City = "AaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaAaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.City);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_City_Hasnt_FirstLetterUpperCase()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.City = "blumenau";

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.City);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Postalcode_Is_Empty()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.PostalCode = "";

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.PostalCode);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Postalcode_Has_More_Than_Nine_Chars()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.PostalCode = "890351237859";

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.PostalCode);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Address_Is_Empty()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.Address = "";

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Address_Has_Less_Than_Three_Chars()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.Address = "Lá";

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Address_Has_More_Than_Two_Hundred_Chars()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.Address = "AaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaAaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Number_Is_Empty()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.Number = int.MinValue;

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Number);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Number_Is_Lesser_Than_One()
        {
            var createCustomer = CreateCustomerFixture.GenerateCreateCustomerFixture();
            createCustomer.Number = -2;

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Number);
        }
    }
}
