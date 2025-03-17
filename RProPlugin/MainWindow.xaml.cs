﻿using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using RProPlugin.Models;

namespace RProPlugin;

public partial class MainWindow : Window
{
    private ObservableCollection<ComponentData> Components { get; set; }
    private ObservableCollection<ComponentPrice> Prices { get; set; } = new ObservableCollection<ComponentPrice>();
    private ObservableCollection<Fencing> Fencings { get; set; } = new ObservableCollection<Fencing>();
    private ObservableCollection<FencingPrice> FencingPrices { get; set; } = new ObservableCollection<FencingPrice>();
    
    private string jsonFilePath = "../../../specificationMain.json";
    private string jsonPricesFilePath = "../../../prices.json";
    private string jsonFencingFilePath = "../../../specificationFenc.json";
    private string jsonFencingPricesFilePath = "../../../fencingPrices.json";
    
    public MainWindow()
    {
        Components = new ObservableCollection<ComponentData>();
        Prices = new ObservableCollection<ComponentPrice>();
        Fencings = new ObservableCollection<Fencing>();
        FencingPrices = new ObservableCollection<FencingPrice>();
        InitializeComponent();
        LoadPrices(); // Загружаем цены при инициализации
        LoadFencingPrices(); // Загружаем цены ограждений при инициализации
        LoadMain();
        LoadFencings(); // Загружаем ограждения при инициализации
        SyncPricesWithComponents(); // Синхронизируем цены с компонентами
        SyncPricesWithFencings(); // Синхронизируем цены с ограждениями
        MyDataGrid.ItemsSource = Components;
        FencesDataGrid.ItemsSource = Fencings;
        DataContext = this;
    }

