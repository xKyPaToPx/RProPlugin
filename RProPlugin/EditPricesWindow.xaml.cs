using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
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
            prices.Select(c => new ComponentPrice { BomName = c.BomName, Price = c.Price }));
        DataContext = this;
    }

    private void AddPrice_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NewNameTextBox.Text))
        {
            MessageBox.Show("Введите корректное BOM имя компонента!");
            return;
        }

        if (string.IsNullOrWhiteSpace(NewPriceTextBox.Text) || NewPriceTextBox.Text == "Введите цену")
        {
            MessageBox.Show("Введите корректную цену!");
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

        if (Prices.Any(p => p.BomName == NewNameTextBox.Text))
        {
            MessageBox.Show("Компонент с таким BOM именем уже существует!");
            return;
        }

        Prices.Add(new ComponentPrice { BomName = NewNameTextBox.Text, Price = price });
        NewNameTextBox.Text = string.Empty; // Очищаем поле после добавления
        NewPriceTextBox.Text = string.Empty; // Очищаем поле после добавления
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(Prices, options);
            File.WriteAllText(jsonFilePath, json);

            // Синхронизация с основным окном (MainWindow)
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.LoadPrices(); // Перезагружаем цены
                mainWindow.SyncPricesWithComponents(); // Синхронизируем цены с компонентами
            }

            DialogResult = true; // Указываем, что изменения сохранены
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
        }
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false; // Отмена изменений
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
            _errors[price.BomName] = errors; // Обновлено с BomName
        }
        else
        {
            _errors.Remove(price.BomName); // Обновлено с BomName
        }

        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(ComponentPrice.Price)));
    }
}