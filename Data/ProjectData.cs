using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomDesigner.Data
{
    public class ProjectData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _projectName;

        public string ProjectName { get { return this._projectName; } set { this._projectName = value; this.OnPropertyChanged("ProjectName"); } }
        public UnitSystem Units { get; }
        public ObservableCollection<Drawing> Drawings { get; }

        public ProjectData(string name, UnitSystem units)
        {
            this.ProjectName = name;
            this.Units = units;
            this.Drawings = new ObservableCollection<Drawing>();
        }

        public enum UnitSystem
        {
            Metric, Imperial
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler evt = PropertyChanged;
            if (evt != null) evt(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
