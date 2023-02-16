using AutoMapper;
using BookStoreApi.Models.Repositories;
using BookStoreApi.Models.Validators;
using BookStoreApiTests.TestSetup;
using FluentAssertions;

namespace BookStoreApiTests.Tests
{
    public class GenreValitdatorsTests
    {

        [Theory]
        [InlineData("")]
        public void WhenInvalidInputsAreGiven_CreateGenreValidator_ShouldBeReturnErrors(string genreName)
        {
            GenreModel genreModel = new() { GenreName = genreName };

            GenreModelValidator validator = new();

            var results = validator.Validate(genreModel);

            results.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
