using API.Helpers;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DataAccess
{

    public class BookService : IBookService
    {
        private readonly BookCartContext _dbContext;

        public BookService(BookCartContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Book>> GetAllBooks(BooksResourceParameters parameters)
        {
            var collection = _dbContext.Books  as IQueryable<Book>;


            if (parameters.Price == "DES")
            {
                collection = collection.OrderByDescending(b => b.Price);
            }
            else collection = collection.OrderBy(b => b.Price);


            //Fintering
            if (!string.IsNullOrWhiteSpace(parameters.Category))
            {
                var category = parameters.Category.Trim().ToLower();
                collection = collection.Where(b => b.Category.ToLower() == category);
            }

            //Searching
            if (!string.IsNullOrWhiteSpace(parameters.Search))
            {
                var searching = parameters.Search.Trim().ToLower();
                collection = collection.Where(b => b.Title.ToLower().Contains(searching) || b.Author.ToLower().Contains(searching));
            }

            return await collection.ToListAsync();

        }

        public async Task<Book?> GetBookById(int id)
        {
            return await  _dbContext.Books.FirstOrDefaultAsync(b => b.BookId == id);
        }

        public async Task<List<Categories>> GetAllCategories()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<bool> IsValidCategory(string category)
        {   
            string cat = category.Trim().ToLower();

            return await _dbContext.Categories.AnyAsync(c => c.CategoryName.ToLower() == cat);
        }

        public async Task<Categories?> GetCategoryById(int id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
        }


        public async Task<List<Book>> GetFiveRandomBooksFromSimilarCategory(Categories category)
        {   
            Random random = new Random();

            List<Book> randomBooks = await _dbContext.Books
                .Where(b => b.Category == category.CategoryName)
                .ToListAsync();

            randomBooks = randomBooks.OrderBy(b => random.Next()).Take(5).ToList();


               

            return randomBooks;
        }
    }
}
