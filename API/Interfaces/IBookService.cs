using API.Models;

namespace API.Interfaces
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooks();

    }
}
