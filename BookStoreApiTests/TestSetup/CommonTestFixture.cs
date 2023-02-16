using AutoMapper;
using BookStoreApi.Common;
using BookStoreApi.DbOperations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApiTests.TestSetup
{
    public class CommonTestFixture
    {
        public BookStoreDbContext BookStoreDbContext { get; set; }
        public IMapper Mapper { get; set; }

        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName: "BookStoreTestDb").Options;
            BookStoreDbContext = new BookStoreDbContext(options);

            BookStoreDbContext.Database.EnsureCreated();
            BookStoreDbContext.AddAuthors();
            BookStoreDbContext.AddBooks();
            BookStoreDbContext.AddGenres();
            BookStoreDbContext.SaveChanges();

            Mapper = new MapperConfiguration(config => { config.AddProfile<MappingProfile>(); }).CreateMapper();
        }


    }
}
