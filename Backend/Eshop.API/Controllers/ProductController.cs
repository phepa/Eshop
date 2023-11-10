using Eshop.Shared.Helpers;
using Eshop.Shared.Models.Requests.Product;
using Eshop.Shared.Models.Responses.Product;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        /// <summary>Schedule new operation under current model version lock.</summary>
        /// <param name="request">See <see cref="GetProductsResponse"/></param>
        /// <returns>See <see cref="GetProductsResponse"/>for HTTP 200</returns>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProductsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProducts(GetProductsRequest request)
        {
            if (request.IsNull())
            {
                return BadRequest(request);
            }

            if (request.SearchQuery.IsNull())
            {
                return BadRequest(nameof(request.SearchQuery));
            }

            List<string> products = new List<string>()
            {
                "Product 1", "Product 2", "Product 3"
            };

            return Ok(products);
        }
    }
}