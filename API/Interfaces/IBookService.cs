using API.Dtos;
using API.Helpers;
using API.Models;

namespace API.Interfaces
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooks(BooksResourceParameters parameters);
        Task<Book?> GetBookById(int id);

        Task<List<Categories>> GetAllCategories();
        Task<bool> IsValidCategory(string category);


        Task<Categories?> GetCategoryById(int id);

        Task<List<Book>> GetFiveRandomBooksFromSimilarCategory(Categories category);


        Task<List<CartItemDto>> GetBooksAvailableInCart(string cartId);

        Task<List<Book>> GetBooksAvailableInWishlist(string wishlistID);

        Task<List<Book>> GetSimilarBooks(int bookId);

        int AddBook(Book book);

        string DeleteBook(int bookId);

        Task<int> UpdateBook(Book book);

    }


}
