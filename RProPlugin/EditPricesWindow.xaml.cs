using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using RProPlugin.Models;

namespace RProPlugin;

public partial class EditPricesWindow : Window, INotifyDataErrorInfo
{
    public ObservableCollection<ComponentPrice> Prices { get; set; }
    private readonly string jsonFilePath;
    private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

    public EditPricesWindow(ObservableCollection<ComponentPrice> prices, string jsonFilePath)
    {
        InitializeComponent();
        this.jsonFilePath = jsonFilePath;
        Prices = new ObservableCollection<ComponentPrice>(
            prices.Select(c => new ComponentPrice { Name = c.Name, Price = c.Price }));
        DataContext = this;
    }

    private void AddPrice_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NewNameTextBox.Text) || NewNameTextBox.Text == "Введите имя")
        {
            MessageBox.Show("Введите корректное имя компонента!");
            return;
        }

        if (!decimal.TryParse(NewPriceTextBox.Text, out decimal price))
        {
            MessageBox.Show("Введите корректную цену!");
            return;
        }

        if (price < 0)
        {
            MessageBox.Show("Цена не может быть отрицательной!");
            return;
        }

        if (Prices.Any(p => p.Name == NewNameTextBox.Text))
        {
            MessageBox.Show("Компонент с таким именем уже существует!");
            return;
        }

        Prices.Add(new ComponentPrice { Name = NewNameTextBox.Text, Price = price });
        NewNameTextBox.Text = string.Empty; // Очищаем поле после добавления
        NewPriceTextBox.Text = "0.00";
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(Prices, options);
            File.WriteAllText(jsonFilePath, json);
            DialogResult = true;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
        }
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }

    // Реализация INotifyDataErrorInfo для валидации в DataGrid
    public bool HasErrors => _errors.Count > 0;

    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

    public System.Collections.IEnumerable GetErrors(string propertyName)
    {
        if (string.IsNullOrEmpty(propertyName) || !_errors.ContainsKey(propertyName))
            return null;
        return _errors[propertyName];
    }

    private void ValidatePrice(ComponentPrice price)
    {
        List<string> errors = new List<string>();
        if (price.Price < 0)
        {
            errors.Add("Цена не может быть отрицательной");
        }

        if (errors.Count > 0)
        {
            _errors[price.Name] = errors;
        }
        else
        {
            _errors.Remove(price.Name);
        }

        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(ComponentPrice.Price)));
    }

    private void NewNameTextBox_GotFocus(object sender, RoutedEventArgs e)
    {
        if (NewNameTextBox.Text == "Введите имя")
        {
            NewNameTextBox.Text = string.Empty;
            NewNameTextBox.Foreground = System.Windows.Media.Brushes.Black; // Восстанавливаем цвет текста
        }
    }

    private void NewNameTextBox_LostFocus(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NewNameTextBox.Text))
        {
            NewNameTextBox.Text = "Введите имя";
            NewNameTextBox.Foreground = System.Windows.Media.Brushes.Gray; // Серый цвет для placeholder
        }
    }
}