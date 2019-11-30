using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// Dokumentaci k šabloně Prázdná aplikace najdete na adrese https://go.microsoft.com/fwlink/?LinkId=234238

namespace RoomDesigner.Views
{
    /// <summary>
    /// Prázdná stránka, která se dá použít samostatně nebo se na ni dá přejít v rámci
    /// </summary>
    public sealed partial class Menu : Page
    {
        public ObservableCollection<Data.ProjectItem> RecentProjects => Data.Storage.Current.LastProjects;
        public Menu()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Data.Storage.Current.ParseLastProjects(e.Parameter);
        }

        private async void NewProject(object sender, TappedRoutedEventArgs e)
        {
            Dialog.NewProject newProjectDialog = new Dialog.NewProject();
            await newProjectDialog.ShowAsync();
            if (newProjectDialog.Confirmed)
            {
                this.NavigateToEditor(new Data.ProjectData(newProjectDialog.ProjectName, newProjectDialog.IsMetric ? Data.ProjectData.UnitSystem.Metric : Data.ProjectData.UnitSystem.Imperial));
            }
        }
        private void OpenRoamingProject(object sender, TappedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private async void OpenOnlineProject(object sender, TappedRoutedEventArgs e)
        {
            Dialog.OpenOnlineProject openOnlineProject = new Dialog.OpenOnlineProject();
            await openOnlineProject.ShowAsync();
            if(openOnlineProject.Confirmed)
            {
                this.LoadingMessage.Text = "Opening web resource";
                this.LoaderOverlay.Visibility = Visibility.Visible;
                System.Net.WebClient client = new System.Net.WebClient();
                string data = "";
                try
                {
                    data = client.DownloadString(openOnlineProject.Url);
                } catch(System.Net.WebException) {
                    this.ErrorMessage.Show("Failed retrieving data from URL.");
                    this.LoaderOverlay.Visibility = Visibility.Collapsed;
                    return;
                }
                this.LoadingMessage.Text = "Processing file";
                Data.ProjectData project;
                try
                {
                    project = Data.Storage.ParseProjectDataJson(data);
                } catch (Exception a) {
                    this.ErrorMessage.Show(a.Message);
                    this.LoaderOverlay.Visibility = Visibility.Collapsed;
                    return;
                }
                this.LoadingMessage.Text = "Opening editor";
                NavigateToEditor(project);
                this.LoaderOverlay.Visibility = Visibility.Collapsed;
            }
        }
        private void OpenLocalProject(object sender, TappedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void NavigateToEditor(Data.ProjectData project)
        {
            this.Frame.Navigate(typeof(Editor), project, new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
        }
    }
}
