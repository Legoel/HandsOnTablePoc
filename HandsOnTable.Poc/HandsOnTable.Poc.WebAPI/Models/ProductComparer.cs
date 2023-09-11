using System.Diagnostics.CodeAnalysis;

namespace HandsOnTable.Poc.WebAPI.Models
{
    public class ProductComparer : IEqualityComparer<Product>
    {
        public bool Equals(Product? x, Product? y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x is null || y is null)
                return false;

            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] Product obj) => obj.Id.GetHashCode();
    }
}