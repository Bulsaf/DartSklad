using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DartSklad.Models;

namespace DartSklad;

public partial class EventInfoWindow : Window
{
    private Guid eventId;
    private Guid parentId;

    public EventInfoWindow(Guid parentId, Guid eventId)
    {
        this.eventId = eventId;
        this.parentId = parentId;

        InitializeComponent();



            var subjectslist = GetAll();

            foreach (var sub in subjectslist)
            {
                var button = new Button
                {
                    Classes = { "uc" },
                    Content = new ProjectsPanelControl
                    {
                        DataContext = sub
                    },
                    Tag = sub 
                };

                button.Click += CardButton_Click; 
                EventsWrapPanel.Children.Add(button); 
            }



    }


            private List<Subject> GetAll()
            {
                using (var context = new TrpoContext())
                {
                // Выполняем запрос к базе данных
                    var subj = context.Subjects.ToList();
                    return subj;
                }
            }


    private void AddButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var addWindow = new CreateStorageWindow();
        addWindow.Show();
        this.Close();
    }
    
    private void BackButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var projWindow = new ProjectInfoWindow(parentId);
        projWindow.Show();
        this.Close();
    }

    private void CardButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var projWindow = new StorageObjectsWindow();
        projWindow.Show();
        this.Close();
    }
}