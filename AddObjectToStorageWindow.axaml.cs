using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DartSklad;

public partial class AddObjectToStorageWindow : Window
{
    public AddObjectToStorageWindow()
    {
        InitializeComponent();
    }

    private void AddButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {

    }

    private void BackButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var objsWindow = new StorageObjectsWindow();
        objsWindow.Show();
        this.Close();
    }
}