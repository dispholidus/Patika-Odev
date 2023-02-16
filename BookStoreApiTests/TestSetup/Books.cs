using BookStoreApi.DbOperations;
using BookStoreApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApiTests.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
            new Book { BookTitle = "Lean Startup", GenreId = 1, AuthorId = 1, BookPageCount = 200, BookPublishDate = new DateTime(2001, 06, 12) },
            new Book { BookTitle = "Herland", GenreId = 2, AuthorId = 2, BookPageCount = 250, BookPublishDate = new DateTime(2010, 05, 23) },
            new Book { BookTitle = "Dune", GenreId = 2, AuthorId = 3, BookPageCount = 540, BookPublishDate = new DateTime(2001, 12, 21) }
            );
        }
    }
}
