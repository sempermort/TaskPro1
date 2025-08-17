using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TaskPro1.Models; // Adjust namespace as needed
using Windows.Services.Maps;
using TaskPro1.Helpers;
namespace TaskPro1.ViewModels
{
    public partial class SearchViewModel : ObservableObject, INotifyPropertyChanged
    {
        private string? _searchText;
        private readonly ICenterOnMap _centerOn;
        private ObservableCollection<Node> _filteredNodes;
        public ICommand ZoomToLocationCommand { get; }

        public event EventHandler<Node>? ZoomRequested;
        [ObservableProperty]
        private bool isImageClicked;
     

        public SearchViewModel(ICenterOnMap centerOn)
        {
            _filteredNodes = new ObservableCollection<Node>(TestData.Nodes);
            ZoomToLocationCommand = new AsyncCommand(async (param,param2) =>
            {
                if (param is Node node)
                {
                    // Optional: animate something bound to the UI
                    await AnimateTapAsync(param2);

                    // Call the service directly (no messenger)
                    _centerOn.CenterMapOnLocation(node.Longitude, node.Latitude, 15);
                }
            });

        }


 
        public string SearchText
        {
            get => _searchText!;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                    FilterNodes();
                }
            }
        }

        public ObservableCollection<Node> FilteredNodes
        {
            get => _filteredNodes;
            private set
            {
                if (_filteredNodes != value)
                {
                    _filteredNodes = value;
                    OnPropertyChanged();
                }
            }
        }

        private void FilterNodes()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredNodes = new ObservableCollection<Node>(TestData.Nodes);
            }
            else
            {
                var filtered = TestData.Nodes
                    .Where(n => n.Name != null && n.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                FilteredNodes = new ObservableCollection<Node>(filtered);
            }

            WeakReferenceMessenger.Default.Send(
                new SearchResultsChangedMessage(FilteredNodes.ToList())
            );
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class SearchResultsChangedMessage : ValueChangedMessage<IReadOnlyList<Node>>
    {
        public SearchResultsChangedMessage(IReadOnlyList<Node> nodes) : base(nodes) { }
    }

    public class ZoomToLocationMessage : ValueChangedMessage<Node>
    {
        public ZoomToLocationMessage(Node node) : base(node) { }
    }
}
