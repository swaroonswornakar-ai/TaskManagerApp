using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace TaskManagerApp
{
    public class TaskManager
    {
        private List<BaseTask> _tasks = new List<BaseTask>();
        private string _filePath = "tasks.json";

        public List<BaseTask> GetAllTasks() => _tasks;

        public void AddTask(BaseTask task)
        {
            _tasks.Add(task);
            SaveToFile();
        }

        public void DeleteTask(BaseTask task)
        {
            _tasks.Remove(task);
            SaveToFile();
        }

        public void UpdateTask(BaseTask oldTask, BaseTask newTask)
        {
            int index = _tasks.IndexOf(oldTask);
            if (index >= 0)
            {
                _tasks[index] = newTask;
                SaveToFile();
            }
        }

        public List<BaseTask> SearchTasks(string keyword)
        {
            return _tasks.FindAll(t =>
                t.Title.ToLower().Contains(keyword.ToLower()));
        }

        public List<BaseTask> FilterByCategory(string category)
        {
            return _tasks.FindAll(t => t.Category == category);
        }

        public void SaveToFile()
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                };
                string json = JsonConvert.SerializeObject(
                     _tasks, Newtonsoft.Json.Formatting.Indented, settings);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save tasks: " + ex.Message);
            }
        }

        public void LoadFromFile()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    var settings = new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    };
                    string json = File.ReadAllText(_filePath);
                    _tasks = JsonConvert.DeserializeObject
                        <List<BaseTask>>(json, settings);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load tasks: " + ex.Message);
            }
        }
    }
}
