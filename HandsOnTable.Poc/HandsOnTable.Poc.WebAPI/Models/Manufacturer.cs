using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace HandsOnTable.Poc.WebAPI.Models
{
    [DebuggerDisplay("{ToString()}")]
    public class Manufacturer : INotifyPropertyChanged
    {
        private string _name = null!;
        private Country _country;

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

        public Country Country
        {
            get => _country;
            set
            {
                if (value != _country)
                {
                    _country = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public override string ToString() => $"{Name} ({Country})";

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}