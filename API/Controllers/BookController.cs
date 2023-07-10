using API.Helpers;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

namespace API.Controllers
{
    [Route("api/books")]
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
            
            books.ForEach(e =>  e.Coverfilename = _configuration["ApiUrl"] + e.Coverfilename);
            
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

            book.Coverfilename = _configuration["ApiUrl"] + book.Coverfilename;

            return Ok(book);
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        [Route("categories")]
        [HttpGet]
        public async Task<ActionResult<Category>> GetAllCategories()
        {
            List<Category> categories = await _bookService.GetAllCategories();

            return Ok(categories);
        }

        /// <summary>
        /// Get five random books from a similar category
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        [Route("FiveRandomBooksFromSimilarCategory/{CategoryId}")]
        [HttpGet]
        public async Task<ActionResult<Book>> GetFiveRandomBooksFromSimilarCategory(int CategoryId)
        {
            Category? category = await _bookService.GetCategoryById(CategoryId);

            if (category == null) return BadRequest();


            List<Book> books = await _bookService.GetFiveRandomBooksFromSimilarCategory(category);
            books.ForEach(e => e.Coverfilename = _configuration["ApiUrl"] + e.Coverfilename);

            return Ok(books);
        }


    }
}
