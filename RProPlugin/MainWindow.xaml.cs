using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.Json;
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
    List<Component> _jsonData = new List<Component>();
    ObservableCollection<Component> Components { get; set; }
    
    
    public MainWindow()
    {
        InitializeComponent();
        Components = new ObservableCollection<Component>
        {
            new Component { Name = "Объект1", Category = "Категория1" },
            new Component { Name = "Объект2", Category = "Категория2" },
            new Component { Name = "Объект3", Category = "Категория3"}
        };
        MyDataGrid.ItemsSource = Components;
        Console.WriteLine(Components[0].Name);
        DataContext = this;
    }

    private void ReadJsonOnClick(object sender, RoutedEventArgs e)
    {
        string json = File.ReadAllText("../../../specification2.json");
        _jsonData = JsonSerializer.Deserialize<List<Component>>(json);
        if (_jsonData.Count == 0)
        {
            Console.WriteLine("No specification found");
            return;
        }
        else
        {
            Console.WriteLine("Ok");
            Components.Clear();
            foreach (var component in _jsonData)
            {
                Components.Add(component);
            }
        }
    }
    
}