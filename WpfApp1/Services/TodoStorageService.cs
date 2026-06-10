using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using WpfApp1.Models;
using Newtonsoft.Json;

namespace WpfApp1.Services
{
    internal class TodoStorageService
    {
        private const string FilePath = "todos.json";

        public void Save(ObservableCollection<TodoItem> todos)
        {
            string json = JsonConvert.SerializeObject(todos);
            File.WriteAllText(FilePath, json);
        }

        public ObservableCollection<TodoItem> Load()
        {
            if (!File.Exists(FilePath))
            {
                return new ObservableCollection<TodoItem>();
            }

            string json = File.ReadAllText(FilePath);

            return JsonConvert.DeserializeObject<ObservableCollection<TodoItem>>(json)
                    ?? new ObservableCollection<TodoItem>();
        }
    }
}
