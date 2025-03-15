using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DartSklad;

public partial class StorageObjectsWindow : Window
{
    public StorageObjectsWindow()
    {
        InitializeComponent();
    }

    private void AddButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var addObjectWindow = new AddObjectToStorageWindow();
        addObjectWindow.Show();
        this.Close();
    }

    private void BackButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        // var eventWindow = new EventInfoWindow();
        // eventWindow.Show();
        this.Close();
    }
}