using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DartSklad.Models;
using Microsoft.EntityFrameworkCore;

namespace DartSklad;

public partial class ProjectInfoWindow : Window
{
    private Guid projectId;
    
    public ProjectInfoWindow(Guid id)
    {
        Console.WriteLine(id);

        projectId = id;
        InitializeComponent();

        try
        {
            var events = GetEventsByProjectId(id);

            if (events.Count > 0)
            {
                foreach (var ev in events)
                {
                    var button = new Button
                    {
                        Classes = { "uc" },
                        Content = new EventCardControl
                        {
                            DataContext = ev
                        },
                        Tag = ev 
                    };

                    button.Click += CardButton_Click; 
                    EventsWrapPanel.Children.Add(button);
                }
            }
            else
            {
                // Добавляем сообщение, если нет мероприятий
                var noEventsLabel = new TextBlock
                {
                    Text = "Нет мероприятий",
                    FontSize = 16,
                    Margin = new Thickness(10)
                };
                EventsWrapPanel.Children.Add(noEventsLabel);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке мероприятий: {ex.Message}");
            // Добавляем сообщение об ошибке
            var errorLabel = new TextBlock
            {
                Text = "Ошибка при загрузке мероприятий",
                FontSize = 16,
                Margin = new Thickness(10)
            };
            EventsWrapPanel.Children.Add(errorLabel);
        }
    }
    


public List<Event> GetEventsByProjectId(Guid projectId)
{
    using (var context = new TrpoContext())
    {
        // Ищем проект по Id и включаем связанные события
        var project = context.Projects
            .Include(p => p.Events) // Подгружаем связанные события
            .FirstOrDefault(p => p.Id == projectId);

        // Проверяем, что проект найден перед обращением к его свойствам
        if (project != null)
        {
            Console.WriteLine(project.Events.Count);
            return project.Events.ToList();
        }
        
        // Если проект не найден, возвращаем пустой список
        return new List<Event>();
    }
}



    private void AddButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var addWindow = new CreateEventWindow(projectId);
        addWindow.Show();
        this.Close();
    }

    private void BackButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var mainWindow = new MainWindow();
        mainWindow.Show();
        this.Close();
    }

    private void CardButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is Event eve)
        {
            var eventInfoWindow = new EventInfoWindow(projectId, eve.Id);
            eventInfoWindow.Show();
            this.Close();
        }
    }
}