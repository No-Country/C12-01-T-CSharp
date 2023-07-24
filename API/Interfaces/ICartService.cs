namespace API.Interfaces
{
    public interface ICartService
    {
        Task AddBookToCart(int userId, int bookId);
        Task RemoveCartItem(int userId, int bookId);
        Task DeleteOneCartItem(int userId, int bookId);
        Task<int> GetCartItemCount(int userId);
        Task MergeCart(int tempUserId, int permUserId);
        Task<int> ClearCart(int userId);
        Task<string> GetCartId(int userId);
    }
}
