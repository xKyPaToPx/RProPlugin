using System.ComponentModel;

namespace RProPlugin.Models;

public class Conveyor : INotifyPropertyChanged
{
    private string _name;
    private string _bomName;
    private double _conveyorWidth;
    private double _conveyorHeight;
    private double _conveyorLength;
    private string _presets;
    private bool _isSelected;
    private decimal _pricePerHeight = 0; // Значение по умолчанию для цены
    private decimal _pricePerWidth = 0;
    private decimal _pricePerLength = 0;
    private bool _hasPrice;

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    public string BomName
    {
        get => _bomName;
        set
        {
            _bomName = value;
            OnPropertyChanged(nameof(BomName));
        }
    }

    public double ConveyorWidth
    {
        get => _conveyorWidth;
        set
        {
            _conveyorWidth = value;
            OnPropertyChanged(nameof(ConveyorWidth));
        }
    }

    public double ConveyorHeight
    {
        get => _conveyorHeight;
        set
        {
            _conveyorHeight = value;
            OnPropertyChanged(nameof(ConveyorHeight));
        }
    }

    public double ConveyorLength
    {
        get => _conveyorLength;
        set
        {
            _conveyorLength = value;
            OnPropertyChanged(nameof(ConveyorLength));
        }
    }

    public string Presets
    {
        get => _presets;
        set
        {
            _presets = value;
            OnPropertyChanged(nameof(Presets));
        }
    }

    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            if (_isSelected != value)
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
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

    public bool HasPrice
    {
        get => _hasPrice;
        set
        {
            if (_hasPrice != value)
            {
                _hasPrice = value;
                OnPropertyChanged(nameof(HasPrice));
            }
        }
    }
    
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

