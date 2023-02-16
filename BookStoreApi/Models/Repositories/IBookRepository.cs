using AutoMapper;

namespace BookStoreApi.Models.Repositories
{
    public interface IBookRepository
    {
        public IEnumerable<BooksModel> GetBooks();
        public BookModel? GetBookById(int bookId);
        public void DeleteBookById(int bookId);
        public void UpdateBookById(int bookId, UpdateBookModel newBook);
        public void AddBook(CreateBookModel newBook);
    }
}
