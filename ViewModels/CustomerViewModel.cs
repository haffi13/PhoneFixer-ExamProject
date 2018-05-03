using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.DialogServices;

namespace ViewModels
{
    public class CustomerViewModel : BaseViewModel, ITabItem
    {
        private ObservableCollection<Customer> customers = new ObservableCollection<Customer>();

        // Keeps an instance of the DialogService instanciated at startup.
        private readonly IDialogService dialogService;
        public CustomerViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
        }
        public string Header { get; set; }

        public ObservableCollection<Customer> Customers
    }
        
        

}
