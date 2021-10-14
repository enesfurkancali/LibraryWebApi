using AutoMapper;
using WebApi.DBOperations;
using WebApi.Common;
using Microsoft.EntityFrameworkCore;

namespace TestSetup
{
    public class CommonTestFixture
    {
        public LibraryDbContext Context{ get; set; }
        public IMapper Mapper { get; set; }
        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<LibraryDbContext>().UseInMemoryDatabase(databaseName:"LibraryTestDB").Options;
            Context = new LibraryDbContext(options);
            Context.Database.EnsureCreated();
            Context.AddBooks();
            Context.AddGenres();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg => {cfg.AddProfile<MappingProfile>();}).CreateMapper();
            

        }
        
    }
    
}