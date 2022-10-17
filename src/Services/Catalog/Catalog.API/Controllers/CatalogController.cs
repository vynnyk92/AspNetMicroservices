using System.Net;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IReadOnlyCollection<Product>))]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Product))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProductById(string id)
        {
            var product = await _productRepository.GetProduct(id);
            if (product is not null)
            {
                return Ok(product);
            }

            return NotFound();
        }

        [HttpGet("[action]/{categoryName}", Name = "GetProductByCategory")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IReadOnlyCollection<Product>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProductByCategory(string categoryName)
        {
            var products = await _productRepository.GetProductByCategory(categoryName);
            if (products.Any())
            {
                return Ok(products);
            }

            return NotFound();
        }

        [HttpGet("[action]/{prodName}", Name = "GetProductByProductName")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Product))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProductByProductName(string prodName)
        {
            var product = await _productRepository.GetProductByName(prodName);
            if (product is not null)
            {
                return Ok(product);
            }

            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Product))]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            await _productRepository.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Product))]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _productRepository.UpdateProduct(product));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productRepository.DeleteProduct(id);
            return NoContent();
        }
    }
}