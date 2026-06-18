using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    internal class TreeInfo : INotifyPropertyChanged
    {
        private int _exp;

        public int Exp
        {
            get => _exp;
            set
            {
                _exp = value;
                OnPropertyChanged(nameof(Exp));
                OnPropertyChanged(nameof(Level));
            }
        }

        public int Level => Exp / 100 + 1;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}