using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ViewModels.DialogServices;

namespace Views
{
    /// <summary>
    /// Interaction logic for CustomerDialogView.xaml
    /// </summary>
    public partial class CustomerDialogView : Window, IDialog
    {
        public CustomerDialogView()
        {
            InitializeComponent();
        }
    }
}
