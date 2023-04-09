using System;
using System.Collections.Generic;
using System.Windows;
using Newtonsoft.Json;

namespace WpfApp1;

[System.Serializable]
public class Project
{
    public string Name { get; set; }
    public int Id { get; set; }

    public Dictionary<string, TimeCounter> Days { get; set; } = new Dictionary<string, TimeCounter>();
    
    public delegate void SecondTick();
    [JsonIgnore]
    public SecondTick OnSecondTick;
    public Project(string name, int id)
    {
        Name = name;
        Id = id;
    }

    public string GetCurrentTime()
    {
        var now = DateTime.Now.ToString("yyyy-MM-dd");
        if (!Days.ContainsKey(now))
            return "null:null:null";
        return Days[now].Time;
    }

    public void TickSecond()
    {
        var now = DateTime.Now.ToString("yyyy-MM-dd");
        if (!Days.ContainsKey(now))
        {
            Days.Add(now, new TimeCounter());
        }
        Days[now].TickSecond();
        if(OnSecondTick != null)
            OnSecondTick.Invoke();
        
    }
    
}