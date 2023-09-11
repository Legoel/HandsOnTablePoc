using System.Diagnostics.CodeAnalysis;
using HandsOnTable.Poc.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HandsOnTable.Poc.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductRepository productFactory, ILogger<ProductsController> logger)
        {
            _productRepository = productFactory;
            _logger = logger;
        }

        [HttpPost(Name = "PostProduct")]
        public bool Create([FromBody] Product product) => _productRepository.Create(product);

        [HttpGet("{id}", Name = "GetProductById")]
        public Product? GetById([FromRoute] int id) => _productRepository.GetById(id);

        [HttpGet(Name = "GetProductList")]
        public ICollection<Product> GetAll()
        {
            _logger.LogInformation("Get all products");
            return _productRepository.GetAll();
        }

        [HttpPut("{id}", Name = "PutProduct")]
        public Product Upsert([FromRoute] int id, [DisallowNull, FromBody] Product product) => _productRepository.Upsert(id, product);

        [HttpDelete("{id}", Name = "DeleteProduct")]
        public bool Delete(int id) => _productRepository.Delete(id);
    }
}
