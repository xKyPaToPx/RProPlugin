using System;
using System.Globalization;
using System.IO;
using System.Windows;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace RProPlugin
{
    public partial class CalculationWindow 
    {
        public decimal SoftwarePrice { get; private set; }
        public decimal ControlSystemPrice { get; private set; }

        public CalculationWindow()
        {
            InitializeComponent();
        }

        // Обработчик кнопки выбора Excel-файла
        private void SelectExcelFile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                Title = "Выберите Excel-файл"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                try
                {
                    // Извлекаем цену из Excel-файла
                    ControlSystemPrice = ExtractPriceFromExcel(filePath);
                    ControlSystemPriceTextBox.Text = ControlSystemPrice.ToString("N2") + " BYN";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при чтении Excel-файла: {ex.Message}");
                }
            }
        }

        // Метод для извлечения цены из Excel-файла
        private decimal ExtractPriceFromExcel(string filePath)
        {
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(filePath, false))
            {
                WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

                // Находим индекс столбца "Сумма, руб"
                int sumColumnIndex = -1;
                bool isHeaderRow = true;

                foreach (Row row in sheetData.Elements<Row>())
                {
                    if (isHeaderRow)
                    {
                        // Ищем заголовок "Сумма, руб" в первой строке
                        foreach (Cell cell in row.Elements<Cell>())
                        {
                            string cellValue = GetCellValue(cell, workbookPart);
                            if (cellValue == "Сумма, руб.")
                            {
                                sumColumnIndex = GetColumnIndex(cell.CellReference);
                                break;
                            }
                        }
                        isHeaderRow = false;
                    }
                    else
                    {
                        // Ищем строку с текстом "Итого"
                        foreach (Cell cell in row.Elements<Cell>())
                        {
                            string cellValue = GetCellValue(cell, workbookPart);
                            if (cellValue.Contains("Итого"))
                            {
                                // Получаем значение из столбца "Сумма, руб"
                                Cell sumCell = row.Elements<Cell>().FirstOrDefault(c => GetColumnIndex(c.CellReference) == sumColumnIndex);
                                if (sumCell != null)
                                {
                                    string sumValue = GetCellValue(sumCell, workbookPart);
                                    if (decimal.TryParse(sumValue, out decimal price))
                                    {
                                        MessageBox.Show(price.ToString());
                                        return price;
                                    }
                                }
                                break;
                            }
                        }
                    }
                }

                throw new Exception("Не удалось найти строку 'Итого' или столбец 'Сумма, руб'.");
            }
        }

        // Метод для получения индекса столбца по его буквенному обозначению (A, B, C, ...)
        private int GetColumnIndex(string cellReference)
        {
            string columnName = new string(cellReference.Where(char.IsLetter).ToArray());
            int index = 0;
            foreach (char ch in columnName)
            {
                index = (index * 26) + (ch - 'A' + 1);
            }
            return index - 1; // Индексация с 0
        }

        // Метод для получения значения ячейки
        private string GetCellValue(Cell cell, WorkbookPart workbookPart)
        {
            if (cell == null || cell.CellValue == null)
            {
                return string.Empty;
            }

            string cellValue = cell.CellValue.Text;

            // Если ячейка содержит формулу, используем SharedStringTable для получения вычисленного значения
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                SharedStringTablePart stringTablePart = workbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
                if (stringTablePart != null)
                {
                    return stringTablePart.SharedStringTable.ElementAt(int.Parse(cellValue)).InnerText;
                }
            }
            // Если ячейка содержит число или результат вычисления формулы
            else if (cell.DataType == null || cell.DataType.Value == CellValues.Number)
            {
                // Используем инвариантную культуру для парсинга числа с точкой
                if (double.TryParse(cellValue, NumberStyles.Any, CultureInfo.InvariantCulture, out double numericValue))
                {
                    numericValue = Math.Round(numericValue, 2); // Округляем до 2 знаков
                    return numericValue.ToString();
                }
            }

            return string.Empty;
        }

        // Обработчик кнопки "Рассчитать"
        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(SoftwarePriceTextBox.Text, out decimal softwarePrice))
            {
                SoftwarePrice = softwarePrice;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Введите корректную цену разработки ПО!");
            }
        }

        // Обработчик кнопки "Отмена"
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}