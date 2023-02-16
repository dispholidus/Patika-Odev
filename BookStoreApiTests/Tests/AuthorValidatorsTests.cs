using BookStoreApi.Models.Repositories;
using BookStoreApi.Models.Validators;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApiTests.Tests
{
    public class AuthorValidatorsTests
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("aasddf", "a")]
        [InlineData("as", "asdasdsa")]
        public void WhenInvalidInputsAreGiven_AuthorValidator_ShouldBeReturnErrors(string name, string surname)
        {
            AuthorModel authorModel = new()
            {
                AuthorName = name,
                AuthorSurname = surname,
                AuthorBirthday = new DateTime(1860, 7, 3)
            };
            AuthorModelValidator validator = new();

            var result = validator.Validate(authorModel);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_CreateBookValidator_ShouldReturnError()
        {
            AuthorModel authorModel = new()
            {
                AuthorName = "WhenDateTimeEqualNowIsGiven_CreateBookValidator_ShouldReturnError",
                AuthorSurname = "WhenDateTimeEqualNowIsGiven_CreateBookValidator_ShouldReturnError",
                AuthorBirthday = DateTime.Now.Date,
            };
            AuthorModelValidator validator = new();

            var result = validator.Validate(authorModel);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
