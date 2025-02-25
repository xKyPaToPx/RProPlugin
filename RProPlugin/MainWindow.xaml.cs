using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using RProPlugin.Models;

namespace RProPlugin;

public partial class MainWindow : Window
{
    ObservableCollection<ComponentData> Components { get; set; }
    private ObservableCollection<ComponentPrice> Prices { get; set; } = new ObservableCollection<ComponentPrice>();
    
    private string jsonFilePath = "../../../specification2.json";
    private string jsonPricesFilePath = "../../../prices.json";
    
    public MainWindow()
    {
        InitializeComponent();
        Components = new ObservableCollection<ComponentData>
        {
            new ComponentData { Name = "Объект1", BomName = "BOM1", Category = "Категория1" },
            new ComponentData { Name = "Объект2", BomName = "BOM2", Category = "Категория2" },
            new ComponentData { Name = "Объект3", BomName = "BOM3", Category = "Категория3" }
        };
        LoadPrices(); // Загружаем цены при инициализации
        SyncPricesWithComponents(); // Синхронизируем цены с компонентами
        MyDataGrid.ItemsSource = Components;
        DataContext = this;
    }

    private void LoadJson()
    {
        if (File.Exists(jsonFilePath))
        {
            try
            {
                string json = File.ReadAllText(jsonFilePath);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var components = JsonSerializer.Deserialize<List<ComponentData>>(json, options) ?? new List<ComponentData>();

                Components.Clear();
                foreach (var component in components)
                {
                    if (component != null)
                    {
                        Components.Add(component);
                    }
                }
                SyncPricesWithComponents(); // Синхронизируем цены после загрузки
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"Ошибка при парсинге JSON: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неизвестная ошибка при загрузке JSON: {ex.Message}");
            }
        }
        else
        {
            MessageBox.Show("JSON-файл не найден!");
        }
    }

    public void LoadPrices()
    {
        if (File.Exists(jsonPricesFilePath))
        {
            try
            {
                string json = File.ReadAllText(jsonPricesFilePath);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var loadedPrices = JsonSerializer.Deserialize<List<ComponentPrice>>(json) ?? new List<ComponentPrice>();
                
                Prices.Clear();
                foreach (var price in loadedPrices)
                {
                    Prices.Add(price);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке цен: {ex.Message}");
            }
        }
    }

    public void SyncPricesWithComponents()
    {
        foreach (var component in Components)
        {
            var price = Prices.FirstOrDefault(p => p.BomName == component.BomName);
            if (price != null)
            {
                component.Price = price.Price;
                component.HasPrice = true;
            }
            else
            {
                component.Price = 0;
                component.HasPrice = false;
            }
        }
    }

    private void ReadJson_Click(object sender, RoutedEventArgs e)
    {
        LoadJson();
    }

    private void EditPrices_Click(object sender, RoutedEventArgs e)
    {
        var editWindow = new EditPricesWindow(Prices, jsonPricesFilePath);
        if (editWindow.ShowDialog() == true)
        {
            LoadPrices(); // Перезагружаем цены после редактирования
            SyncPricesWithComponents(); // Синхронизируем цены с компонентами
            MessageBox.Show("Цены обновлены!");
        }
    }

    private void CalculatePrice_Click(object sender, RoutedEventArgs e)
    {
        decimal totalPrice = 0;
        var selectedComponents = Components.Where(c => c.IsSelected).ToList();
        
        foreach (var component in selectedComponents)
        {
            totalPrice += component.Price;
        }
        
        MessageBox.Show($"Общая стоимость выбранных компонентов: {totalPrice:C}");
    }

    private void Exit_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void AddPriceForComponent_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is string bomName)
        {
            var component = Components.FirstOrDefault(c => c.BomName == bomName);
            if (component != null)
            {
                var editPriceWindow = new EditSinglePriceWindow(component, Prices, jsonPricesFilePath);
                if (editPriceWindow.ShowDialog() == true)
                {
                    LoadPrices(); // Перезагружаем цены после редактирования
                    SyncPricesWithComponents(); // Синхронизируем цены с компонентами
                }
            }
        }
    }
}

// Конвертер для видимости (показывает элемент, если значение true)
public class BooleanToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }
        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

// Инверсный конвертер для видимости (показывает элемент, если значение false)
public class InverseBooleanToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return !boolValue ? Visibility.Visible : Visibility.Collapsed;
        }
        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}