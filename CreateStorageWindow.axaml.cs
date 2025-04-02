using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DartSklad.Models;

namespace DartSklad;

public partial class CreateStorageWindow : Window
{
    private Guid eventId;
    private Guid parentId;

    public CreateStorageWindow(Guid parentId, Guid eventId)
    {
        Console.WriteLine("Которое в создавалку пришло:");
        Console.WriteLine(eventId);
        this.eventId = eventId;
        this.parentId = parentId;
        
        InitializeComponent();
    }
    private void AddButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        try
        {
            string storageName = Title.Text?? "";
            
            // Проверка на пустое название склада
            if (string.IsNullOrWhiteSpace(storageName))
            {
                var messageWindow = new MessageWindow("Ошибка", "Название склада не может быть пустым");
                messageWindow.Show();
                return;
            }

            var newStorage = new Storage
            {
                Title = storageName,
                EventId = eventId,
            };

            // Сохраняем склад в базу данных
            SaveStorage(newStorage);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при создании склада: {ex.Message}");
            var messageWindow = new MessageWindow("Ошибка", $"Не удалось создать склад: {ex.Message}");
            messageWindow.Show();
        }
    }

    private void SaveStorage(Storage newStorage)
    {
        try
        {
            using (var context = new TrpoContext())
            {
                context.Storages.Add(newStorage);
                context.SaveChanges();
            }

            var eventWindow = new EventInfoWindow(parentId, eventId);
            eventWindow.Show();
            this.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении склада: {ex.Message}");
            throw; // Пробрасываем исключение выше для обработки в AddButton_Click
        }
    }

    private void BackButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        try
        {
            var eventWindow = new EventInfoWindow(parentId, eventId);
            eventWindow.Show();
            this.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при возврате к мероприятию: {ex.Message}");
            var messageWindow = new MessageWindow("Ошибка", $"Не удалось вернуться к мероприятию: {ex.Message}");
            messageWindow.Show();
        }
    }
}