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

        projectId = id;
        InitializeComponent();


            var events = GetEventsByProjectId(id);

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
    


public List<Event> GetEventsByProjectId(Guid projectId)
{
    using (var context = new TrpoContext())
    {
        // Ищем проект по Id и включаем связанные события
        var project = context.Projects
            .Include(p => p.Events) // Подгружаем связанные события
            .FirstOrDefault(p => p.Id == projectId);

        // Если проект найден, возвращаем его события
        return project?.Events.ToList() ?? new List<Event>();
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
        var createProjectWindow = new EventInfoWindow(projectId, projectId);
        createProjectWindow.Show();
        this.Close();
    }
}