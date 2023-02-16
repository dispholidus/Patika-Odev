using BookStoreApi.DbOperations;
using BookStoreApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApiTests.TestSetup
{
    public static class Genres
    {
        public static void AddGenres(this BookStoreDbContext context)
        {
            context.Genres.AddRange(
            new Genre { GenreName = "Personal Growth" },
            new Genre { GenreName = "Science Fiction" },
            new Genre { GenreName = "Romance" }
            );
        }
    }
}
