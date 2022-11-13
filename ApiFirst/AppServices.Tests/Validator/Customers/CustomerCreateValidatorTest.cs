using AppModels.AppModels.Customers;
using AppServices.Validator.Customers;
using FluentAssertions;
using FluentValidation.TestHelper;
using System;
using Xunit;

namespace AppServices.Tests.Validator.Customers
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
            var createCustomer = new CreateCustomer(fullName: "João Pedro", email: "aaaaaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaaaaa@g",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente de Moraes", number: 123);

            var result = validatorCreate.Validate(createCustomer);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_FullName_IsNull()
        {
            var createCustomer = new CreateCustomer(fullName: null, email: "aaaaaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaaaaa@g",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente De Moraes", number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.FullName);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_FullName_Has_Less_Than_Five_Chars()
        {
            var createCustomer = new CreateCustomer(fullName: "Ana", email: "aaaaaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaaaaa@g",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente De Moraes", number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.FullName);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_FullName_Has_More_Than_Fifty_Chars()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", email: "aaaaaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaaaaa@g",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente De Moraes", number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.FullName);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Email_Isnt_EmailAddress()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaa", email: "aaaaaaaaaaaaag", emailConfirmation: "aaaaaaaaaaaaaaag",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente De Moraes", number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Email_Isnt_Equal_To_EmailConfirmation()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaa", email: "oooooooooooooooog", emailConfirmation: "aaaaaaaaaaaaaaag",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente De Moraes", number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Email_Has_Less_Than_Twelve_Chars()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaa", email: "aaa@g", emailConfirmation: "aaa@g",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente De Moraes", number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Email_Has_More_Than_Fifty_Chars()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaa", email: "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa@g",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente De Moraes", number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Email_Is_Empty()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaa", email: "", emailConfirmation: "",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente De Moraes", number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Cpf_Is_Empty()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaa", email: "aaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaa@g",
                cpf: "", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente De Moraes", number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Cpf);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Cpf_Has_Not_Eleven_Chars()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaa", email: "aaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaa@g",
                cpf: "42713070848888", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente De Moraes", number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Cpf);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Cpf_Isnt_A_ValidCpf()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaa", email: "aaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaa@g",
                cpf: "12345678901", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente De Moraes", number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Cpf);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Cellphone_Is_Empty()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaa", email: "aaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaa@g",
                cpf: "42713070848", cellphone: "",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente De Moraes", number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Cellphone);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Cellphone_Has_Less_Than_Ten_Chars()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaa", email: "aaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaa@g",
                cpf: "42713070848", cellphone: "123456789",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente De Moraes", number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Cellphone);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_DateOfBirth_Is_Empty()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaa", email: "aaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaa@g",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: new DateTime(),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente De Moraes", number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.DateOfBirth);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_DateOfBirth_Is_Less_Than_Adult_Age()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaa", email: "aaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaa@g",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: DateTime.Now,
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente De Moraes", number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.DateOfBirth);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_DateOfBirth_Is_Less_Than_Min_Value()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaa", email: "aaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaa@g",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: DateTime.MinValue,
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente De Moraes", number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.DateOfBirth);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Country_Is_Empty()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaa", email: "aaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaa@g",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente De Moraes", number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Country);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Country_Has_Less_Than_Three_Chars()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaa", email: "aaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaa@g",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "BR",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente De Moraes", number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Country);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Country_Has_More_Than_Ninety_Chars()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaa", email: "aaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaa@g",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente De Moraes", number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Country);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Country_Hasnt_FirstLetterUpperCase()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaa", email: "aaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaa@g",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente De Moraes", number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Country);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_City_Is_Empty()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaa", email: "aaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaa@g",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "", postalCode: "89035360",
                address: "Rua Prudente De Moraes", number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.City);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_City_Has_Less_Than_Three_Chars()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaa", email: "aaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaa@g",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Sp", postalCode: "89035360",
                address: "Rua Prudente De Moraes", number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.City);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_City_Has_More_Than_One_Hundred_And_Ninety_Chars()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaa", email: "aaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaa@g",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "AaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaAaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                postalCode: "89035360",
                address: "Rua Prudente De Moraes", number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.City);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_City_Hasnt_FirstLetterUpperCase()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaa", email: "aaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaa@g",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "blumenau",
                postalCode: "89035360",
                address: "Rua Prudente De Moraes", number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.City);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Postalcode_Is_Empty()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaa", email: "aaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaa@g",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau",
                postalCode: "",
                address: "Rua Prudente De Moraes", number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.PostalCode);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Postalcode_Hasnt_Eight_Chars()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaa", email: "aaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaa@g",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau",
                postalCode: "89035",
                address: "Rua Prudente De Moraes", number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.PostalCode);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Address_Is_Empty()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaa", email: "aaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaa@g",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau",
                postalCode: "89035360",
                address: "", number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Address_Has_Less_Than_Three_Chars()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaa", email: "aaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaa@g",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau",
                postalCode: "89035360",
                address: "Lá", number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Address_Has_More_Than_Two_Hundred_Chars()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaa", email: "aaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaa@g",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau",
                postalCode: "89035360",
                address: "AaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaAaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Address_Hasnt_FirstLetterUpperCase()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaa", email: "aaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaa@g",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau",
                postalCode: "89035360",
                address: "rua prudente de moraes",
                number: 123);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Number_Is_Empty()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaa", email: "aaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaa@g",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau",
                postalCode: "89035360",
                address: "Rua Prudente De Moraes",
                number: int.MinValue);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Number);
        }

        [Fact]
        public void Should_Not_Validate_CreateCustomer_When_Number_Is_Lesser_Than_One()
        {
            var createCustomer = new CreateCustomer(fullName: "Anaaaa", email: "aaaaaaaaaa@g", emailConfirmation: "aaaaaaaaaa@g",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau",
                postalCode: "89035360",
                address: "Rua Prudente De Moraes",
                number: -2);

            var result = validatorCreate.TestValidate(createCustomer);

            result.ShouldHaveValidationErrorFor(x => x.Number);
        }

        [Fact]
        public void Should_Validate_UpdateCustomer_Sucessfully()
        {
            var updateCustomer = new UpdateCustomer(fullName: "João Pedro", email: "aaaaaaaaaaaaa@g",
                cpf: "42713070848", cellphone: "47991541506",
                dateOfBirth: new DateTime(year: 2002, month: 2, day: 2, hour: 14, minute: 22, second: 2),
                emailSms: true, whatsapp: true, country: "Brazil",
                city: "Blumenau", postalCode: "89035360",
                address: "Rua Prudente de Moraes", number: 123);

            var result = validatorUpdate.Validate(updateCustomer);

            result.IsValid.Should().BeTrue();
        }
    }
}
