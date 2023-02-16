using AutoMapper;
using BookStoreApi.DbOperations;
using BookStoreApi.Models.Entities;
using BookStoreApi.Models.Repositories;
using BookStoreApi.Models.Validators;
using BookStoreApiTests.TestSetup;
using FluentAssertions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApiTests.Tests
{

    public class BookValidatorsTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMapper _mapper;

        public BookValidatorsTests(CommonTestFixture testFixture)
        {
            _mapper = testFixture.Mapper;
        }

        [Theory]
        [InlineData("Lord Of The Rings", 0, 0)]
        [InlineData("Lord Of The Rings", 0, 1)]
        [InlineData("Lord Of The Rings", 100, 0)]
        [InlineData("", 0, 0)]
        [InlineData("", 100, 1)]
        [InlineData("", 0, 1)]
        [InlineData("Lor", 100, 1)]
        [InlineData("Lor", 100, 0)]
        [InlineData(" ", 100, 1)]
        public void WhenInvalidInputsAreGiven_CreateBookValidator_ShouldBeReturnErrors(string title, int pageCount, int genreId)
        {
            //Arrange
            CreateBookModel createBookModel = new()
            {
                BookTitle = title,
                BookPageCount = pageCount,
                BookPublishDate = DateTime.Now.Date,
                GenreId = genreId
            };
            CreateBookModelValidator validator = new();
            //Act
            var result = validator.Validate(createBookModel);
            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_CreateBookValidator_ShouldReturnError()
        {
            CreateBookModel createBookModel = new()
            {
                BookTitle = "Lord Of The Rings",
                BookPageCount = 100,
                BookPublishDate = DateTime.Now.Date,
                GenreId = 1
            };
            CreateBookModelValidator validator = new();

            var result = validator.Validate(createBookModel);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("Lord Of The Rings", 0, 0)]
        [InlineData("Lord Of The Rings", 0, 1)]
        [InlineData("Lord Of The Rings", 100, 0)]
        [InlineData("", 0, 0)]
        [InlineData("", 100, 1)]
        [InlineData("", 0, 1)]
        [InlineData("Lor", 100, 1)]
        [InlineData("Lor", 100, 0)]
        [InlineData(" ", 100, 1)]
        public void WhenInvalidInputsAreGiven_BookValidator_ShouldBeReturnErrors(string title, int pageCount, int genreId)
        {
            //Arrange
            Book book = new()
            {
                BookTitle = title,
                BookPageCount = pageCount,
                BookPublishDate = DateTime.Now.Date,
                GenreId = genreId
            };
            BookModel bookModel = _mapper.Map<BookModel>(book);
            BookModelValidator validator = new();
            //Act
            var result = validator.Validate(bookModel);
            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_BookValidator_ShouldReturnError()
        {
            Book book = new()
            {
                BookTitle = "Lord Of The Rings",
                BookPageCount = 100,
                BookPublishDate = DateTime.Now.Date,
                GenreId = 1
            };
            BookModel bookModel = _mapper.Map<BookModel>(book);
            BookModelValidator validator = new();

            var result = validator.Validate(bookModel);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("Lord Of The Rings", 0, 0)]
        [InlineData("Lord Of The Rings", 0, 1)]
        [InlineData("Lord Of The Rings", 100, 0)]
        [InlineData("", 0, 0)]
        [InlineData("", 100, 1)]
        [InlineData("", 0, 1)]
        [InlineData("Lor", 100, 1)]
        [InlineData("Lor", 100, 0)]
        [InlineData(" ", 100, 1)]
        public void WhenInvalidInputsAreGiven_UpdateBookValidator_ShouldBeReturnErrors(string title, int pageCount, int genreId)
        {
            //Arrange
            UpdateBookModel updateBookModel = new()
            {
                BookTitle = title,
                BookPageCount = pageCount,
                BookPublishDate = DateTime.Now.Date,
                GenreId = genreId
            };
            UpdateBookModelValidator validator = new();
            //Act
            var result = validator.Validate(updateBookModel);
            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_UpdateBookValidator_ShouldReturnError()
        {
            UpdateBookModel updateBookModel = new()
            {
                BookTitle = "Lord Of The Rings",
                BookPageCount = 100,
                BookPublishDate = DateTime.Now.Date,
                GenreId = 1
            };
            UpdateBookModelValidator validator = new();

            var result = validator.Validate(updateBookModel);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
