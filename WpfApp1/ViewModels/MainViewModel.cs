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
using System.Windows;

namespace WpfApp1.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public string Title { get; set; }
        public string NewTodo { get; set; }
        public ObservableCollection<TodoItem> Todos { get; set; }
        public ICommand AddTodoCommand { get; set; }
        public ICommand DelTodoCommand {  get; set; }
        
        public TodoItem SelectedTodo { get; set; }

        private string _weatherText;

        public string WeatherText
        {
            get => _weatherText;
            set
            {
                _weatherText = value;
                OnPropertyChanged(nameof(WeatherText));
            }
        }

        private double _temperature;

        public double Temperature
        {
            get => _temperature;
            set
            {
                _temperature = value;
                OnPropertyChanged(nameof(Temperature));
            }
        }

        public TreeInfo Tree { get; set; }

        private readonly TodoStorageService _storage;
        private readonly WeatherService _weatherService;
        private readonly TreeStorageService _treeStorage;
        public string CurrentDate => DateTime.Now.ToString("yyyy-MM-dd");

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }

        public MainViewModel() {
            _storage = new TodoStorageService();
            _weatherService = new WeatherService();
            _treeStorage = new TreeStorageService();
            Tree = _treeStorage.Load();

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

            _ = LoadWeather();

        }

        private async Task LoadWeather()
        {
            WeatherInfo weather =
                await _weatherService.GetWeatherAsync();

            Temperature = weather.Temperature;

            WeatherText =
                _weatherService.GetWeatherDescription(weather.WeatherCode);
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

            if (todo.IsDone)
            {
                Tree.Exp += 10;

                //100 채우면 경험치 초기화 + 레벨업
                if (Tree.Exp >= 100)
                {
                    Tree.Level++;
                    Tree.Exp = 0;
                }

                MessageBox.Show("나무 저장!");
                _treeStorage.Save(Tree);
            }
            Todos.Remove(todo);
            _storage.Save(Todos);
        }

        private void Todo_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _storage.Save(Todos);
        }
    }
}
