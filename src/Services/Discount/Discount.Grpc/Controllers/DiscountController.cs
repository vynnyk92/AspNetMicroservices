using System.Net;
using Discount.Api.Entities;
using Discount.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {

        private readonly IDiscountRepository _discountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        [HttpGet("{productName}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Coupon))]
        public async Task<IActionResult> GetProducts(string productName)
        {
            var discount = await _discountRepository.GetDiscount(productName);
            return Ok(discount);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateCoupon([FromBody] Coupon coupon)
        {
            await _discountRepository.CreateCoupon(coupon);
            return CreatedAtRoute("GetDiscount", new { id = coupon.ProductName }, coupon);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateCoupon([FromBody] Coupon coupon)
        {
            await _discountRepository.UpdateCoupon(coupon);
            return CreatedAtRoute("GetDiscount", new { id = coupon.ProductName }, coupon);
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteCoupon([FromQuery] string productName)
        {
            await _discountRepository.DeleteCoupon(productName);
            return Ok();
        }
    }
}