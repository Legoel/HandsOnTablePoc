using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace HandsOnTable.Poc.WebAPI.Models
{
    public interface IManufacturerRepository
    {
        ConcurrentDictionary<int, Manufacturer> Manufacturers { get; }

        bool Create(Manufacturer manufacturer);
        bool Delete(int id);
        ICollection<Manufacturer> GetAll();
        Manufacturer? GetById(int id);
        Manufacturer? GetByName(string name);
        Manufacturer Upsert(int id, [DisallowNull] Manufacturer manufacturer);
    }
}