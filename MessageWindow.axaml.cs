using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DartSklad;

public partial class MessageWindow : Window
{
    public MessageWindow(string text)
    {
        InitializeComponent();
        textblock.Text = text;
    }
}