using AutoMapper;
using TestSetup;
using Xunit;
using WebApi.DBOperations;
using WebApi.Application.BookOperations.Commands.CreateBook;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using WebApi.Entities;
using FluentAssertions;
using System;
using System.Linq;

namespace AppLication.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly LibraryDbContext _context;
        private readonly IMapper _mapper;
        
        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        
        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange 
            var book = new Book(){Title = "WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", PageCount = 100, PublishDate=new System.DateTime(1999,01,10), GenreId=1};
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel(){Title = book.Title};

            //act & assert 
            FluentActions.Invoking(()=> command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut!");

        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            //Arrange
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
             CreateBookModel model = new CreateBookModel(){
                Title = "Lotrr", 
                PageCount=1000,
                PublishDate=DateTime.Now.Date.AddYears(-2),
                GenreId=1
            };
            command.Model=model;

            //act
            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var book = _context.Books.SingleOrDefault(book=> book.Title == model.Title);

            book.Should().NotBeNull();
            book.PublishDate.Should().Be(model.PublishDate);
            book.PageCount.Should().Be(model.PageCount);
            book.GenreId.Should().Be(model.GenreId);
            
            

        }


        
        
    }
    
}