using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WpfApp1.Models
{
    internal class TodoItem : INotifyPropertyChanged
    {
        private bool _isDone;

        public string Title { get; set; }

        public bool IsDone
        {
            get => _isDone;
            set
            {
                _isDone = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(IsDone)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
