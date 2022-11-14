using ApiFirst.Tests.Fixtures.AppServices.Customer;
using AppServices.Validator.Customers;
using FluentAssertions;
using FluentValidation.TestHelper;
using System;
using Xunit;

namespace ApiFirst.Tests.Validator.Customers
{
    public class CustomerUpdateValidatorTest
    {
        public readonly CustomerUpdateValidator validatorUpdate;

        public CustomerUpdateValidatorTest()
        {
            validatorUpdate = new();
        }

        [Fact]
        public void Should_Validate_UpdateCustomer_Sucessfully()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();

            var result = validatorUpdate.Validate(updateCustomer);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_FullName_IsNull()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.FullName = null;

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.FullName);
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_FullName_Has_Less_Than_Five_Chars()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.FullName = "Ana";

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.FullName);
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_FullName_Has_More_Than_Fifty_Chars()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.FullName = "Anaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.FullName);
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_Email_Isnt_EmailAddress()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.Email = "aaaaaaaaaaaaaaaaag";

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_Email_Has_Less_Than_Twelve_Chars()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.Email = "aaa@g";

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_Email_Has_More_Than_Fifty_Chars()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.Email = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa@gmail";

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_Email_Is_Empty()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.Email = "";

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_Cpf_Is_Empty()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.Cpf = "";

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Cpf);
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_Cpf_Has_Not_Eleven_Chars()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.Cpf = "427130708488888";

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Cpf);
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_Cpf_Isnt_A_ValidCpf()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.Cpf = "11111111111";

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Cpf);
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_Cellphone_Is_Empty()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.Cellphone = "";

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Cellphone);
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_Cellphone_Has_Less_Than_Ten_Chars()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.Cellphone = "123456789";

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Cellphone);
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_DateOfBirth_Is_Empty()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.DateOfBirth = new DateTime();

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.DateOfBirth);
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_DateOfBirth_Is_Less_Than_Adult_Age()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.DateOfBirth = DateTime.Now;

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.DateOfBirth);
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_DateOfBirth_Is_Less_Than_Min_Value()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.DateOfBirth = DateTime.MinValue;

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.DateOfBirth);
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_Country_Is_Empty()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.Country = "";

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Country);
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_Country_Has_Less_Than_Three_Chars()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.Country = "BR";

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Country);
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_Country_Has_More_Than_Ninety_Chars()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.Country = "Aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Country);
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_Country_Hasnt_FirstLetterUpperCase()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.Country = "brazil";

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Country);
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_City_Is_Empty()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.City = "";

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.City);
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_City_Has_Less_Than_Three_Chars()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.City = "Sp";

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.City);
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_City_Has_More_Than_One_Hundred_And_Ninety_Chars()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.City = "AaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaAaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.City);
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_City_Hasnt_FirstLetterUpperCase()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.City = "blumenau";

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.City);
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_Postalcode_Is_Empty()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.PostalCode = "";

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.PostalCode);
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_Postalcode_Hasnt_Eight_Chars()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.PostalCode = "8903512345678";

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.PostalCode);
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_Address_Is_Empty()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.Address = "";

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_Address_Has_Less_Than_Three_Chars()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.Address = "Lá";

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_Address_Has_More_Than_Two_Hundred_Chars()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.Address = "AaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaAaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_Number_Is_Empty()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.Number = int.MinValue;

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Number);
        }

        [Fact]
        public void Should_Not_Validate_UpdateCustomer_When_Number_Is_Lesser_Than_One()
        {
            var updateCustomer = UpdateCustomerFixture.GenerateUpdateCustomerFixture();
            updateCustomer.Number = -2;

            var result = validatorUpdate.TestValidate(updateCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Number);
        }
    }
}
