namespace ViewModels
{
    // This class should be in the Model layer. Caused dependency problems.
    public interface ITabItem
    {
        string Header { get; set; }
    }
}
