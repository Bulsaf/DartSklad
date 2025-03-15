using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using DartSklad.Models;
namespace DartSklad
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            var projects = GetAllProjects();

            foreach (var project in projects)
            {
                var button = new Button
                {
                    Classes = { "uc" },
                    Content = new ProjectsPanelControl
                    {
                        DataContext = project
                    },
                    Tag = project 
                };

                button.Click += CardButton_Click; 
                ProjectsWrapPanel.Children.Add(button); 
            }



        }



            private List<Project> GetAllProjects()
            {
                using (var context = new TrpoContext())
                {
                // Выполняем запрос к базе данных
                    var projects = context.Projects.ToList();
                    return projects;
                }
            }

        private void AllObjectsButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var allObjWindow = new AllObjectsWindow();
            allObjWindow.Show();
            this.Close();
        }

        private void AddButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var createProjectWindow = new CreateProjectWindow();
            createProjectWindow.Show();
            this.Close();
        }

        private void CardButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
        if (sender is Button button && button.Tag is Project project)
            {
                // Создаем новое окно и передаем Id проекта
                var projectInfoWindow = new ProjectInfoWindow(project.Id);
                projectInfoWindow.Show();
                this.Close();
            }
        }
    }
}