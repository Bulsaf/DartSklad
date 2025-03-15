using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using DartSklad.Models;

namespace DartSklad;

public partial class CreateProjectWindow : Window
{
    public CreateProjectWindow()
    {
        InitializeComponent();
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
{
    // Получаем данные из текстовых полей и DatePicker'ов
    string projectName = ProjectNameTextBox.Text;
    DateTimeOffset? entryDateOffset = EntryDatePicker.SelectedDate;
    DateTimeOffset? startDateOffset = StartDatePicker.SelectedDate;
    DateTimeOffset? endDateOffset = EndDatePicker.SelectedDate;

    // Проверяем, что все поля заполнены
    if (string.IsNullOrWhiteSpace(projectName) || !entryDateOffset.HasValue || !startDateOffset.HasValue || !endDateOffset.HasValue)
    {
        Console.WriteLine("Все поля должны быть заполнены!");
        return;
    }

    // Преобразуем DateTimeOffset? в DateTime и явно указываем UTC
    DateTime entryDate = entryDateOffset.Value.UtcDateTime;
    DateTime startDate = startDateOffset.Value.UtcDateTime;
    DateTime endDate = endDateOffset.Value.UtcDateTime;

    // Создаем новый проект
    var newProject = new Project
    {
        Title = projectName,
        EntryDate = entryDate,
        BeginDate = startDate,
        EndDate = endDate
    };

    // Сохраняем проект в базу данных
    SaveProject(newProject);

}

   
    private void SaveProject(Project project)
    {
        using (var context = new TrpoContext())
        {
            context.Projects.Add(project);
            context.SaveChanges();
        }
    }

    private void BackButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var mainWindow = new MainWindow();
        mainWindow.Show();
        this.Close();
    }
}