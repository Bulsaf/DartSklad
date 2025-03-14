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
            allObjWindow.Show();
            this.Close();
        }

        private void AddButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var createProjectWindow = new CreateProjectWindow();
            createProjectWindow.Show();
            this.Close();
        }

        private void CardButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var createProjectWindow = new ProjectInfoWindow();
            createProjectWindow.Show();
            this.Close();
        }
    }
}