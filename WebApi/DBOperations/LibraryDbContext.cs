using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class LibraryDbContext : DbContext, ILibraryDbContext
    {
       public LibraryDbContext(DbContextOptions<LibraryDbContext> options):base(options)
       {

       }
       public DbSet<Book> Books { get; set; }
       public DbSet<Genre> Genres { get; set; }
       public DbSet<User> Users { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    } 
    
}