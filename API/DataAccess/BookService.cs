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
        public Task<List<Book>> GetAllBooks()
        {
            return _dbContext.Books.AsNoTracking().ToListAsync();
        }
    }
}
