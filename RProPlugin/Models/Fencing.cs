using System.ComponentModel;

namespace RProPlugin.Models;
    
public class Fencing : INotifyPropertyChanged
{
    private string _name;
    private string _bomName;
    private double _height;
    private double _length;
    private double _standHeight;
    private bool _isSelected;
    private decimal _pricePerHeight = 0; // Значение по умолчанию для цены
    private decimal _pricePerWidth = 0; // Значение по умолчанию для цены
    private bool _hasPrice = false; // Флаг наличия цены

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    public string BOMName
    {
        get => _bomName;
        set
        {
            _bomName = value;
            OnPropertyChanged(nameof(BOMName));
        }
    }

    public double FenceHeight
    {
        get => _height;
        set
        {
            _height = value;
            OnPropertyChanged(nameof(FenceHeight));
        }
    }

    public double FenceLength
    {
        get => _length;
        set
        {
            _length = value; 
            OnPropertyChanged(nameof(FenceLength));
        }
    }
    
    public double Height
    {
        get => _height;
        set
        {
            _height = value;
            OnPropertyChanged(nameof(FenceHeight));
        }
    }

    public double Width
    {
        get => _length;
        set
        {
            _length = value; 
            OnPropertyChanged(nameof(FenceLength));
        }
    }

    public double StandHeight
    {
        get => _standHeight;
        set
        {
            _standHeight = value;
            OnPropertyChanged(nameof(StandHeight));
        }
    }

    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            _isSelected = value;
            OnPropertyChanged(nameof(IsSelected));
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

    public bool HasPrice
    {
        get => _hasPrice;
        set
        {
            _hasPrice = value;
            OnPropertyChanged(nameof(HasPrice));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}