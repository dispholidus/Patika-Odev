using BookStoreApi.DbOperations;
using BookStoreApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApiTests.TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {

            context.Authors.AddRange(
                    new Author
                    {
                        AuthorName = "Eric",
                        AuthorSurname = "Ries",
                        AuthorBirthday = new DateTime(1978, 09, 22)
                    },
                    new Author
                    {
                        AuthorName = "Charlotte",
                        AuthorSurname = "Perkins",
                        AuthorBirthday = new DateTime(1860, 7, 3)
                    },
                    new Author
                    {
                        AuthorName = "Frank",
                        AuthorSurname = "Herbert",
                        AuthorBirthday = new DateTime(1920, 10, 8)
                    });
        }
    }
}
