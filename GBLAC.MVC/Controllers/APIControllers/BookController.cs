using GBLAC.Models;
using GBLAC.Models.DTOs;
using GBLAC.Services.APIServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GBLAC.MVC.Controllers.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
       private readonly IBookServices _bookServices;
        public BookController(IBookServices bookServices)
        {
            _bookServices = bookServices;

        }

        [HttpGet("Book")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetBook(string bookName)
        {
            return Ok(_bookServices.GetBookAsync(bookName));
        }

        [HttpGet("BooksByCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllBooksByCategory(PagingDTO pager, string categoryName)
        {
            return Ok(_bookServices.GetAllBooksByCategory(pager,categoryName));
        }

        [HttpGet("BooksByTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllBooksByTypes(PagingDTO pager, string bookTypeName)
        {
            return Ok(_bookServices.GetAllBooksByType(pager, bookTypeName));
        }

        [HttpPost("Book")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddBook(BookDTO book)
        {
            return Ok(_bookServices.AddBookAsync(book));
        }

        [HttpPost("BookDelete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteBook(Book book)
        {
            return Ok(_bookServices.DeleteBookAsync(book));
        }

        [HttpPut("BookUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateBook(Book book)
        {
            return Ok(_bookServices.UpdateBook(book));
        }

    }
}
