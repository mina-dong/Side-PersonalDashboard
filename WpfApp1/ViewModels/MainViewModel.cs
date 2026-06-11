using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using WpfApp1.Models;
using System.Windows.Input;
using WpfApp1.Commands;
using WpfApp1.Services;
using System.ComponentModel;

namespace WpfApp1.ViewModels
{
    internal class MainViewModel
    {
        public string Title { get; set; }
        public string NewTodo { get; set; }
        public ObservableCollection<TodoItem> Todos { get; set; }
        public ICommand AddTodoCommand { get; set; }
        public ICommand DelTodoCommand {  get; set; }
        
        public TodoItem SelectedTodo { get; set; }

        private readonly TodoStorageService _storage;
        public string CurrentDate => DateTime.Now.ToString("yyyy-MM-dd");

        public MainViewModel() {
            _storage = new TodoStorageService();
            Title = "Personal DashBoard";

            Todos = _storage.Load();
            foreach (var todo in Todos)
            {
                todo.PropertyChanged += Todo_PropertyChanged;
            }

            //초기 임시데이터
            //Todos = new ObservableCollection<TodoItem> { 
            //    new TodoItem {Title = "운동하기"},
            //    new TodoItem {Title = "아침식사 준비하기"},
            //    new TodoItem {Title = "세탁기 정리하기"},
            //};

            AddTodoCommand = new RelayCommand(AddTodo);
            DelTodoCommand = new RelayCommand(DelTodo);
        }

        private void AddTodo(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(NewTodo))
            {
                Todos.Add(new TodoItem
                {
                    Title = NewTodo
                });
                _storage.Save(Todos);
            }
        }

        private void DelTodo(object parameter)
        {
            TodoItem todo = parameter as TodoItem;

            if (todo != null)
            {
                Todos.Remove(todo);
                _storage.Save(Todos);
            }
        }
        private void Todo_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _storage.Save(Todos);
        }
    }
}
