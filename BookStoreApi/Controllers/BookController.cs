using AutoMapper;
using BookStoreApi.Models;
using BookStoreApi.Models.Repositories;
using BookStoreApi.Models.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly CreateBookModelValidator _createBookModelValidator;
        private readonly BookModelValidator _bookModelValidator;
        private readonly UpdateBookModelValidator _updateBookModelValidator;
        public BookController(IBookRepository bookRepository, CreateBookModelValidator createBookModelValidator,
            BookModelValidator bookModelValidator, UpdateBookModelValidator updateBookModelValidator)
        {
            _bookRepository = bookRepository;
            _createBookModelValidator = createBookModelValidator;
            _bookModelValidator = bookModelValidator;
            _updateBookModelValidator = updateBookModelValidator;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            IEnumerable<BooksModel> books = _bookRepository.GetBooks();
            return Ok(books);
        }

        [HttpGet("{bookId}")]
        public IActionResult GetBookById(int bookId)
        {
            BookModel? book = _bookRepository.GetBookById(bookId);
            try
            {
                if (book != null)
                {
                    _bookModelValidator.ValidateAndThrow(book);
                    return Ok(book);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel book)
        {
            try
            {
                _createBookModelValidator.ValidateAndThrow(book);
                _bookRepository.AddBook(book);
                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpDelete("{bookId}")]
        public IActionResult DeleteBook(int bookId)
        {
            try
            {
                _bookRepository.DeleteBookById(bookId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{bookId}")]
        public IActionResult UpdateBook(int bookId, UpdateBookModel newBook)
        {
            try
            {
                _updateBookModelValidator.ValidateAndThrow(newBook);
                _bookRepository.UpdateBookById(bookId, newBook);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}