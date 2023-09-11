using System.Diagnostics;

namespace HandsOnTable.Poc.WebApp.Models
{
    [DebuggerDisplay("{ToString()}")]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Manufacturer Manufacturer { get; set; } = null!;
        public decimal PriceDollar { get; set; }
        public decimal PriceEuro => PriceDollar * 0.93m; 
        public bool Available { get; set; }
        public int Stock { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.None;
        public DateTime? AvailabilityDate { get; set; }
        public string? AvailabilityDateJson => AvailabilityDate?.ToString("dd-MM-yyyy");
        public string? Description { get; set; }
        public double? Rating { get; set; }

        public override string ToString() => $"{Name} ({Manufacturer.Name})";

    }
}