using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.ApIs.Dtos;
using Talabat.ApIs.Errors;
using Talabat.Core.Services;

namespace Talabat.ApIs.Controllers
{
 
    public class PaymentsController : BaseApiController
    {
        private readonly IpaymentService _paymentService;

        public PaymentsController(IpaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [ProducesResponseType(typeof(CustomerBasketDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost] // POST :  /api/Payments?id=basketId
        public  async Task<ActionResult<CustomerBasketDto>> CreateOrUpdateIntent(string basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            if (basket == null) return BadRequest(new ApiResponse(400, "A Problem with your Basket"));

            return Ok(basket);

        }

    }
}
