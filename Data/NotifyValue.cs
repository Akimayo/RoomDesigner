using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomDesigner.Data
{
    public class NotifyValue<T> : INotifyPropertyChanged
    {
        private T _value;
        public T Value { get { return this._value; } set { this._value = value; OnPropertyChanged("Value"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public NotifyValue() { }
        public NotifyValue(T value) {
            this.Value = value;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler evt = PropertyChanged;
            if (evt != null) evt(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
