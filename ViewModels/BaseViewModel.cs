using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ViewModels
{
    // BaseViewModel is inherited by all other view models.
    // It saves on implementing INotifyPropertyChanged individually for each view model.
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // ([CallerMemberName]) gives OnPropertyChanged the name of the calling member
        // without it having to be passed as a parameter to the method.
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}