using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DartSklad.Models;
using Microsoft.EntityFrameworkCore;

namespace DartSklad;

public partial class AllObjectsWindow : Window
{
    private WrapPanel objectsWrapPanel;

    public AllObjectsWindow()
    {
        InitializeComponent();
        LoadAllObjects();
    }
    private void LoadAllObjects()
    {
        objectsWrapPanel = this.FindControl<WrapPanel>("ObjectsWrapPanel");
        objectsWrapPanel.Children.Clear();

        using (var context = new TrpoContext())
        {
            // Загружаем SubjectType вместе с Subject
            var allObjects = context.Subjects
                .Include(s => s.SubjectType) 
                .OrderBy(s => s.Title)      // Сортируем по названию
                .ToList();

            foreach (var obj in allObjects)
            {
                var card = new AllObjectsPanelControl
                {
                    DataContext = obj,
                    Margin = new Thickness(10),
                    Width = 300 // Фиксированная ширина для одинаковых карточек
                };

                objectsWrapPanel.Children.Add(card);
            }
        }
    }
    private void CreateObjectButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var addObjWindow = new AddObjectWindow();       
        addObjWindow.Show();
        this.Close();
    }

    private void BackButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var addObjWindow = new MainWindow();
        addObjWindow.Show();
        this.Close();
    }
}