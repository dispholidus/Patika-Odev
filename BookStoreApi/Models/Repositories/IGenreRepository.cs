using AutoMapper;

namespace BookStoreApi.Models.Repositories
{
    public interface IGenreRepository
    {
        public IEnumerable<GenreModel> GetGenres();
        public GenreModel? GetGenreById(int genreId);
        public void DeleteGenreById(int genreId);
        public void UpdateGenreById(int genreId, GenreModel genreModel);
        public void AddGenre(GenreModel genreModel);
    }
}