    private void LoadFencings()
    {
        if (File.Exists(jsonFencingFilePath))
        {
            try
            {
                string json = File.ReadAllText(jsonFencingFilePath);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var fencings = JsonSerializer.Deserialize<List<Fencing>>(json, options) ?? new List<Fencing>();

                Fencings.Clear();
                foreach (var fencing in fencings)
                {
                    if (fencing != null)
                    {
                        Fencings.Add(fencing);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке спецификации ограждений: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void LoadMain()
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
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке спецификации: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

    private void LoadFencingPrices()
    {
        if (File.Exists(jsonFencingPricesFilePath))
        {
            try
            {
                string json = File.ReadAllText(jsonFencingPricesFilePath);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var loadedPrices = JsonSerializer.Deserialize<List<FencingPrice>>(json) ?? new List<FencingPrice>();
                
                FencingPrices.Clear();
                foreach (var price in loadedPrices)
                {
                    FencingPrices.Add(price);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке цен ограждений: {ex.Message}");
            }
        }
    }

    private void SaveFencingPrices()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(FencingPrices.ToList(), options);
            File.WriteAllText(jsonFencingPricesFilePath, json);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при сохранении цен ограждений: {ex.Message}");
        }
    }

    private void SyncPricesWithFencings()
    {
        foreach (var fencing in Fencings)
        {
            var price = FencingPrices.FirstOrDefault(p => p.BomName == fencing.BOMName);
            if (price != null)
            {
                fencing.PricePerHeight = price.PricePerHeight;
                fencing.PricePerWidth = price.PricePerWidth;
                fencing.HasPrice = true;
            }
            else
            {
                fencing.PricePerHeight = 0;
                fencing.PricePerWidth = 0;
                fencing.HasPrice = false;
            }
        }
    }

    private void ReadJson_Click(object sender, RoutedEventArgs e)
    {
        LoadMain();
    }

    private void EditPrices_Click(object sender, RoutedEventArgs e)
    {
        var editWindow = new EditAllPricesWindow(Prices, FencingPrices, jsonPricesFilePath, jsonFencingPricesFilePath);
        if (editWindow.ShowDialog() == true)
        {
            LoadPrices(); // Перезагружаем цены после редактирования
            LoadFencingPrices(); // Перезагружаем цены ограждений после редактирования
            SyncPricesWithComponents(); // Синхронизируем цены с компонентами
            SyncPricesWithFencings(); // Синхронизируем цены с ограждениями
            MessageBox.Show("Цены обновлены!");
        }
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
    
    private void CalculatePrice_Click(object sender, RoutedEventArgs e)
    {
        var calculationWindow = new CalculationWindow();
        if (calculationWindow.ShowDialog() == true)
        {
            decimal softwarePrice = calculationWindow.SoftwarePrice;
            decimal controlSystemPrice = calculationWindow.ControlSystemPrice;

            decimal totalPrice = softwarePrice + controlSystemPrice;

            // Экспортируем данные в Word
            ExportToWord(softwarePrice, controlSystemPrice, totalPrice);

            MessageBox.Show($"Общая стоимость: {totalPrice:N2} BYN");
        }
    }

    // Обновленный метод ExportToWord
    private void ExportToWord(decimal softwarePrice, decimal controlSystemPrice, decimal totalPrice)
    {
        try
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "Calculation_Report.docx");

            using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = new Body();

                // Создаем таблицу
                Table table = new Table();

                // Стиль таблицы (границы)
                TableProperties tableProps = new TableProperties(
                    new TableBorders(
                        new TopBorder() { Val = BorderValues.Single, Size = 4 },
                        new BottomBorder() { Val = BorderValues.Single, Size = 4 },
                        new LeftBorder() { Val = BorderValues.Single, Size = 4 },
                        new RightBorder() { Val = BorderValues.Single, Size = 4 },
                        new InsideHorizontalBorder() { Val = BorderValues.Single, Size = 4 },
                        new InsideVerticalBorder() { Val = BorderValues.Single, Size = 4 }
                    )
                );
                table.Append(tableProps);

                // Заголовки таблицы
                TableRow headerRow = new TableRow();
                headerRow.Append(
                    new TableCell(new Paragraph(new Run(new Text("Тип")))),
                    new TableCell(new Paragraph(new Run(new Text("Цена (BYN)"))))
                );
                table.Append(headerRow);

                // Строка с ценой разработки ПО
                TableRow softwareRow = new TableRow();
                softwareRow.Append(
                    new TableCell(new Paragraph(new Run(new Text("Разработка ПО")))),
                    new TableCell(new Paragraph(new Run(new Text(softwarePrice.ToString("N2")))))
                );
                table.Append(softwareRow);

                // Строка с ценой системы управления
                TableRow controlSystemRow = new TableRow();
                controlSystemRow.Append(
                    new TableCell(new Paragraph(new Run(new Text("Система управления")))),
                    new TableCell(new Paragraph(new Run(new Text(controlSystemPrice.ToString("N2")))))
                );
                table.Append(controlSystemRow);

                // Строка "Итого"
                TableRow totalRow = new TableRow();
                totalRow.Append(
                    new TableCell(new Paragraph(new Run(new Text("Итого")))),
                    new TableCell(new Paragraph(new Run(new Text(totalPrice.ToString("N2")))))
                );
                // Делаем текст "Итого" жирным
                totalRow.Descendants<Run>().First().RunProperties = new RunProperties(new Bold());
                table.Append(totalRow);

                body.Append(table);
                mainPart.Document.Append(body);
                mainPart.Document.Save();
            }

            MessageBox.Show($"Документ сохранён на рабочий стол: {filePath}");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при создании документа Word: {ex.Message}");
        }
    }
   
    private void AddPriceForFencing_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is string bomName)
        {
            var fencing = Fencings.FirstOrDefault(f => f.BOMName == bomName);
            if (fencing != null)
            {
                var editPriceWindow = new EditFencingPriceWindow(fencing);
                if (editPriceWindow.ShowDialog() == true)
                {
                    var existingPrice = FencingPrices.FirstOrDefault(p => p.BomName == fencing.BOMName);
                    if (existingPrice != null)
                    {
                        existingPrice.PricePerHeight = fencing.PricePerHeight;
                        existingPrice.PricePerWidth = fencing.PricePerWidth;
                    }
                    else
                    {
                        FencingPrices.Add(new FencingPrice
                        {
                            BomName = fencing.BOMName,
                            PricePerHeight = fencing.PricePerHeight,
                            PricePerWidth = fencing.PricePerWidth
                        });
                    }
                    SaveFencingPrices();
                    // Добавляем вызов синхронизации для обновления всех ограждений
                    SyncPricesWithFencings();
                }
            }
        }
    }

    private void SelectAllComponents_Click(object sender, RoutedEventArgs e)
    {
        // Проверяем, все ли компоненты выбраны
        bool allSelected = Components.All(c => c.IsSelected);
        // Устанавливаем противоположное значение для всех
        foreach (var component in Components)
        {
            component.IsSelected = !allSelected;
        }
    }

    private void SelectAllCovers_Click(object sender, RoutedEventArgs e)
    {
        if (CoversDataGrid.ItemsSource is IEnumerable<dynamic> covers)
        {
            // Проверяем, все ли ковейры выбраны
            bool allSelected = true;
            foreach (dynamic cover in covers)
            {
                if (!cover.IsSelected)
                {
                    allSelected = false;
                    break;
                }
            }
            // Устанавливаем противоположное значение для всех
            foreach (dynamic cover in covers)
            {
                cover.IsSelected = !allSelected;
            }
        }
    }

    private void SelectAllFences_Click(object sender, RoutedEventArgs e)
    {
        if (FencesDataGrid.ItemsSource is IEnumerable<dynamic> fences)
        {
            // Проверяем, все ли ограждения выбраны
            bool allSelected = true;
            foreach (dynamic fence in fences)
            {
                if (!fence.IsSelected)
                {
                    allSelected = false;
                    break;
                }
            }
            // Устанавливаем противоположное значение для всех
            foreach (dynamic fence in fences)
            {
                fence.IsSelected = !allSelected;
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