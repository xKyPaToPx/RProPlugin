using System.ComponentModel;

namespace RProPlugin.Models;

public class ComponentPrice : INotifyPropertyChanged
{
    private string _bomName;
    private decimal _price;

    public string BomName
    {
        get => _bomName;
        set
        {
            _bomName = value;
            OnPropertyChanged(nameof(BomName));
        }
    }

    public decimal Price
    {
        get => _price;
        set
        {
            _price = value;
            OnPropertyChanged(nameof(Price));
        }
    }
    

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}