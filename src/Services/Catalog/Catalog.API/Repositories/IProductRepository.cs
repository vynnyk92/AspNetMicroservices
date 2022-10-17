using Catalog.API.Entities;

namespace Catalog.API.Repositories
{
    public interface IProductRepository
    {
        Task<IReadOnlyCollection<Product>> GetProducts();
        Task<Product> GetProduct(string id);
        Task<Product> GetProductByName(string name);
        Task<IReadOnlyCollection<Product>> GetProductByCategory(string categoryName);
        Task CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);
    }
}
