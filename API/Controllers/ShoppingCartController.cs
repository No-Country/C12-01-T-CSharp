using API.Dtos;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        readonly ICartService _cartService;
        readonly IBookService _bookService;

        public ShoppingCartController(ICartService cartService, IBookService bookService)
        {
            _cartService = cartService;
            _bookService = bookService;
        }

        /// <summary>
        /// Get the shopping cart for a user upon Login. If the user logs in for the first time, creates the shopping cart.
        /// </summary>
        /// <param name="oldUserId"></param>
        /// <param name="newUserId"></param>
        /// <returns>The count of items in shopping cart</returns>
        //[Authorize]
        [HttpGet]
        [Route("SetShoppingCart/{oldUserId}/{newUserId}")]
        public async Task<int> Get(int oldUserId, int newUserId)
        {
            await _cartService.MergeCart(oldUserId, newUserId);
            return await _cartService.GetCartItemCount(newUserId);
        }

        /// <summary>
        /// Get the list of items in the shopping cart
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public async Task<List<CartItemDto>> Get(int userId)
        {
            string cartid = await _cartService.GetCartId(userId);
            return await _bookService.GetBooksAvailableInCart(cartid);
        }


        /// <summary>
        /// Add a single item into the shopping cart. If the item already exists, increase the quantity by one
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bookId"></param>
        /// <returns>Count of items in the shopping cart</returns>
        [HttpPost]
        [Route("AddToCart/{userId}/{bookId}")]
        public async Task<int> Post(int userId, int bookId)
        {
            await _cartService.AddBookToCart(userId, bookId);
            return await _cartService.GetCartItemCount(userId);
        }

        /// <summary>
        /// Reduces the quantity by one for an item in shopping cart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bookId"></param>
        /// <returns></returns>
        [HttpPut("{userId}/{bookId}")]
        public async Task<int> Put(int userId, int bookId)
        {
            await _cartService.DeleteOneCartItem(userId, bookId);
            return await _cartService.GetCartItemCount(userId);
        }

        /// <summary>
        /// Delete a single item from the cart 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bookId"></param>
        /// <returns></returns>
        [HttpDelete("{userId}/{bookId}")]
        public async Task<int> Delete(int userId, int bookId)
        {
            await _cartService.RemoveCartItem(userId, bookId);
            return  await _cartService.GetCartItemCount(userId);
        }

        /// <summary>
        /// Clear the shopping cart
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete("{userId}")]
        public async Task<int> Delete(int userId)
        {
            return await _cartService.ClearCart(userId);
        }
    }
}
