using AutoMapper;
using BookStoreApi.DbOperations;
using BookStoreApi.Models.Entities;
using BookStoreApi.Models.Repositories;
using BookStoreApiTests.TestSetup;
using FluentAssertions;

namespace BookStoreApiTests.Tests
{
    public class BookRepositoryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _bookStoreDbContext;
        private readonly IMapper _mapper;
        private readonly BookRepository _bookRepository;

        public BookRepositoryTests(CommonTestFixture testFixture)
        {
            _bookStoreDbContext = testFixture.BookStoreDbContext;
            _mapper = testFixture.Mapper;
            _bookRepository = new BookRepository(_bookStoreDbContext, _mapper);

        }

        // Worst Case Scenario's
        /*******************************************************************************************************/
        [Fact]
        public void WhenAlreadyExistBookTitleGiven_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange
            var book = new Book()
            {
                BookTitle = "WhenAlreadyExistBookTitleGiven_InvalidOperationException_ShouldBeReturn",
                BookPageCount = 100,
                BookPublishDate = new DateTime(1980, 01, 10),
                GenreId = 1
            };
            _bookStoreDbContext.Books.Add(book);
            _bookStoreDbContext.SaveChanges();


            //Act & Assert
            FluentActions
                .Invoking(() => _bookRepository.AddBook(_mapper.Map<CreateBookModel>(book)))
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut!");

        }

        [Fact]
        public void WhenNonExistingBookIdGivenToDelete_InvalidOperationException_ShouldBeReturn()
        {
            int bookId = 0;

            FluentActions
                .Invoking(() => _bookRepository.DeleteBookById(bookId))
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap mevcut deðil!");
        }

        [Fact]
        public void WhenNonExistingBookIdGivenToUpdate_InvalidOperationException_ShouldBeReturn()
        {
            int bookId = 0;
            var book = new Book()
            {
                BookTitle = "WhenNonExistingBookIdGivenToUpdate_InvalidOperationException_ShouldBeReturn",
                BookPageCount = 100,
                BookPublishDate = new DateTime(1980, 01, 10),
                GenreId = 1
            };

            FluentActions
                .Invoking(() => _bookRepository.UpdateBookById(bookId, _mapper.Map<UpdateBookModel>(book)))
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap mevcut deðil!");
        }

        [Fact]
        public void WhenNonExistingBookIdGivenToGet_InvalidOperationException_ShouldBeReturn()
        {
            int bookId = 0;


            FluentActions
                .Invoking(() => _bookRepository.GetBookById(bookId))
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap mevcut deðil!");
        }

        // Happy Path
        /*******************************************************************************************************/
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            var book = new Book()
            {
                BookTitle = " WhenValidInputsAreGiven_Book_ShouldBeCreated",
                BookPageCount = 100,
                BookPublishDate = new DateTime(1980, 01, 10),
                GenreId = 1
            };


            FluentActions
                .Invoking(() => _bookRepository.AddBook(_mapper.Map<CreateBookModel>(book))).Invoke();

            var addedBook = _bookStoreDbContext.Books.SingleOrDefault(b => b.BookTitle == book.BookTitle);

            addedBook.Should().NotBeNull();
            addedBook.BookPageCount.Should().Be(book.BookPageCount);
            addedBook.BookPublishDate.Should().Be(book.BookPublishDate);
            addedBook.GenreId.Should().Be(book.GenreId);
        }

        [Fact]
        public void WhenExistingBookIdGiven_Book_ShouldBeDeleted()
        {
            var bookToDelete = new Book()
            {
                BookTitle = "WhenExistingBookIdGiven_Book_ShouldBeDeleted",
                BookPageCount = 100,
                BookPublishDate = new DateTime(1980, 01, 10),
                GenreId = 1
            };
            _bookStoreDbContext.Books.Add(bookToDelete);
            _bookStoreDbContext.SaveChanges();


            FluentActions
                .Invoking(() => _bookRepository.DeleteBookById(bookToDelete.BookId)).Invoke();

            var deletedBook = _bookStoreDbContext.Books.FirstOrDefault(b => b.BookId == bookToDelete.BookId);

            deletedBook.Should().BeNull();
        }
        [Fact]
        public void WhenExistingBookIdGiven_Book_ShouldBeUpdated()
        {
            var bookToUpdate = new Book()
            {
                BookTitle = "WhenExistingBookIdGiven_Book_ShouldBeUpdated",
                BookPageCount = 100,
                BookPublishDate = new DateTime(1980, 01, 10),
                GenreId = 1
            };
            var book = new Book()
            {
                BookTitle = "WhenAlreadyExistBookTitleGiven_InvalidOperationException_ShouldBeReturn",
                BookPageCount = 100,
                BookPublishDate = new DateTime(1980, 01, 10),
                GenreId = 1
            };

            _bookStoreDbContext.Books.Add(bookToUpdate);
            _bookStoreDbContext.SaveChanges();

            FluentActions
                .Invoking(() => _bookRepository.UpdateBookById(bookToUpdate.BookId, _mapper.Map<UpdateBookModel>(book))).Invoke();

            var updatedBook = _bookStoreDbContext.Books.FirstOrDefault(b => b.BookId == bookToUpdate.BookId);

            updatedBook.BookTitle.Should().Be(book.BookTitle);
            updatedBook.BookPageCount.Should().Be(book.BookPageCount);
            updatedBook.BookPublishDate.Should().Be(book.BookPublishDate);
            updatedBook.GenreId.Should().Be(book.GenreId);
        }

        [Fact]
        public void WhenExistingBookIdGiven_BookModel_ShouldBeReturn()
        {
            var bookToGet = new Book()
            {
                BookTitle = "WhenExistingBookIdGiven_BookModel_ShouldBeReturn",
                BookPageCount = 100,
                BookPublishDate = new DateTime(1980, 01, 10),
                GenreId = 1
            };
            _bookStoreDbContext.Books.Add(bookToGet);
            _bookStoreDbContext.SaveChanges();

            FluentActions
                .Invoking(() => _bookRepository.GetBookById(bookToGet.BookId)).Invoke()
                .Should().BeOfType<BookModel>().And.Subject.Should().NotBeNull();
        }
    }
}