using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DartSklad.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace DartSklad;

public partial class AddObjectWindow : Window
{
    private List<SubjectType> _subjectTypes;
    public Subject NewSubject { get; set; } = new Subject();
    public List<SubjectType> SubjectTypes => _subjectTypes;
    
    private SubjectType _selectedSubjectType;
    public SubjectType SelectedSubjectType
    {
        get => _selectedSubjectType;
        set
        {
            _selectedSubjectType = value;
            if (value != null)
            {
                NewSubject.SubjectTypeId = value.Id;
            }
        }
    }

    public AddObjectWindow()
    {
        InitializeComponent();
        LoadSubjectTypes();
        DataContext = this;
    }

    private void LoadSubjectTypes()
    {
        using (var db = new TrpoContext())
        {
            _subjectTypes = db.SubjectTypes.ToList();
        }
    }

    private async void AddButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NewSubject.Title))
        {
            // Show error message to user
            return;
        }

        // Fix for PostgreSQL DateTime issue - ensure UTC
        NewSubject.CreatedAt = DateTime.UtcNow;

        try
        {
            using (var db = new TrpoContext())
            {
                db.Subjects.Add(NewSubject);
                await db.SaveChangesAsync();
            }
            var allObjectsWindow = new AllObjectsWindow();
            allObjectsWindow.Show();
            Close();
        }
        catch (Exception ex)
        {
            // Handle or log the exception
            Console.WriteLine($"Error saving to database: {ex.Message}");
        }
    }

    private void BackButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var allObjectsWindow = new AllObjectsWindow();
        allObjectsWindow.Show();
        Close();
    }
}