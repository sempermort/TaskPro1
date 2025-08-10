using TaskPro1.ViewModels;

public class MapPageCompositeViewModel
{
    public MapPageViewModel MapPageVM { get; }
    public SearchViewModel SearchVM { get; }

    public MapPageCompositeViewModel(MapPageViewModel mapPageVM, SearchViewModel searchVM)
    {
        MapPageVM = mapPageVM;
        SearchVM = searchVM;
    }
}