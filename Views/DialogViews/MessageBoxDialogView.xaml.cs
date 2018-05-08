using System.Windows;
using ViewModels.DialogServices;

namespace Views
{
    /// <summary>
    /// Interaction logic for MessageBoxDialogView.xaml
    /// </summary>
    public partial class MessageBoxDialogView : Window, IDialog
    {
        public MessageBoxDialogView()
        {
            InitializeComponent();
        }
    }
}
