using Avalonia.Controls;

namespace DartSklad
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AllObjectsButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var allObjWindow = new AllObjectsWindow();
            allObjWindow.ShowDialog(this);
        }
    }
}