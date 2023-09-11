using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace HandsOnTable.Poc.WebAPI.Models
{
    public interface IProductRepository
    {
        ConcurrentDictionary<int, Product> Products { get; }

        bool Create(Product product);
        bool Delete(int id);
        ICollection<Product> GetAll();
        Product? GetById(int id);
        Product Upsert(int id, [DisallowNull] Product product);
    }
}