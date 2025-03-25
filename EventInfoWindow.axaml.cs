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

        Console.WriteLine(eventId);
        this.eventId = eventId;
        this.parentId = parentId;

        InitializeComponent();

        var storagesList = GetStoragesByEventId(eventId);

        foreach (var storage in storagesList)
        {
            var button = new Button
            {
                Classes = { "uc" },
                Content = new ProjectsPanelControl
                {
                    DataContext = storage
                },
                Tag = storage
            };

            button.Click += CardButton_Click;
            EventsWrapPanel.Children.Add(button);
        }
    }

    private List<Storage> GetStoragesByEventId(Guid eventId)
    {
        using (var context = new TrpoContext())
        {
            // Выполняем запрос к базе данных, чтобы получить только склады, привязанные к этому событию
            var storages = context.Storages
                .Where(s => s.EventId == eventId)
                .ToList();

            return storages;
        }
    }

    private void AddButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var addWindow = new CreateStorageWindow(eventId, eventId); // Передаем parentId и eventId
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
        if (sender is Button button && button.Tag is Storage storage)
        {
            var storageObjectsWindow = new StorageObjectsWindow(storage.Id); // Передаем Id склада
            storageObjectsWindow.Show();
            this.Close();
        }
    }
}