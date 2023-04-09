using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using Newtonsoft.Json;

namespace WpfApp1;

public class ProjectsManager
{
    private const string SAVE_PATH = "Projects.json";
    public static ProjectsManager Instance
    {
        get
        {
            if (instance == null)
                instance = new ProjectsManager();
            return instance;
        }
    }
    public delegate void RefreshEvent(Project id);

    public RefreshEvent OnRefresh;
    private static ProjectsManager instance;
    private List<Project> projects { get; set; } = new List<Project>();
    private bool timerIsOn = false;
    private Project selectedProject;
    private Stopwatch stopwatch = new Stopwatch();
    private Thread thread;
    private long elapsedMiliseconds = 0;
    private bool isOn = true;
    

    private ProjectsManager()
    {
        TryLoad();
        if (projects == null)
            projects = new List<Project>();
        
        thread = new Thread(TimerLoop);
        thread.Start();
        Application.Current.Exit += (sender, args) =>
        {
            isOn = false;
        };
        Application.Current.Exit += Save;
    }

    public void StartTimer(Project project)
    {
        OnRefresh.Invoke(project);
        timerIsOn = true;
        selectedProject = project;
        stopwatch.Restart();
        stopwatch.Start();
    }

    public void StopTimer(Project project)
    {
        if (project != selectedProject)
            return;
        timerIsOn = false;
        stopwatch.Stop();
        OnRefresh.Invoke(null);
        selectedProject = null;
    }
    
    public void AddNewProject(string name)
    {
        projects.Add(new Project(name, projects.Count));
    }

    public void RemoveProject(string name)
    {
        projects.Remove(projects.Find(ctg => ctg.Name == name));
    }

    public List<Project> GetProjects()
    {
        return projects;
    }

    private void TimerLoop()
    {
        while (isOn)
        {
            // if (!timerIsOn)
            //     return;
            if (stopwatch.ElapsedMilliseconds> 1000)
            {
                selectedProject?.TickSecond();
                stopwatch.Restart();
            }
            
        }
    }

    private void TryLoad()
    {
        try
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SAVE_PATH);
            if (!File.Exists(path))
            {
                return;
            }

            var json = File.ReadAllText(path);

            projects = JsonConvert.DeserializeObject<List<Project>>(json);
        }
        catch (Exception e)
        {
            Error(e.Message);
        }
    }

    public void Save(object sender, ExitEventArgs e)
    {
        try
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory , SAVE_PATH);
        
            if (!File.Exists(path))
            {
                File.Create(path);
            
            }
            var json = JsonConvert.SerializeObject(projects);
            File.WriteAllText(path, json);
        }
        catch (Exception exception)
        {
            Error(exception.Message);
        }
    }

    private void Error(string str)
    {
        MessageBox.Show(str, "Save or loading problem", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}