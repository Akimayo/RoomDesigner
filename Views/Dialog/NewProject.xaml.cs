using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Dokumentaci k šabloně položky Dialog obsahu najdete na adrese https://go.microsoft.com/fwlink/?LinkId=234238

namespace RoomDesigner.Views.Dialog
{
    public sealed partial class NewProject : ContentDialog, INotifyPropertyChanged
    {
        public bool IsMetric { get; set; } = true;
        public bool IsImperial { get { return !this.IsMetric; } set { this.IsMetric = !value; } }

        private string _projectName;
        public string ProjectName { get { return this._projectName; } set { this._projectName = value; OnPropertyChanged("AllowConfirm"); } }
        public bool AllowConfirm { get { return ProjectName != null && ProjectName.Length > 0; } set { } }

        public bool Confirmed = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public NewProject()
        {
            this.InitializeComponent();
        }

        private void CreateNewProject(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.Confirmed = true;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler evt = PropertyChanged;
            if (evt != null) evt(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
