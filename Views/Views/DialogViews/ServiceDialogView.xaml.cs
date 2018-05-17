using System.Windows;
using ViewModels.DialogServices;

namespace Views
{
    /// <summary>
    /// Interaction logic for ServiceDialogView.xaml
    /// </summary>
    public partial class ServiceDialogView : Window, IDialog
    {
        public ServiceDialogView()
        {
            InitializeComponent();
        }
    }
}
