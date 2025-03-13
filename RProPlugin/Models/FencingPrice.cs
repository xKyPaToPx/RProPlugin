using System.ComponentModel;

namespace RProPlugin.Models;

public class FencingPrice : INotifyPropertyChanged
{
    private string _bomName;
    private decimal _pricePerHeight;
    private decimal _pricePerWidth;

    public string BomName
    {
        get => _bomName;
        set
        {
            _bomName = value;
            OnPropertyChanged(nameof(BomName));
        }
    }

    public decimal PricePerHeight
    {
        get => _pricePerHeight;
        set
        {
            _pricePerHeight = value;
            OnPropertyChanged(nameof(PricePerHeight));
        }
    }

    public decimal PricePerWidth
    {
        get => _pricePerWidth;
        set
        {
            _pricePerWidth = value;
            OnPropertyChanged(nameof(PricePerWidth));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
} 