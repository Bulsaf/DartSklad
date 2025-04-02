using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DartSklad;

public partial class MessageWindow : Window
{
    public MessageWindow()
    {
        InitializeComponent();
    }

    public MessageWindow(string text)
    {
        InitializeComponent();
        textblock.Text = text;
    }

    public MessageWindow(string title, string message)
    {
        InitializeComponent();
        this.Title = title;
        textblock.Text = message;
    }

    private void OkButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        this.Close();
    }
}