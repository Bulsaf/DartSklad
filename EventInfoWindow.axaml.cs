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

        try
        {
            var storagesList = GetStoragesByEventId(eventId);

            if (storagesList.Count > 0)
            {
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
            else
            {
                // Добавляем сообщение, если нет складов
                var noStoragesLabel = new TextBlock
                {
                    Text = "Нет складов",
                    FontSize = 16,
                    Margin = new Thickness(10)
                };
                EventsWrapPanel.Children.Add(noStoragesLabel);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке складов: {ex.Message}");
            // Добавляем сообщение об ошибке на экран
            var errorLabel = new TextBlock
            {
                Text = $"Ошибка при загрузке складов: {ex.Message}",
                FontSize = 16,
                Margin = new Thickness(10)
            };
            EventsWrapPanel.Children.Add(errorLabel);
        }
    }

    private List<Storage> GetStoragesByEventId(Guid eventId)
    {
        try
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
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при получении списка складов: {ex.Message}");
            return new List<Storage>(); // Возвращаем пустой список в случае ошибки
        }
    }

    private void AddButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        try
        {
            var addWindow = new CreateStorageWindow(parentId, eventId); // Передаем parentId и eventId
            addWindow.Show();
            this.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при открытии окна создания склада: {ex.Message}");
            var messageWindow = new MessageWindow("Ошибка", $"Не удалось открыть окно создания склада: {ex.Message}");
            messageWindow.Show();
        }
    }

    private void BackButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        try
        {
            var projWindow = new ProjectInfoWindow(parentId);
            projWindow.Show();
            this.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при возврате к проекту: {ex.Message}");
            var messageWindow = new MessageWindow("Ошибка", $"Не удалось вернуться к проекту: {ex.Message}");
            messageWindow.Show();
        }
    }

    private void CardButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        try
        {
            if (sender is Button button && button.Tag is Storage storage)
            {
                var storageObjectsWindow = new StorageObjectsWindow(storage.Id); // Передаем Id склада
                storageObjectsWindow.Show();
                this.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при открытии окна объектов склада: {ex.Message}");
            var messageWindow = new MessageWindow("Ошибка", $"Не удалось открыть окно объектов склада: {ex.Message}");
            messageWindow.Show();
        }
    }
}