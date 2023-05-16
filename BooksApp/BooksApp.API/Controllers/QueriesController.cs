using BooksApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BooksApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueriesController : ControllerBase
    {
        private readonly QueryingService queryingService;

        public QueriesController(QueryingService queryingService)
        {
            this.queryingService = queryingService;
        }

        [HttpGet("[action]")]
        public IActionResult GetFirstBook()
        {
            var firstBook = queryingService.GetFirstBook();
            return Ok(firstBook);
        }

        [HttpGet("[action]")]
        public IActionResult GetBooks()
        {
            var firstBook = queryingService.GetBooks();
            return Ok(firstBook);
        }

        [HttpGet("[action]")]
        public IActionResult GetBooksSelect()
        {
            var firstBook = queryingService.GetBooksWithSelectLoading();
            return Ok(firstBook);
        }


        [HttpPost("[action]")]
        public IActionResult AddReviewToBook(int id, string comment)
        {
            queryingService.AddReviewToBook(id, comment);
            return Ok();
        }
    }
}
