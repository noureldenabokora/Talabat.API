using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.ApIs.Dtos;
using Talabat.ApIs.Errors;
using Talabat.Core.Entites.Order_Aggregate;
using Talabat.Core.Services;

namespace Talabat.ApIs.Controllers
{

    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService,IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status400BadRequest)]
        [HttpPost]  // POST :  /api/order
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var address = _mapper.Map<AddressDto,Address>(orderDto.ShippingAddress);

            var order =  await _orderService.CreateOrderAsync(buyerEmail,orderDto.BasketId,orderDto.DeliveryMethodId, address);

            if (order is null) return BadRequest(new ApiResponse(400));

            return Ok(order);
        }

        [HttpGet] // GET : /api/orders 
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrderForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimValueTypes.Email);

            var orders = await _orderService.GetOrdersForSpecificUserAsync(buyerEmail);

            return Ok(orders);
        }


        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")] //GET : /api/orders/1
        public async Task<ActionResult<Order>> GetOrderForUser(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.GetOrderByIdForUserAsync(id, buyerEmail);


            if (order is null) return NotFound(new ApiResponse(404));
          
            return Ok(order);
        }


        [HttpGet("{deliveryMethods}")] //GET : /api/orders/deliveryMethods
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethod()
        {
            var deliveryMethod =await _orderService.GetDeliveryMethodAsync();

            return Ok(deliveryMethod);
        }
 
    }
}
