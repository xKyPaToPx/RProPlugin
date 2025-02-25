using System.ComponentModel;

namespace RProPlugin.Models;

public class ComponentData : INotifyPropertyChanged
{
    private string _name;
    private string _bomName;
    private string _category;
    private bool _isSelected = false;
    private decimal _price = 0; // Значение по умолчанию для цены
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

    public string BomName
    {
        get => _bomName;
        set
        {
            _bomName = value;
            OnPropertyChanged(nameof(BomName));
        }
    }

    public string Category
    {
        get => _category;
        set
        {
            _category = value;
            OnPropertyChanged(nameof(Category));
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

    public decimal Price
    {
        get => _price;
        set
        {
            _price = value;
            HasPrice = value > 0; // Обновляем HasPrice при изменении цены
            OnPropertyChanged(nameof(Price));
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