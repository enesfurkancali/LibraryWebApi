using WebApi.DBOperations;
using WebApi.Entities;
using System;

namespace TestSetup
{
    public static class Books
    {
        public static void AddBooks(this LibraryDbContext context)
        {

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

        }
        
    }
    
}