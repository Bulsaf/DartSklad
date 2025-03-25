using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DartSklad.Models;

namespace DartSklad;

public partial class StorageObjectsWindow : Window
{
    private Guid storageId;

    public StorageObjectsWindow(Guid storageId)
    {
        this.storageId = storageId;

        InitializeComponent();

        var objectsList = GetObjectsByStorageId(storageId);

        foreach (var obj in objectsList)
        {
            var button = new Button
            {
                Classes = { "uc" },
                Content = new ProjectsPanelControl
                {
                    DataContext = obj
                },
                Tag = obj
            };

            button.Click += ObjectButton_Click;
            ObjectsWrapPanel.Children.Add(button);
        }
    }
private List<Subject> GetObjectsByStorageId(Guid storageId)
{
    using (var context = new TrpoContext())
    {
        // Получаем все Subject, которые связаны с указанным Storage через SubjectsStorage
        var objects = context.Subjects
            .Where(s => s.SubjectsStorages.Any(ss => ss.StorageId == storageId))
            .ToList();

        return objects;
    }
}

    private void AddButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var addObjectWindow = new AddObjectToStorageWindow(storageId); // Передаем storageId
        addObjectWindow.Show();
        this.Close();
    }

    private void BackButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        this.Close();
    }

    private void ObjectButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is Storage storageObject)
        {
            // Здесь можно открыть окно для просмотра или редактирования объекта
            // var objectDetailsWindow = new ObjectDetailsWindow(storageObject.Id);
            // objectDetailsWindow.Show();
            this.Close();
        }
    }
}