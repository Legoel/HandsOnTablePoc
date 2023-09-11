using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using Bogus;
using HandsOnTable.Poc.WebApp.Models;

namespace HandsOnTable.Poc.WebApp.Controllers
{
    public class ManufacturersController : IManufacturersController
    {
        private readonly ConcurrentDictionary<int, Manufacturer> _manufacturers = new();

        public ManufacturersController()
        {
            var mId = 0;

            var fakerManufacturer = new Faker<Manufacturer>()
                .StrictMode(true)
                .RuleFor(m => m.Id, f => mId++)
                .RuleFor(m => m.Name, f => f.Company.CompanyName())
                .RuleFor(m => m.Country, f => f.PickRandom<Country>());

            foreach (var item in fakerManufacturer.GenerateLazy(10))
            {
                _manufacturers.TryAdd(item.Id, item);
            }
        }

        public bool Create(Manufacturer manufacturer) => _manufacturers.TryAdd(manufacturer.Id, manufacturer);

        public Manufacturer? GetById(int id) => _manufacturers.GetValueOrDefault(id);

        public ICollection<Manufacturer> GetAll() => _manufacturers.Values;

        public Manufacturer Upsert(int id, [DisallowNull] Manufacturer manufacturer) => _manufacturers.AddOrUpdate(id, manufacturer, (id, existing) => manufacturer);

        public bool Delete(int id) => _manufacturers.TryRemove(id, out _);
    }
}
