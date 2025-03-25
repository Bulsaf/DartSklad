using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DartSklad.Models;

namespace DartSklad;

public partial class CreateStorageWindow : Window
{
    private Guid eventId;

    public CreateStorageWindow(Guid parentId, Guid eventId)
    {
        Console.WriteLine("Которое в создавалку пришло:");
        Console.WriteLine(eventId);
        this.eventId = eventId;
        
        InitializeComponent();
    }
    private void AddButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        string storageName = Title.Text?? "";

        var newStorage = new Storage
        {

            Title = storageName,
            EventId = eventId,
        };

        // Сохраняем склад в базу данных
        SaveStorage(newStorage);

      
    }

    private void SaveStorage(Storage newStorage)
    {
        using (var context = new TrpoContext())
        {
            context.Storages.Add(newStorage);
            context.SaveChanges();
        }

        var eventWindow = new EventInfoWindow(eventId, newStorage.Id);
        eventWindow.Show();
        this.Close();
    }

    private void BackButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var eventWindow = new MainWindow();
        eventWindow.Show();
        this.Close();
    }
}