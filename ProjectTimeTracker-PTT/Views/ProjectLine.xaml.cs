using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp1;

namespace ProjectTimeCounter_PTC.Views;

public partial class ProjectLine : UserControl
{
    public Project Project { get; private set; }


    public ProjectLine(Project project)
    {
        InitializeComponent();
        ProjectNameTextBox.Text = project.Name;
        ProjectTimer.Text = project.GetCurrentTime();
        Project = project;
        // Project.OnSecondTick += UpdateTime;
        ProjectsManager.Instance.OnRefresh += (ctg) => Application.Current.Dispatcher.Invoke(() => Refresh(ctg));;
        Project.OnSecondTick += () => Application.Current.Dispatcher.Invoke(() => UpdateTime());
        }

    public void Refresh(Project project)
    {
        ProjectTimer.Background = new SolidColorBrush(project == Project ? new Color() {A = 120, G = 255, B = 0, R = 0} : new Color() {A = 120, G = 0, B = 0, R = 255} );
    }

    private void StartButton_OnClick(object sender, RoutedEventArgs e)
    {
        ProjectsManager.Instance.StartTimer(Project);
    }

    private void StopButton_OnClick(object sender, RoutedEventArgs e)
    {
        ProjectsManager.Instance.StopTimer(Project);
    }

    private void Showbutton_OnClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Not implemented", "Not implemented", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void UpdateTime()
    {
        ProjectTimer.Text = Project.GetCurrentTime();
    }
}