using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DartSklad;

public partial class CreateStorageWindow : Window
{
    public CreateStorageWindow()
    {
        InitializeComponent();
    }
    private void AddButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
      
    }

    private void BackButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        // var eventWindow = new EventInfoWindow();
        // eventWindow.Show();
        this.Close();
    }
}