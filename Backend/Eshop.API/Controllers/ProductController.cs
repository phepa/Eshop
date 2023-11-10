using Eshop.API.Publishers;
using Eshop.Database;
using Eshop.Shared.Helpers;
using Eshop.Shared.Models.Messages;
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
        private readonly EshopDbContext _context;
        private readonly NotificationPublisher _notificationPublisher;

        public ProductController(ILogger<ProductController> logger, NotificationPublisher notificationPublisher, EshopDbContext context)
        {
            _logger = logger;
            _notificationPublisher = notificationPublisher;
            _context = context;
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

            await _notificationPublisher.SendNotification(new NotificationMessage()
            {
                Source = nameof(GetProducts),
                Notification = $"Send {products.Count} products",
            });

            return Ok(products);
        }
    }
}