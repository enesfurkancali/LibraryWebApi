using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Commands.UpdateBook{
    public class UpdateBookCommand{
        private readonly ILibraryDbContext _dbContext;
        public UpdateBookModel Model{ get; set; }
        public int BookId { get; set; }
        public UpdateBookCommand(ILibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle(){
            var book = _dbContext.Books.SingleOrDefault(x=> x.Id==BookId);
            if(book is null){
                throw new InvalidOperationException("Güncellenecek Kitap Bulunamadı!");
            }
            
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            //book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            //book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : 
            //book.PublishDate;
            book.Title = string.IsNullOrEmpty(Model.Title.Trim()) ? book.Title : Model.Title;
            _dbContext.SaveChanges();
        }
        public class UpdateBookModel{
            public string Title { get; set; }
            public int GenreId { get; set; }
        }

    }
}
