using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using Bogus;
using HandsOnTable.Poc.WebApp.Models;

namespace HandsOnTable.Poc.WebApp.Controllers
{
    public class ProductsController : IProductsController
    {
        private readonly ConcurrentDictionary<int, Product> _products = new();
        private readonly IManufacturersController _manufacturersController;

        public ProductsController(IManufacturersController manufacturersController)
        {
            _manufacturersController = manufacturersController;

            var pId = 0;

            static DateTime? setAvailability(Faker faker, Product product)
            {
                return product.Status switch
                {
                    OrderStatus.Registered or OrderStatus.Validated or OrderStatus.Ready => (DateTime?)DateTime.Today.AddDays(faker.Random.Int(10, 20)),
                    OrderStatus.Sent => (DateTime?)DateTime.Today.AddDays(faker.Random.Int(3, 10)),
                    OrderStatus.Received => (DateTime?)DateTime.Today.AddDays(faker.Random.Int(1, 3)),
                    _ => null,
                };
            }

            var fakerProduct = new Faker<Product>()
                .RuleFor(p => p.Id, f => pId++)
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Manufacturer, f => f.PickRandom(_manufacturersController.GetAll()))
                .RuleFor(p => p.PriceDollar, f => f.Random.Decimal(min: 100.0m, max: 1000.0m))
                .RuleFor(p => p.Available, f => f.Random.Bool(.8f))
                .RuleFor(p => p.Stock, (f, u) => u.Available ? f.Random.Number(min: 1, max: 100) : 0)
                .RuleFor(p => p.Status, (f, u) => u.Available ? OrderStatus.None : f.PickRandom<OrderStatus>())
                .RuleFor(p => p.AvailabilityDate, setAvailability)
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Rating, f => f.Random.Double(3.0f, 5.0));

            foreach (var item in fakerProduct.GenerateBetween(100, 200))
            {
                _products.TryAdd(item.Id, item);
            }
        }

        public bool Create(Product product) => _products.TryAdd(product.Id, product);

        public Product? GetById(int id) => _products.GetValueOrDefault(id);

        public ICollection<Product> GetAll() => _products.Values;

        public Product Upsert(int id, [DisallowNull] Product product) => _products.AddOrUpdate(id, product, (id, existing) => product);

        public bool Delete(int id) => _products.TryRemove(id, out _);
    }
}
