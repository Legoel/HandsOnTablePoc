using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using Bogus;

namespace HandsOnTable.Poc.WebAPI.Models
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        public ManufacturerRepository()
        {
            var mId = 0;

            var fakerManufacturer = new Faker<Manufacturer>()
                .StrictMode(true)
                .RuleFor(m => m.Id, f => mId++)
                .RuleFor(m => m.Name, f => f.Company.CompanyName())
                .RuleFor(m => m.Country, f => f.PickRandom<Country>());

            foreach (var item in fakerManufacturer.GenerateLazy(10))
            {
                Manufacturers.TryAdd(item.Id, item);
            }
        }

        public ConcurrentDictionary<int, Manufacturer> Manufacturers { get; } = new ConcurrentDictionary<int, Manufacturer>();

        public bool Create(Manufacturer manufacturer) => Manufacturers.TryAdd(manufacturer.Id, manufacturer);

        public Manufacturer? GetById(int id) => Manufacturers.GetValueOrDefault(id);

        public Manufacturer? GetByName(string name)
            => Manufacturers.Values.FirstOrDefault(kvp => string.Equals(kvp.Name, name, StringComparison.InvariantCultureIgnoreCase));

        public ICollection<Manufacturer> GetAll() => Manufacturers.Values;

        public Manufacturer Upsert(int id, [DisallowNull] Manufacturer manufacturer) => Manufacturers.AddOrUpdate(id, manufacturer, (id, existing) => manufacturer);

        public bool Delete(int id) => Manufacturers.TryRemove(id, out _);
    }
}