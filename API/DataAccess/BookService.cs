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
        public async Task<List<Book>> GetAllBooks()
        {
            return await _dbContext.Books.AsNoTracking().ToListAsync();
        }

        public async Task<Book?> GetBookById(int id)
        {
            return await  _dbContext.Books.FirstOrDefaultAsync(b => b.Bookid == id);
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c => c.Categoryid == id);
        }


        public async Task<List<Book>> GetFiveRandomBooksFromSimilarCategory(Category category)
        {   
            Random random = new Random();

            List<Book> randomBooks = await _dbContext.Books
                .Where(b => b.Category == category.Categoryname)
                .ToListAsync();

            randomBooks = randomBooks.OrderBy(b => random.Next()).Take(5).ToList();


               

            return randomBooks;
        }
    }
}
