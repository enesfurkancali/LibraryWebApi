using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator{
        public static void Initialize(IServiceProvider serviceProvider){
            using (var context = new LibraryDbContext(serviceProvider.GetRequiredService<DbContextOptions<LibraryDbContext>>())){
                if (context.Books.Any()){
                    return;
                }

                context.Genres.AddRange(
                    new Genre{
                        Name = "Personal Growth"
                    },
                    new Genre{
                        Name = "Science Fiction"
                    },
                    new Genre{
                        Name = "Romance"
                    }
                );

                context.Books.AddRange(
                    new Book{
                //Id=1,
                Title="karamazov kardesler",
                GenreId=1,
                PageCount=700,
                PublishDate=new DateTime(2000,06,12)
            },
             new Book{
                //Id=2,
                Title="yabanci",
                GenreId=2,
                PageCount=100,
                PublishDate=new DateTime(1999,06,12)
            },
             new Book{
                //Id=3,
                Title="dune",
                GenreId=3,
                PageCount=500,
                PublishDate=new DateTime(2002,12,21)
            }                     
                );
                context.SaveChanges();
            }
        }
    }
    
}