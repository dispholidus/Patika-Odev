using AutoMapper;
using BookStoreApi.DbOperations;
using BookStoreApi.Models.Entities;
using BookStoreApi.Models.Repositories;
using BookStoreApiTests.TestSetup;
using FluentAssertions;

namespace BookStoreApiTests.Tests
{
    public class GenreRepositoryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _bookStoreDbContext;
        private readonly IMapper _mapper;
        private readonly GenreRepository _genreRepository;

        public GenreRepositoryTests(CommonTestFixture testFixture)
        {
            _bookStoreDbContext = testFixture.BookStoreDbContext;
            _mapper = testFixture.Mapper;
            _genreRepository = new GenreRepository(_bookStoreDbContext, _mapper);
        }

        // Worst Case Scenario's
        /*******************************************************************************************************/
        [Fact]
        public void WhenWrongGenreIdGivenToCreate_InvalidOperationException_ShouldBeReturn()
        {
            Genre genre = new() { GenreName = "WhenWrongGenreIdGiven_InvalidOperationException_ShouldBeReturn" };
            _bookStoreDbContext.Add(genre);
            _bookStoreDbContext.SaveChanges();

            FluentActions
                .Invoking(() => _genreRepository.AddGenre(_mapper.Map<GenreModel>(genre)))
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Tür zaten mevcut!");

        }

        [Fact]
        public void WhenNonExistingGenreIdGivenToDelete_InvalidOperationException_ShouldBeReturn()
        {
            int genreId = 0;

            FluentActions
                .Invoking(() => _genreRepository.DeleteGenreById(genreId))
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Tür mevcut değil!");

        }
        [Fact]
        public void WhenNonExistingGenreIdGivenToUpdate_InvalidOperationException_ShouldBeReturn()
        {
            int genreId = 0;
            Genre genre = new() { GenreName = "WhenNonExistingGenreIdGivenToUpdate_InvalidOperationException_ShouldBeReturn" };


            FluentActions
                .Invoking(() => _genreRepository.UpdateGenreById(genreId, _mapper.Map<GenreModel>(genre)))
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Tür mevcut değil!");

        }
        [Fact]
        public void WhenNonExistingGenreIdGivenToGet_InvalidOperationException_ShouldBeReturn()
        {
            int genreId = 0;


            FluentActions
                .Invoking(() => _genreRepository.GetGenreById(genreId))
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Tür mevcut değil!");
        }
        // Happy Path
        /*******************************************************************************************************/
        [Fact]
        public void WhenValidInputsGivenToCreate_Genre_ShouldBeCreated()
        {
            Genre genre = new() { GenreName = "WhenValidInputsGiven_Genre_ShouldBeCreated" };

            FluentActions
                .Invoking(() => _genreRepository.AddGenre(_mapper.Map<GenreModel>(genre))).Invoke();

            Genre? addedGenre = _bookStoreDbContext.Genres.FirstOrDefault(g => g.GenreName == genre.GenreName);

            addedGenre.Should().NotBeNull();
            addedGenre.GenreName.Should().Be(genre.GenreName);
            addedGenre.IsActive.Should().Be(genre.IsActive);
        }
        [Fact]
        public void WhenValidGenreIdGivenToDelete_Genre_ShouldBeDeleted()
        {
            Genre genreToDelete = new() { GenreName = "WhenValidGenreIdGivenToCreate_Genre_ShouldBeDeleted" };

            _bookStoreDbContext.Add(genreToDelete);
            _bookStoreDbContext.SaveChanges();

            FluentActions
                .Invoking(() => _genreRepository.DeleteGenreById(genreToDelete.GenreId)).Invoke();

            Genre? deletedGenre = _bookStoreDbContext.Genres.FirstOrDefault(g => g.GenreId == genreToDelete.GenreId);

            deletedGenre.Should().BeNull();
        }
        [Fact]
        public void WhenValidGenreIdAndGenreGivenToUpdate_Genre_ShouldBeUpdated()
        {
            Genre genreToUpdate = new() { GenreName = "WhenValidGenreIdGivenToCreate_Genre_ShouldBeDeleted" };
            Genre genre = new() { GenreName = "WhenValidGenreIdGivenToCreate_Genre_ShouldBeDeleted2" };

            _bookStoreDbContext.Add(genreToUpdate);
            _bookStoreDbContext.SaveChanges();

            FluentActions
                .Invoking(() => _genreRepository.UpdateGenreById(genreToUpdate.GenreId, _mapper.Map<GenreModel>(genre))).Invoke();

            Genre? updatedGenre = _bookStoreDbContext.Genres.FirstOrDefault(g => g.GenreId == genreToUpdate.GenreId);

            updatedGenre.GenreName.Should().Be(genre.GenreName);
            updatedGenre.IsActive.Should().Be(genre.IsActive);
        }
        [Fact]
        public void WhenValidGenreIdGivenToGet_GenreModel_ShouldBeReturn()
        {
            Genre genreToGet = new() { GenreName = "WhenValidGenreIdGivenToGet_GenreModel_ShouldBeReturn" };
            _bookStoreDbContext.Add(genreToGet);
            _bookStoreDbContext.SaveChanges();

            FluentActions
                .Invoking(() => _genreRepository.GetGenreById(genreToGet.GenreId)).Invoke()
                .Should().BeOfType<GenreModel>().And.Subject.Should().NotBeNull();
        }
    }
}
