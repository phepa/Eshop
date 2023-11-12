using Eshop.API.Publishers;
using Eshop.Database;
using Eshop.Database.Entities;
using Eshop.Internal.Services;
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
        private readonly ProductService _productService;
        private readonly NotificationPublisher _notificationPublisher;

        public ProductController(ILogger<ProductController> logger, NotificationPublisher notificationPublisher, EshopDbContext context, ProductService productService)
        {
            _logger = logger;
            _notificationPublisher = notificationPublisher;
            _context = context;
            _productService = productService;
        }

        /// <summary>Get products filtered by query name.</summary>
        /// <param name="query">Search query</param>
        /// <returns>See <see cref="GetProductsResponse"/>for HTTP 200</returns>
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetProductsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProducts(string? query)
        {
            List<Product> result = await _productService.GetProducts(query);

            return Ok(result);
        }

        /// <summary>Schedule new operation under current model version lock.</summary>
        /// <param name="request">See <see cref="Product"/></param>
        /// <returns>See <see cref="Product"/>for HTTP 200</returns>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct(CreateProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product result = await _productService.CreateProduct(request);

            await _notificationPublisher.SendNotification(new NotificationMessage()
            {
                Source = nameof(GetProducts),
                Notification = $"{nameof(CreateProduct)} - product created - {result}",
            });

            return Ok(result);
        }

        /// <summary>Schedule new operation under current model version lock.</summary>
        /// <param name="request">See <see cref="Product"/></param>
        /// <returns>See <see cref="Product"/>for HTTP 200</returns>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProduct(UpdateProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product result = await _productService.UpdateProduct(request);

            await _notificationPublisher.SendNotification(new NotificationMessage()
            {
                Source = nameof(GetProducts),
                Notification = $"{nameof(UpdateProduct)} - product updated - {result}",
            });

            return Ok(result);
        }

        /// <summary>Schedule new operation under current model version lock.</summary>
        /// <param name="request">See <see cref="GetProductsResponse"/></param>
        /// <returns>See <see cref="GetProductsResponse"/>for HTTP 200</returns>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool result = await _productService.DeleteProduct(productId);

            await _notificationPublisher.SendNotification(new NotificationMessage()
            {
                Source = nameof(GetProducts),
                Notification = $"{nameof(DeleteProduct)} - product deleted",
            });

            return Ok(result);
        }
    }
}