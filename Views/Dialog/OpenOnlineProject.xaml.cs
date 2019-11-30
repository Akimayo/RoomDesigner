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
    public sealed partial class OpenOnlineProject : ContentDialog, INotifyPropertyChanged
    {
        private string _url;
        public string Url { get { return this._url; } set { this._url = value; OnPropertyChanged("AllowConfirm"); } }
        public bool Confirmed = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool AllowConfirm { get { return Url != null && Url.Length > 0; } set { } }
        public OpenOnlineProject()
        {
            this.InitializeComponent();
        }

        private void OpenProject(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.Confirmed = true;
        }

        private async void Paste(object sender, TappedRoutedEventArgs e)
        {
            this.Url = await Windows.ApplicationModel.DataTransfer.Clipboard.GetContent().GetTextAsync();
            OnPropertyChanged("Url");
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler evt = PropertyChanged;
            if (evt != null) evt(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
