using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RProPlugin.Models;

namespace RProPlugin;


public partial class MainWindow : Window
{
    
    ObservableCollection<ComponentData> Components { get; set; } = new ObservableCollection<ComponentData>();
    private ObservableCollection<ComponentPrice> Prices { get; set; } = new ObservableCollection<ComponentPrice>();
    
    private string jsonFilePath = "../../../specification.json";
    private string jsonPricesFilePath = "../../../prices.json";
    
    public MainWindow()
    {
        InitializeComponent();
        LoadPrices();
        LoadJson();
        MyDataGrid.ItemsSource = Components;
        Console.WriteLine(Components[0].Name);
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
                    PropertyNameCaseInsensitive = true // Игнорируем регистр имен свойств
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
    
    private void LoadPrices()
    {
        if (File.Exists(jsonPricesFilePath))
        {
            string json = File.ReadAllText(jsonPricesFilePath);
            var prices = JsonSerializer.Deserialize<List<ComponentPrice>>(json);
            Prices.Clear();
            foreach (var price in prices)
            {
                Prices.Add(price);
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
            // Обновляем основную коллекцию после сохранения
            Components.Clear();
            foreach (var price in editWindow.Prices)
            {
                Prices.Add(price);
            }
            MessageBox.Show("Цены обновлены!");
        }
    }

    private void CalculatePrice_Click(object sender, RoutedEventArgs e)
    {
        decimal totalPrice = 0;
        var selectedComponents = Components.Where(c => c.IsSelected).ToList();
    
        foreach (var component in selectedComponents)
        {
            var price = Prices.FirstOrDefault(p => p.Name == component.Name)?.Price ?? 0;
            totalPrice += price;
        }
    
        MessageBox.Show($"Общая стоимость выбранных компонентов: {totalPrice:C}");
    }

    private void Exit_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
}