using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IConfiguration _configuration;
        public BookController(IBookService bookService, IConfiguration configuration)
        {
            _bookService = bookService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> Get()
        {

            List<Book> books = await _bookService.GetAllBooks();
            books.ForEach(e =>  e.Coverfilename = _configuration["ApiUrl"] + e.Coverfilename);
            
            return books;
        }

    }
}
