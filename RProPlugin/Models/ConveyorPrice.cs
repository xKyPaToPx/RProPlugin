using System.ComponentModel;

namespace RProPlugin.Models;

public class ConveyorPrice : INotifyPropertyChanged
{
    private string _presets;
    private decimal _pricePerHeight;
    private decimal _pricePerWidth;
    private decimal _pricePerLength;

    public string Presets
    {
        get => _presets;
        set
        {
            _presets = value;
            OnPropertyChanged(nameof(Presets));
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

    public decimal PricePerLength
    {
        get => _pricePerLength;
        set
        {
            _pricePerLength = value;
            OnPropertyChanged(nameof(PricePerLength));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
} 