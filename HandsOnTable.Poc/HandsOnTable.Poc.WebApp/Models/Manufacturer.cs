using System.Diagnostics;

namespace HandsOnTable.Poc.WebApp.Models
{
    [DebuggerDisplay("{ToString()}")]
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Country Country { get; set; }

        public override string ToString() => $"{Name} ({Country})";
    }
}