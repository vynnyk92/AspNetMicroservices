using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }


        public async Task<IReadOnlyCollection<Product>> GetProducts()
        {
            return await _context.Products
                .Find(p => true).ToListAsync();
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _context.Products
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Product> GetProductByName(string name)
        {
            return await _context.Products
                .Find(p => p.Name == name)
                .FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyCollection<Product>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);

            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var result = await _context.Products.ReplaceOneAsync(p => p.Id == product.Id, replacement: product);
            return result.IsAcknowledged && result.ModifiedCount == 1;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var result = await _context.Products.DeleteOneAsync(p => p.Id == id);
            return result.IsAcknowledged && result.DeletedCount == 1;
        }
    }
}
