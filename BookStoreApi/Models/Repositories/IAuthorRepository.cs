using BookStoreApi.Models.Entities;

namespace BookStoreApi.Models.Repositories
{
    public interface IAuthorRepository
    {
        public AuthorModel? GetAuthorById(int authorId);
        public IEnumerable<AuthorModel> GetAuthors();
        public void AddAuthor(AuthorModel authorModel);
        public void UpdateAuthorById(int authorId, AuthorModel authorModel);
        public void DeleteAuthorById(int authorId);
    }
}
