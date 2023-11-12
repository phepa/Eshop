using Eshop.Database;
using Eshop.Database.Entities;
using Eshop.Shared.Helpers;
using Eshop.Shared.Models.Requests.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Eshop.Internal.Services
{
    public class ProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly EshopDbContext _context;

        public ProductService(ILogger<ProductService> logger, EshopDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<List<Product>> GetProducts(string? query)
        {
            try
            {
                IQueryable<Product> result = _context.Products.AsQueryable();

                if (query.NotNullOrEmpty())
                {
                    result = result.Where(x => x.Name.Contains(query));
                }

                return await result.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("{Service} - ERROR - {Message}", nameof(GetProducts), ex.Message);
                throw;
            }
        }

        public async Task<Product> CreateProduct(CreateProductRequest request)
        {
            try
            {
                Product newProduct = new Product()
                {
                    Name = request.Name
                };

                _context.Products.Add(newProduct);

                await _context.SaveChangesAsync();

                return newProduct;
            }
            catch (Exception ex)
            {
                _logger.LogError("{Service} - ERROR - {Message}", nameof(CreateProduct), ex.Message);
                throw;
            }
        }

        public async Task<Product> UpdateProduct(UpdateProductRequest request)
        {
            try
            {
                Product? productToUpdate = _context.Products.SingleOrDefault(x => x.Id == request.Id);

                if (productToUpdate.NotNull())
                {
                    productToUpdate.Name = request.Name;
                    productToUpdate.CategoryId = request.CategoryId;

                    await _context.SaveChangesAsync();

                    return productToUpdate;
                }
                else
                {
                    throw new ArgumentException($"Product not found by id {request.Id}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("{Service} - ERROR - {Message}", nameof(UpdateProduct), ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                bool result = false;

                Product? productToDelete = _context.Products.SingleOrDefault(x => x.Id == productId);

                if (productToDelete.NotNull())
                {
                    _context.Products.Remove(productToDelete);

                    await _context.SaveChangesAsync();

                    result = true;
                }
                else
                {
                    throw new ArgumentException($"Product not found by id {productId}");
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("{Service} - ERROR - {Message}", nameof(GetProducts), ex.Message);
                throw;
            }
        }
    }
}
