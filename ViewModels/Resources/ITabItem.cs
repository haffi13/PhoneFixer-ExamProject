namespace ViewModels
{
    // This class should be in the Model layer. Caused dependency problems.
    // The View layer is not directly dependent on the Model layer and therefore when the DataContext is set for the 
    public interface ITabItem
    {
        string Header { get; set; }
    }
}
