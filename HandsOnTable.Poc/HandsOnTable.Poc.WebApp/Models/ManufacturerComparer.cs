using System.Diagnostics.CodeAnalysis;

namespace HandsOnTable.Poc.WebApp.Models
{
    public class ManufacturerComparer : IEqualityComparer<Manufacturer>
    {
        public bool Equals(Manufacturer? x, Manufacturer? y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x is null || y is null)
                return false;

            return x.Id == y .Id;
        }

        public int GetHashCode([DisallowNull] Manufacturer obj) => obj.Id.GetHashCode();
    }
}