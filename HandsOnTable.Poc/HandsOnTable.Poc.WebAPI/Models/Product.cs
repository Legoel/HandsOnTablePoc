using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace HandsOnTable.Poc.WebAPI.Models
{
    [DebuggerDisplay("{ToString()}")]
    public class Product : INotifyPropertyChanged
    {
        private string _name = null!;
        private Manufacturer _manufacturer = null!;
        private decimal _priceDollar;
        private bool _available;
        private int _stock;
        private OrderStatus _status = OrderStatus.None;
        private DateTime? _availabilityDate;
        private string? _description;
        private double? _rating;

        public int Id { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Manufacturer Manufacturer
        {
            get => _manufacturer;
            set
            {
                if (value != _manufacturer)
                {
                    _manufacturer = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public decimal PriceDollar
        {
            get => _priceDollar;
            set
            {
                if (value != _priceDollar)
                {
                    _priceDollar = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public decimal PriceEuro => PriceDollar * 0.93m;

        public bool Available
        {
            get => _available;
            set
            {
                if (value ^ _available)
                {
                    _available = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Stock
        {
            get => _stock;
            set
            {
                if (value != _stock)
                {
                    _stock = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public OrderStatus Status
        {
            get => _status;
            set
            {
                if (value != _status)
                {
                    _status = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime? AvailabilityDate
        {
            get => _availabilityDate;
            set
            {
                if (value != _availabilityDate)
                {
                    _availabilityDate = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string? AvailabilityDateJson => AvailabilityDate?.ToString("dd-MM-yyyy");

        public string? Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double? Rating
        {
            get => _rating;
            set
            {
                if (value != _rating)
                {
                    _rating = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public override string ToString() => $"{Name} ({Manufacturer.Name})";

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}