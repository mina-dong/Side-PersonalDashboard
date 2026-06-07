using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    internal class MainViewModel
    {
       public string Title { get; set; }
        
       public ObservableCollection<TodoItem> Todos { get; set; } 
       
        public MainViewModel() {
            Title = "Personal DashBoard";

            Todos = new ObservableCollection<TodoItem> { 
                new TodoItem {Title = "운동하기"},
                new TodoItem {Title = "아침식사 준비하기"},
                new TodoItem {Title = "세탁기 정리하기"},
                    


            };

        }

        
    }
}
