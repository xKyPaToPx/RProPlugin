using System.Windows;
using RProPlugin.Models;

namespace RProPlugin;

public partial class EditFencingPriceWindow : Window
{
    private readonly Fencing _fencing;

    public EditFencingPriceWindow(Fencing fencing)
    {
        InitializeComponent();
        _fencing = fencing;
        
        // Заполняем текстовые поля текущими значениями
        PricePerHeightTextBox.Text = _fencing.PricePerHeight.ToString();
        PricePerWidthTextBox.Text = _fencing.PricePerWidth.ToString();
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (decimal.TryParse(PricePerHeightTextBox.Text, out decimal pricePerHeight) &&
            decimal.TryParse(PricePerWidthTextBox.Text, out decimal pricePerWidth))
        {
            _fencing.PricePerHeight = pricePerHeight;
            _fencing.PricePerWidth = pricePerWidth;
            DialogResult = true;
            Close();
        }
        else
        {
            MessageBox.Show("Пожалуйста, введите корректные числовые значения для цен.", "Ошибка", 
                          MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
} 