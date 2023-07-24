namespace API.Interfaces
{
    public interface IWishlistService
    { 
        Task ToggleWishlistItem(int userId, int bookId);
        Task<int> ClearWishlist(int userId);
        string GetWishlistId(int userId);
    }
}
