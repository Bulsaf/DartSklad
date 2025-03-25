using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DartSklad.Models;

namespace DartSklad;

public partial class CreateEventWindow : Window
{
    private Guid parentId;
    public CreateEventWindow( Guid parentId)
    {
        InitializeComponent();
        this.parentId = parentId;
    }

    private void AddButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        string projectName = Title.Text ?? "";
        DateTimeOffset? entryDateOffset = EnterDate.SelectedDate;
        DateTimeOffset? startDateOffset = StartDate.SelectedDate;
        DateTimeOffset? endDateOffset = EndDate.SelectedDate;

        // Проверяем, что все поля заполнены
        if (string.IsNullOrWhiteSpace(projectName) || !entryDateOffset.HasValue || !startDateOffset.HasValue || !endDateOffset.HasValue)
        {
            Console.WriteLine("Все поля должны быть заполнены!");
            return;
        }

        // Преобразуем DateTimeOffset? в DateTime и явно указываем UTC
        DateTime entryDate = entryDateOffset.Value.UtcDateTime;
        DateTime startDate = startDateOffset.Value.UtcDateTime;
        DateTime endDate = endDateOffset.Value.UtcDateTime;

        // Создаем новый проект
        var newEvent= new Event
        {
            Title = projectName,
            EntryDate = entryDate,
            BeginDate = startDate,
            EndDate = endDate,
            ProjectId = parentId
            
        };

        // Сохраняем проект в базу данных
        SaveEvent(newEvent);
        
    }

    private void SaveEvent(Event newEvent)
    {
        using (var context = new TrpoContext())
        {
            context.Events.Add(newEvent);
            context.SaveChanges();
        }
        // Возвращаемся к проекту
        var mainWindow = new ProjectInfoWindow(parentId);
        mainWindow.Show();
        this.Close();
    }
    private void BackButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var mainWindow = new ProjectInfoWindow(parentId);
        mainWindow.Show();
        this.Close();
    }
}