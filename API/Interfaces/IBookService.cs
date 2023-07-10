using API.Helpers;
using API.Models;

namespace API.Interfaces
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooks(BooksResourceParameters parameters);
        Task<Book?> GetBookById(int id);

        Task<List<Category>> GetAllCategories();
        Task<bool> IsValidCategory(string category);
        Task<Category?> GetCategoryById(int id);

        Task<List<Book>> GetFiveRandomBooksFromSimilarCategory(Category category);
    }

    
}
