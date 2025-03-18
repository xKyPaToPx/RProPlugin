using System.Windows;
using RProPlugin.Models;

namespace RProPlugin;

public partial class EditConveyorPriceWindow : Window
{
    private readonly Conveyor _conveyor;

    public EditConveyorPriceWindow(Conveyor conveyor)
    {
        InitializeComponent();
        _conveyor = conveyor;
        DataContext = _conveyor;
    }

    private void OkButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
        Close();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
} 