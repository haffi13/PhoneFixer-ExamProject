using System.Windows;
using ViewModels.DialogServices;

namespace Views
{
    /// <summary>
    /// Interaction logic for ConfirmationDialogView.xaml
    /// </summary>
    public partial class ConfirmationDialogView : Window, IDialog
    {
        public ConfirmationDialogView()
        {
            InitializeComponent();
        }
    }
}
