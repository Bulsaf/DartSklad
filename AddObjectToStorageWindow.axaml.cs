using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DartSklad.Models;
// using DartSklad.MainWindow;

namespace DartSklad;

public partial class AddObjectToStorageWindow : Window
{
    private Guid storageId;
    private ListBox objectsListBox;
    
    public AddObjectToStorageWindow(Guid storageId)
    {
        this.storageId = storageId;
        InitializeComponent();
        
        // Получаем все доступные объекты
        var allObjects = GetAllAvailableObjects();
        
        // Создаем ListBox для отображения объектов с чекбоксами
        

        objectsListBox = new ListBox
        {
            SelectionMode = SelectionMode.Multiple
        };

        foreach (var obj in allObjects)
        {
            objectsListBox.Items.Add(new CheckBox
            {
                Content = obj.Title,
                Tag = obj,
                Margin = new Thickness(5),
                FontSize = 16
            });
        }
        // Заменяем WrapPanel на наш ListBox
        var scrollViewer = this.FindControl<ScrollViewer>("MainScrollViewer");
        scrollViewer.Content = objectsListBox;
    }

    private List<Subject> GetAllAvailableObjects()
    {
        using (var context = new TrpoContext())
        {
            // Получаем все объекты, которые еще не привязаны к этому складу
            return context.Subjects
                .Where(s => !s.SubjectsStorages.Any(ss => ss.StorageId == storageId))
                .ToList();
        }
    }

    private void AddButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        // Получаем выбранные объекты
        var selectedObjects = objectsListBox.Items
            .Cast<CheckBox>()
            .Where(cb => cb.IsChecked == true)
            .Select(cb => cb.Tag as Subject)
            .ToList();



        // Добавляем выбранные объекты на склад
        using (var context = new TrpoContext())
        {
            foreach (var subject in selectedObjects)
            {
                if (subject != null)
                {
                    var subjectStorage = new SubjectsStorage
                    {
                        SubjectId = subject.Id,
                        StorageId = storageId
                    };
                    context.SubjectsStorages.Add(subjectStorage);
                }
            }
            context.SaveChanges();
        }

        // Закрываем окно после добавления
        this.Close();
    }

    private void BackButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        
    }
}