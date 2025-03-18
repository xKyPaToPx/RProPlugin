using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using RProPlugin.Models;

namespace RProPlugin;

public partial class EditAllPricesWindow : Window
{
    private readonly ObservableCollection<ComponentPrice> _prices;
    private readonly ObservableCollection<FencingPrice> _fencingPrices;
    private readonly ObservableCollection<ConveyorPrice> _conveyorPrices;
    private readonly string _pricesFilePath;
    private readonly string _fencingPricesFilePath;
    private readonly string _conveyorPricesFilePath;

    public EditAllPricesWindow(ObservableCollection<ComponentPrice> prices, 
                             ObservableCollection<FencingPrice> fencingPrices,
                             ObservableCollection<ConveyorPrice> conveyorPrices,
                             string pricesFilePath,
                             string fencingPricesFilePath,
                             string conveyorPricesFilePath)
    {
        InitializeComponent();
        _prices = new ObservableCollection<ComponentPrice>(prices);
        _fencingPrices = new ObservableCollection<FencingPrice>(fencingPrices);
        _conveyorPrices = new ObservableCollection<ConveyorPrice>(conveyorPrices);
        _pricesFilePath = pricesFilePath;
        _fencingPricesFilePath = fencingPricesFilePath;
        _conveyorPricesFilePath = conveyorPricesFilePath;

        MainPricesDataGrid.ItemsSource = _prices;
        FencingPricesDataGrid.ItemsSource = _fencingPrices;
        ConveyorPricesGrid.ItemsSource = _conveyorPrices;
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            
            // Сохраняем цены компонентов
            string json = JsonSerializer.Serialize(_prices.ToList(), options);
            File.WriteAllText(_pricesFilePath, json);
            
            // Сохраняем цены ограждений
            json = JsonSerializer.Serialize(_fencingPrices.ToList(), options);
            File.WriteAllText(_fencingPricesFilePath, json);
            
            // Сохраняем цены конвейеров
            json = JsonSerializer.Serialize(_conveyorPrices.ToList(), options);
            File.WriteAllText(_conveyorPricesFilePath, json);

            DialogResult = true;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при сохранении цен: {ex.Message}");
        }
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
} 