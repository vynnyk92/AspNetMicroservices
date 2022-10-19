using Microsoft.AspNetCore.Mvc;

namespace Discount.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiscountController : ControllerBase
    {

        private readonly ILogger<DiscountController> _logger;

        public DiscountController(ILogger<DiscountController> logger)
        {
            _logger = logger;
        }

    }
}