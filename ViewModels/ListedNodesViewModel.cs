using System.Collections.ObjectModel;
using TaskPro1.Models;

namespace TaskPro1.ViewModels
{
    public class ListedNodesViewModel
    {
        public ObservableCollection<Node> Nodes { get; set; }
        public ObservableCollection<Grouping<string, Node>> GroupedNodes { get; set; }

        public ListedNodesViewModel()
        {
            Nodes = new ObservableCollection<Node>
            {
                new Node { Name = "Kimbinyiko1", Location = "Location 1", Latitude = 34.0522, Longitude = -118.2437, Price = 100.00m, Department = "Selemara" },
                new Node { Name = "Magori", Location = "Location 2", Latitude = 40.7128, Longitude = -74.0060, Price = 200.00m, Department = "Programmer" },
                new Node { Name = "Nyamisuri", Location = "Location 3", Latitude = 37.7749, Longitude = -122.4194, Price = 300.00m, Department = "Accountant" },
                new Node { Name = "Node4", Location = "Location 4", Latitude = 51.5074, Longitude = -0.1278, Price = 400.00m, Department = "Selemara" },
                new Node { Name = "Node5", Location = "Location 5", Latitude = 48.8566, Longitude = 2.3522, Price = 500.00m, Department = "Programmer" },
                new Node { Name = "Node6", Location = "Location 6", Latitude = 35.6895, Longitude = 139.6917, Price = 600.00m, Department = "Accountant" },
                new Node { Name = "Node7", Location = "Location 7", Latitude = -33.8688, Longitude = 151.2093, Price = 700.00m, Department = "Selemara" },
                new Node { Name = "Node8", Location = "Location 8", Latitude = 55.7558, Longitude = 37.6173, Price = 800.00m, Department = "Programmer" },
                new Node { Name = "Node9", Location = "Location 9", Latitude = 39.9042, Longitude = 116.4074, Price = 900.00m, Department = "Accountant" },
                new Node { Name = "Node10", Location = "Location 10", Latitude = 52.5200, Longitude = 13.4050, Price = 1000.00m, Department = "Selemara" }
            };
            var groupedNodes = from node in Nodes
                               group node by node.Department into nodeGroup
                               select new Grouping<string, Node>(nodeGroup.Key, nodeGroup);

            GroupedNodes = new ObservableCollection<Grouping<string, Node>>(groupedNodes);
        }


    }
    public class Grouping<K, T> : ObservableCollection<T>
    {
        public K Key { get; private set; }

        public Grouping(K key, IEnumerable<T> items)
        {
            Key = key;
            foreach (var item in items)
                this.Items.Add(item);
        }
    }

}