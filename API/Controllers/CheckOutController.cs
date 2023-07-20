using API.Dtos;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckOutController : ControllerBase
    {
        readonly IOrderService _orderService;
        readonly ICartService _cartService;

        public CheckOutController(IOrderService orderService, ICartService cartService)
        {
            _orderService = orderService;
            _cartService = cartService;
        }

        /// <summary>
        /// Checkout from shopping cart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="checkedOutItems"></param>
        /// <returns></returns>
        [HttpPost("{userId}")]
        public async Task<int> Post(int userId, [FromBody] OrdersDto checkedOutItems)
        {
             _orderService.CreateOrder(userId, checkedOutItems);
            return await _cartService.ClearCart(userId);
        }
    }
}
