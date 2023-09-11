using System.Diagnostics.CodeAnalysis;
using HandsOnTable.Poc.WebApp.Models;

namespace HandsOnTable.Poc.WebApp.Controllers
{
    public interface IProductsController
    {
        bool Create(Product product);
        bool Delete(int id);
        ICollection<Product> GetAll();
        Product? GetById(int id);
        Product Upsert(int id, [DisallowNull] Product product);
    }
}