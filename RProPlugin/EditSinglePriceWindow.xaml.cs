using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using RProPlugin.Models;

namespace RProPlugin;

public partial class EditSinglePriceWindow : Window
{
    private readonly ComponentData _component;
    private readonly ObservableCollection<ComponentPrice> _prices;
    private readonly string _jsonPricesFilePath;

    public EditSinglePriceWindow(ComponentData component, ObservableCollection<ComponentPrice> prices, string jsonPricesFilePath)
    {
        InitializeComponent();
        _component = component;
        _prices = prices;
        _jsonPricesFilePath = jsonPricesFilePath;
        ComponentNameTextBlock.Text = $"{component.BomName}";
        PriceTextBox.Text = component.Price.ToString("0.00");
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        if (decimal.TryParse(PriceTextBox.Text, out decimal price) && price >= 0)
        {
            _component.Price = price;
            _component.HasPrice = true;

            // Обновляем или добавляем цену в коллекцию Prices
            var existingPrice = _prices.FirstOrDefault(p => p.BomName == _component.BomName);
            if (existingPrice != null)
            {
                existingPrice.Price = price;
            }
            else
            {
                _prices.Add(new ComponentPrice { BomName = _component.BomName, Price = price });
            }

            // Сохраняем обновленные цены в JSON
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(_prices, options);
                File.WriteAllText(_jsonPricesFilePath, json);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
            }
        }
        else
        {
            MessageBox.Show("Введите корректную цену (положительное число)!");
        }
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}