using API.Interfaces;
using API.models;
using API.Models;

namespace API.DataAccess
{
    public class WishlistDataAccessLayer : IWishlistService
    {
        readonly BookCartContext _dbContext;

        public WishlistDataAccessLayer(BookCartContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ToggleWishlistItem(int userId, int bookId)
        {
            string wishlistId = GetWishlistId(userId);
            WishlistItems existingWishlistItem = _dbContext.WishlistItems.FirstOrDefault(x => x.ProductId == bookId && x.WishlistId == wishlistId);

            if (existingWishlistItem != null)
            {
                _dbContext.WishlistItems.Remove(existingWishlistItem);
                _dbContext.SaveChanges();
            }
            else
            {
                WishlistItems wishlistItem = new WishlistItems
                {
                    WishlistId = wishlistId,
                    ProductId = bookId,
                };
                _dbContext.WishlistItems.Add(wishlistItem);
                _dbContext.SaveChanges();
            }
        }

        public async Task<int> ClearWishlist(int userId)
        {
            
            string wishlistId = GetWishlistId(userId);
            List<WishlistItems> wishlistItem = _dbContext.WishlistItems.Where(x => x.WishlistId == wishlistId).ToList();

            if (!string.IsNullOrEmpty(wishlistId))
            {
                foreach (WishlistItems item in wishlistItem)
                {
                    _dbContext.WishlistItems.Remove(item);
                    await _dbContext.SaveChangesAsync();
                }
            }
            return 0;
        }

        public string GetWishlistId(int userId)
        {
            
            Wishlist wishlist = _dbContext.Wishlist.FirstOrDefault(x => x.UserId == userId);

            if (wishlist != null)
            {
                return  wishlist.WishlistId;
            }
            else
            {
                return CreateWishlist(userId);
            }
        }

        string CreateWishlist(int userId)
        {
            Wishlist wishList = new Wishlist
            {
                WishlistId = Guid.NewGuid().ToString(),
                UserId = userId,
                DateCreated = DateTime.Now.Date
            };

            _dbContext.Wishlist.Add(wishList);
            _dbContext.SaveChanges();

            return wishList.WishlistId;
        }
    }
}
