using API.Models;

namespace API.Interfaces
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooks();
        Task<Book?> GetBookById(int id);

        Task<List<Category>> GetAllCategories();
        Task<Category?> GetCategoryById(int id);
        Task<List<Book>> GetFiveRandomBooksFromSimilarCategory(Category category);
    }

    
}
