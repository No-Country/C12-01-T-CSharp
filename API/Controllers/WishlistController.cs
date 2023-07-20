using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        readonly IWishlistService _wishlistService;
        readonly IBookService _bookService;
        readonly IUserService _userService;

        public WishlistController(IWishlistService wishlistService, IBookService bookService, IUserService userService)
        {
            _wishlistService = wishlistService;
            _bookService = bookService;
            _userService = userService;
        }

        /// <summary>
        /// Get the list of items in the Wishlist
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>All the items in the Wishlist</returns>
        [HttpGet("{userId}")]
        public async Task<List<Book>> Get(int userId)
        {
            return await GetUserWishlist(userId);
        }

        /// <summary>
        /// Toggle the items in Wishlist. If item doesn't exists, it will be added to the Wishlist else it will be removed.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bookId"></param>
        /// <returns>All the items in the Wishlist</returns>
        [Authorize]
        [HttpPost]
        [Route("ToggleWishlist/{userId}/{bookId}")]
        public async Task<List<Book>> Post(int userId, int bookId)
        {
            _wishlistService.ToggleWishlistItem(userId, bookId);
            return await GetUserWishlist(userId);
        }

        /// <summary>
        /// Clear the Wishlist
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{userId}")]
        public async Task<int> Delete(int userId)
        {
            return await _wishlistService.ClearWishlist(userId);
        }

         async Task<List<Book>> GetUserWishlist(int userId)
        {
            bool user = _userService.isUserExists(userId);
            if (user)
            {
                string Wishlistid = _wishlistService.GetWishlistId(userId);
                return await  _bookService.GetBooksAvailableInWishlist(Wishlistid);
            }
            else
            {
                return new List<Book>();
            }

        }
    }
}
