using API.Interfaces;
using API.models;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DataAccess
{
    public class CartService :ICartService
    {   
        private readonly BookCartContext _dbContext;

        public CartService(BookCartContext context)
        {
            _dbContext = context;
        }
        public async Task AddBookToCart(int userId, int bookId)
        {
            string cartId = await GetCartId(userId);
            int quantity = 1;

            CartItems existingCartItem = _dbContext.CartItems.FirstOrDefault(x => x.ProductId == bookId && x.CartId == cartId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += 1;
                _dbContext.Entry(existingCartItem).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                CartItems cartItems = new CartItems
                {
                    CartId = cartId,
                    ProductId = bookId,
                    Quantity = quantity
                };
                _dbContext.CartItems.Add(cartItems);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveCartItem(int userId, int bookId)
        {
            string cartId = await GetCartId(userId);
            CartItems cartItem = _dbContext.CartItems.FirstOrDefault(x => x.ProductId == bookId && x.CartId == cartId);

            _dbContext.CartItems.Remove(cartItem);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOneCartItem(int userId, int bookId)
        {
            string cartId = await GetCartId(userId);
            CartItems cartItem = await _dbContext.CartItems.FirstOrDefaultAsync(x => x.ProductId == bookId && x.CartId == cartId);

            cartItem.Quantity -= 1;
            _dbContext.Entry(cartItem).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> GetCartItemCount(int userId)
        {
            string cartId = await GetCartId(userId);

            if (!string.IsNullOrEmpty(cartId))
            {
                int cartItemCount = _dbContext.CartItems.Where(x => x.CartId == cartId).Sum(x => x.Quantity);

                return cartItemCount;
            }

            return 0;
        }

        public async Task MergeCart(int tempUserId, int permUserId)
        {
            if (tempUserId != permUserId && tempUserId > 0 && permUserId > 0)
            {
                string tempCartId = await GetCartId(tempUserId);
                string permCartId = await GetCartId(permUserId);

                List<CartItems> tempCartItems =  _dbContext.CartItems.Where(x => x.CartId == tempCartId).ToList();
                
                foreach (CartItems item in tempCartItems)
                {
                    CartItems cartItem = await _dbContext.CartItems.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.CartId == permCartId);

                    if (cartItem != null)
                    {
                        cartItem.Quantity += item.Quantity;
                        _dbContext.Entry(cartItem).State = EntityState.Modified;
                    }
                    else
                    {
                        CartItems newCartItem = new CartItems
                        {
                            CartId = permCartId,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity
                        };
                        await _dbContext.CartItems.AddAsync(newCartItem);
                    }
                    _dbContext.CartItems.Remove(item);
                    await _dbContext.SaveChangesAsync();
                }

                // Delete Card
                Cart cart = await _dbContext.Cart.FindAsync(tempCartId);
                _dbContext.Cart.Remove(cart);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<int> ClearCart(int userId)
        {
            string cartId = await GetCartId(userId);
            List<CartItems> cartItem = await _dbContext.CartItems.Where(x => x.CartId == cartId).ToListAsync();

            if (!string.IsNullOrEmpty(cartId))
            {
                foreach (CartItems item in cartItem)
                {
                    _dbContext.CartItems.Remove(item);
                    await _dbContext.SaveChangesAsync();
                }
            }
            return 0;
        }

        public async Task<string> GetCartId(int userId)
        {
            Cart? cart = await _dbContext.Cart.FirstOrDefaultAsync(x => x.UserId == userId);

            if (cart != null)
            {
                return cart.CartId;
            }

            Cart shoppingCart = new Cart
                {
                    CartId = Guid.NewGuid().ToString(),
                    UserId = userId,
                    DateCreated = DateTime.Now.Date
                };
            await _dbContext.Cart.AddAsync(shoppingCart);
            await _dbContext.SaveChangesAsync();

            return shoppingCart.CartId;
        }


      
    }
}
