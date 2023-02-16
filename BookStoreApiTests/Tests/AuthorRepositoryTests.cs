using AutoMapper;
using BookStoreApi.DbOperations;
using BookStoreApi.Models.Entities;
using BookStoreApi.Models.Repositories;
using BookStoreApiTests.TestSetup;
using FluentAssertions;

namespace BookStoreApiTests.Tests
{
    public class AuthorRepositoryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _bookStoreDbContext;
        private readonly IMapper _mapper;
        private readonly AuthorRepository _authorRepository;

        public AuthorRepositoryTests(CommonTestFixture testFixture)
        {
            _bookStoreDbContext = testFixture.BookStoreDbContext;
            _mapper = testFixture.Mapper;
            _authorRepository = new AuthorRepository(_bookStoreDbContext, _mapper);
        }
        // Worst Case Scenario's
        /*******************************************************************************************************/
        [Fact]
        public void WhenAlreadyExistAuthorGiven_InvalidOperationException_ShouldBeReturn()
        {
            Author authorToCreate = new()
            {
                AuthorName = "WhenAlreadyExistAuthorGiven_InvalidOperationException_ShouldBeReturn",
                AuthorSurname = "WhenAlreadyExistAuthorGiven_InvalidOperationException_ShouldBeReturnt",
                AuthorBirthday = new DateTime(1920, 10, 8)
            };
            _bookStoreDbContext.Add(authorToCreate);
            _bookStoreDbContext.SaveChanges();

            FluentActions
                .Invoking(() => _authorRepository.AddAuthor(_mapper.Map<AuthorModel>(authorToCreate)))
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar zaten mevcut!");
        }

        [Fact]
        public void WhenNonExistingAuthorIdGivenToDelete_InvalidOperationException_ShouldBeReturn()
        {
            int authorId = 0;

            FluentActions
                .Invoking(() => _authorRepository.DeleteAuthorById(authorId))
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar mevcut değil!");

        }
        [Fact]
        public void WhenWithExistingBookAuthorIdGivenToDelete_InvalidOperationException_ShouldBeReturn()
        {
            int authorId = 1;

            FluentActions
                .Invoking(() => _authorRepository.DeleteAuthorById(authorId))
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazarın aktif kitabı var!");

        }
        [Fact]
        public void WhenWithExistingBookAuthorIdGivenToUpdate_InvalidOperationException_ShouldBeReturn()
        {
            int authorId = 0;
            Author authorToUpdate = new()
            {
                AuthorName = " WhenWithExistingBookAuthorIdGivenToUpdate_InvalidOperationException_ShouldBeReturn",
                AuthorSurname = " WhenWithExistingBookAuthorIdGivenToUpdate_InvalidOperationException_ShouldBeReturn",
                AuthorBirthday = new DateTime(1920, 10, 8)
            };
            FluentActions
                .Invoking(() => _authorRepository.UpdateAuthorById(authorId, _mapper.Map<AuthorModel>(authorToUpdate)))
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar mevcut değil!");

        }
        [Fact]
        public void WhenWithExistingBookAuthorIdGivenToGet_InvalidOperationException_ShouldBeReturn()
        {
            int authorId = 0;

            FluentActions
                .Invoking(() => _authorRepository.GetAuthorById(authorId))
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar mevcut değil!");
        }
        // Happy Path
        /*******************************************************************************************************/
        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            Author authorToCreate = new()
            {
                AuthorName = "WhenValidInputsAreGiven_Author_ShouldBeCreated",
                AuthorSurname = "WhenValidInputsAreGiven_Author_ShouldBeCreated",
                AuthorBirthday = new DateTime(1920, 10, 8)
            };

            FluentActions
                .Invoking(() => _authorRepository.AddAuthor(_mapper.Map<AuthorModel>(authorToCreate))).Invoke();

            var createdAuthor = _bookStoreDbContext.Authors.SingleOrDefault(a => a.AuthorName == authorToCreate.AuthorName);

            createdAuthor.Should().NotBeNull();
            createdAuthor.AuthorSurname.Should().Be(authorToCreate.AuthorSurname);
            createdAuthor.AuthorBirthday.Should().Be(authorToCreate.AuthorBirthday);
        }
        [Fact]
        public void WhenValidAuthorIdAreGivenToDelete_Author_ShouldBeDeleted()
        {
            Author authorToDelete = new()
            {
                AuthorName = "WhenValidAuthorIdAreGivenToDelete_Author_ShouldBeCreated",
                AuthorSurname = "WhenValidAuthorIdAreGivenToDelete_Author_ShouldBeCreated",
                AuthorBirthday = new DateTime(1920, 10, 8)
            };
            _bookStoreDbContext.Authors.Add(authorToDelete);
            _bookStoreDbContext.SaveChanges();

            FluentActions
                .Invoking(() => _authorRepository.DeleteAuthorById(authorToDelete.AuthorId)).Invoke();

            var deletedAuthor = _bookStoreDbContext.Authors.SingleOrDefault(a => a.AuthorId == authorToDelete.AuthorId);

            deletedAuthor.Should().BeNull();
        }
        [Fact]
        public void WhenValidAuthorIdAreGivenToUpdate_Author_ShouldBeUpdated()
        {
            Author authorToUpdate = new()
            {
                AuthorName = "WhenValidAuthorIdAreGivenToUpdate_Author_ShouldBeUpdated",
                AuthorSurname = "WhenValidAuthorIdAreGivenToUpdate_Author_ShouldBeUpdated",
                AuthorBirthday = new DateTime(1920, 10, 8)
            };
            _bookStoreDbContext.Authors.Add(authorToUpdate);
            _bookStoreDbContext.SaveChanges();
            Author author = new()
            {
                AuthorName = "WhenValidAuthorIdAreGivenToUpdate_Author_ShouldBeUpdated2",
                AuthorSurname = "WhenValidAuthorIdAreGivenToUpdate_Author_ShouldBeUpdated2",
                AuthorBirthday = new DateTime(1920, 10, 7)
            };

            FluentActions
                .Invoking(() => _authorRepository.UpdateAuthorById(authorToUpdate.AuthorId, _mapper.Map<AuthorModel>(author))).Invoke();

            var updatedAuthor = _bookStoreDbContext.Authors.SingleOrDefault(a => a.AuthorId == authorToUpdate.AuthorId);

            updatedAuthor.AuthorName.Should().Be(author.AuthorName);
            updatedAuthor.AuthorSurname.Should().Be(author.AuthorSurname);
            updatedAuthor.AuthorBirthday.Should().Be(author.AuthorBirthday);
        }

        [Fact]
        public void WhenValidAuthorIdAreGivenToGet_AuthorModel_ShouldBeReturn()
        {
            Author authorToGet = new()
            {
                AuthorName = "WhenValidAuthorIdAreGivenToUpdate_Author_ShouldBeUpdated",
                AuthorSurname = "WhenValidAuthorIdAreGivenToUpdate_Author_ShouldBeUpdated",
                AuthorBirthday = new DateTime(1920, 10, 8)
            };

            _bookStoreDbContext.Authors.Add(authorToGet);
            _bookStoreDbContext.SaveChanges();

            FluentActions
                .Invoking(() => _authorRepository.GetAuthorById(authorToGet.AuthorId)).Invoke()
                .Should().BeOfType<AuthorModel>().And.Subject.Should().NotBeNull();

        }
    }
}
