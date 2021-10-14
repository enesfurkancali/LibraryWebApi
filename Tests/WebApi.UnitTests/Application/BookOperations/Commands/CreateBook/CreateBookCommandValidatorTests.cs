using AutoMapper;
using TestSetup;
using Xunit;
using WebApi.DBOperations;
using WebApi.Application.BookOperations.Commands.CreateBook;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using WebApi.Entities;
using FluentAssertions;
using System;

namespace AppLication.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {        
        [Theory]
        [InlineData("lotrdds",0,0)]
        [InlineData("lotrdds",0,1)]
         [InlineData("lotrdds",100,0)]
        [InlineData("",0,0)]
        [InlineData("",100,1)]
        [InlineData("",0,1)]
        [InlineData("lot",100,1)]
        [InlineData("lord",100,0)]
        [InlineData("lotrdds",0,1)]
        [InlineData("",100,1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId)
        {
            //arrange
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel(){
                Title = title, PageCount=pageCount,PublishDate=DateTime.Now.Date.AddYears(-1),GenreId=genreId
            };

            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);           

        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError(){
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Lotrr", 
                PageCount=1000,
                PublishDate=DateTime.Now.Date,
                GenreId=1
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError(){
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel(){
                Title = "Lotrr", 
                PageCount=1000,
                PublishDate=DateTime.Now.Date.AddYears(-2),
                GenreId=1
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }

        
        
    }
    
}