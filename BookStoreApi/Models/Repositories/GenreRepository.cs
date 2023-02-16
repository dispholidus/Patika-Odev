using AutoMapper;
using BookStoreApi.DbOperations;
using BookStoreApi.Models.Entities;
using System.Net;

namespace BookStoreApi.Models.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly BookStoreDbContext _bookStoreDbContext;
        private readonly IMapper _mapper;

        public GenreRepository(BookStoreDbContext bookStoreDbContext, IMapper mapper)
        {
            _bookStoreDbContext = bookStoreDbContext;
            _mapper = mapper;
        }

        public void AddGenre(GenreModel genreModel)
        {
            Genre? genre = _bookStoreDbContext.Genres.FirstOrDefault(g => g.GenreName == genreModel.GenreName);
            if (genre != null)
            {
                throw new InvalidOperationException("Tür zaten mevcut!");
            }
            genre = _mapper.Map<Genre>(genreModel);

            _bookStoreDbContext.Add(genre);
            _bookStoreDbContext.SaveChanges();
        }

        public void DeleteGenreById(int genreId)
        {
            Genre? genre = _bookStoreDbContext.Genres.FirstOrDefault(g => g.GenreId == genreId);
            if (genre == null)
            {
                throw new InvalidOperationException("Tür mevcut değil!");
            }
            _bookStoreDbContext.Genres.Remove(genre);
            _bookStoreDbContext.SaveChanges();
        }

        public GenreModel? GetGenreById(int genreId)
        {
            Genre? genre = _bookStoreDbContext.Genres.FirstOrDefault(g => g.GenreId == genreId);

            if (genre == null)
            {
                throw new InvalidOperationException("Tür mevcut değil!");
            }
            GenreModel genreModel = _mapper.Map<GenreModel>(genre);
            return genreModel;
        }

        public IEnumerable<GenreModel> GetGenres()
        {
            IEnumerable<Genre> genres = _bookStoreDbContext.Genres.OrderBy(g => g.GenreId).ToList();
            List<GenreModel> genresModel = new();
            foreach (var genre in genres)
            {
                genresModel.Add(_mapper.Map<GenreModel>(genre));
            }
            return genresModel;
        }

        public void UpdateGenreById(int genreId, GenreModel genreModel)
        {
            Genre? genre = _bookStoreDbContext.Genres.FirstOrDefault(b => b.GenreId == genreId);
            if (genre == null)
            {
                throw new InvalidOperationException("Tür mevcut değil!");
            }
            genre.GenreName = genreModel.GenreName != default ? genreModel.GenreName : genre.GenreName;
            _bookStoreDbContext.SaveChanges();
        }
    }
    public class GenreModel
    {
        public string GenreName { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = true;
    }
}
