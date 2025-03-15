using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DartSklad;

public partial class CreateEventWindow : Window
{
    public CreateEventWindow( Guid parentId)
    {
        InitializeComponent();
    }

    private void AddButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {

    }

    private void BackButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        // var mainWindow = new ProjectInfoWindow();
        // mainWindow.Show();
        this.Close();
    }
}