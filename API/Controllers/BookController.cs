using API.Helpers;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

namespace API.Controllers
{
    [Route("api/Book")]
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

        /// <summary>
        /// Get all books
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks([FromQuery] BooksResourceParameters parameters)
        {
            if (parameters.Category != null)
            {
                bool isValidCategory = await _bookService.IsValidCategory(parameters.Category);

                if (!isValidCategory)
                {
                    return BadRequest($"Categoria Invalida: {parameters.Category} ");
                }
            }

            // execute filtering and searching of books
            List<Book> books = await _bookService.GetAllBooks(parameters);

            if (books.Count == 0) return NotFound($"No se encontro '{parameters.Search}' en Title ni en Author");
            
            books.ForEach(e =>  e.CoverFileName = _configuration["ApiUrl"] + e.CoverFileName);
            
            return books;
        }

        /// <summary>
        /// Get book by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            Book? book = await _bookService.GetBookById(id);

            if (book == null) return BadRequest();

            book.CoverFileName = _configuration["ApiUrl"] + book.CoverFileName;

            return Ok(book);
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        [Route("GetCategoriesList")]
        [HttpGet]
        public async Task<ActionResult<Categories>> GetAllCategories()
        {
            List<Categories> categories = await _bookService.GetAllCategories();

            return Ok(categories);
        }


        /// <summary>
        /// Get the random five books from the category of book whose BookId is supplied
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSimilarBooks/{bookId}")]
        public async Task<List<Book>> SimilarBooks(int bookId)
        {
            
            var  books = await _bookService.GetSimilarBooks(bookId);
            books.ForEach(e =>  e.CoverFileName = _configuration["ApiUrl"] + e.CoverFileName);

            return books;
        }

    }
}
