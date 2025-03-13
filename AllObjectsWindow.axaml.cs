using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DartSklad;

public partial class AllObjectsWindow : Window
{

    public AllObjectsWindow()
    {
        InitializeComponent();
    }

    private void CreateObjectButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var addObjWindow = new AddObjectWindow();       
        addObjWindow.ShowDialog(this);
    }

    private void BackButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        this.Close();
    }
}