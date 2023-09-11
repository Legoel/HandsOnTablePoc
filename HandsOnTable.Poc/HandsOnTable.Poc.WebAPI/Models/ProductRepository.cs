using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using Bogus;

namespace HandsOnTable.Poc.WebAPI.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly IManufacturerRepository _manufacturerFactory;

        public ProductRepository(IManufacturerRepository manufacturerFactory)
        {
            _manufacturerFactory = manufacturerFactory;
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
                .RuleFor(p => p.Manufacturer, f => f.PickRandom(_manufacturerFactory.Manufacturers.Values))
                .RuleFor(p => p.PriceDollar, f => f.Random.Decimal(min: 100.0m, max: 1000.0m))
                .RuleFor(p => p.Available, f => f.Random.Bool(.8f))
                .RuleFor(p => p.Stock, (f, u) => u.Available ? f.Random.Number(min: 1, max: 100) : 0)
                .RuleFor(p => p.Status, (f, u) => u.Available ? OrderStatus.None : f.PickRandom<OrderStatus>())
                .RuleFor(p => p.AvailabilityDate, setAvailability)
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Rating, f => f.Random.Double(3.0f, 5.0));

            foreach (var item in fakerProduct.GenerateBetween(100, 200))
            {
                Products.TryAdd(item.Id, item);
            }
        }

        public ConcurrentDictionary<int, Product> Products { get; } = new ConcurrentDictionary<int, Product>();

        public bool Create(Product product) => Products.TryAdd(product.Id, product);

        public Product? GetById(int id) => Products.GetValueOrDefault(id);

        public ICollection<Product> GetAll() => Products.Values;

        public Product Upsert(int id, [DisallowNull] Product product) => Products.AddOrUpdate(id, product, (id, existing) => product);

        public bool Delete(int id) => Products.TryRemove(id, out _);
    }
}