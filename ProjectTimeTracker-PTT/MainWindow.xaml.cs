using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjectTimeCounter_PTC.Views;
using WpfApp1;

namespace ProjectTimeCounter_PTC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateProjectsList();
        }

        private void UpdateProjectsList()
        {
            var projects = ProjectsManager.Instance.GetProjects();
            List<ProjectLine> projectLines = new List<ProjectLine>();
            for (int i = 0; i < projects.Count; i++)
            {
                projectLines.Add(new ProjectLine(projects[i]));
            }

            ProjectList.ItemsSource = projectLines;
        }

        private void Add_OnClick(object sender, RoutedEventArgs e)
        {
            ProjectsManager.Instance.AddNewProject(ProjectName.Text);
            UpdateProjectsList();
        }

        private void Remove_OnClick(object sender, RoutedEventArgs e)
        {
            ProjectsManager.Instance.RemoveProject(ProjectName.Text);
            UpdateProjectsList();
        }

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            ProjectsManager.Instance.Save(sender, null);
        }

        private void About_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is PTT made by thief01.\n It can help you track your time projects.\n\n Ver. info: alfa v1.0");
        }
    }
}