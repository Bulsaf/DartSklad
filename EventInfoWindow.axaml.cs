using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DartSklad;

public partial class EventInfoWindow : Window
{
    public EventInfoWindow()
    {
        InitializeComponent();
    }

    private void AddButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var addWindow = new CreateStorageWindow();
        addWindow.Show();
        this.Close();
    }
    
    private void BackButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var projWindow = new ProjectInfoWindow();
        projWindow.Show();
        this.Close();
    }

    private void CardButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var projWindow = new StorageObjectsWindow();
        projWindow.Show();
        this.Close();
    }
}