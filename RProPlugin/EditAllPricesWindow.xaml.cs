using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using RProPlugin.Models;

namespace RProPlugin;

public partial class EditAllPricesWindow
{
    private readonly ObservableCollection<ComponentPrice> _componentPrices;
    private readonly ObservableCollection<FencingPrice> _fencingPrices;
    private readonly string _componentPricesPath;
    private readonly string _fencingPricesPath;

    public ObservableCollection<ComponentPrice> ComponentPrices => _componentPrices;
    public ObservableCollection<FencingPrice> FencingPrices => _fencingPrices;

    public EditAllPricesWindow(ObservableCollection<ComponentPrice> componentPrices, 
                              ObservableCollection<FencingPrice> fencingPrices,
                              string componentPricesPath,
                              string fencingPricesPath)
    {
        InitializeComponent();
        _componentPrices = componentPrices;
        _fencingPrices = fencingPrices;
        _componentPricesPath = componentPricesPath;
        _fencingPricesPath = fencingPricesPath;
        DataContext = this;
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            // Сохраняем цены основных компонентов
            var componentOptions = new JsonSerializerOptions { WriteIndented = true };
            string componentJson = JsonSerializer.Serialize(_componentPrices.ToList(), componentOptions);
            File.WriteAllText(_componentPricesPath, componentJson);

            // Сохраняем цены ограждений
            var fencingOptions = new JsonSerializerOptions { WriteIndented = true };
            string fencingJson = JsonSerializer.Serialize(_fencingPrices.ToList(), fencingOptions);
            File.WriteAllText(_fencingPricesPath, fencingJson);

            DialogResult = true;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при сохранении цен: {ex.Message}", "Ошибка", 
                          MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
} 