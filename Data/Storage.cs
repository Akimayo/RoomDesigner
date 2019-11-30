using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace RoomDesigner.Data
{
    public class Storage
    {
        private static Storage _storage;
        public static Storage Current { get { if (Storage._storage == null) Storage._storage = new Storage(); return Storage._storage; } }

        public ObservableCollection<ProjectItem> LastProjects { get; private set; }

        public Storage()
        {
            this.LastProjects = new ObservableCollection<ProjectItem>();
        }

        public void ParseLastProjects(object e)
        {
            ApplicationDataContainer data = (ApplicationDataContainer)e;
            if (data.Values.Count > 0)
            {
                foreach (KeyValuePair<string, object> item in data.Values)
                {
                    this.LastProjects.Add(new ProjectItem() { IsItem = true, Name = item.Key, Path = (Uri)item.Value });
                }
            } else this.LastProjects.Add(new ProjectItem() { IsItem = false, Name = "No recent projects" });
        }

        internal static ProjectData ParseProjectDataJson(string data)
        {
            throw new NotImplementedException();
        }
    }
}
