using System.Diagnostics.CodeAnalysis;
using HandsOnTable.Poc.WebApp.Models;

namespace HandsOnTable.Poc.WebApp.Controllers
{
    public interface IManufacturersController
    {
        bool Create(Manufacturer manufacturer);
        bool Delete(int id);
        ICollection<Manufacturer> GetAll();
        Manufacturer? GetById(int id);
        Manufacturer Upsert(int id, [DisallowNull] Manufacturer manufacturer);
    }
}