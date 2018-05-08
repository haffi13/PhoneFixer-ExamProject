using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.DialogServices;
using Models;

namespace ViewModels
{
    public class ServiceViewModel : BaseViewModel, ITabItem
    {
        private readonly IDialogService dialogService;
        private Sale sale;
        public ServiceViewModel(IDialogService dialogService, Sale sale)
        {
            this.dialogService = dialogService;
            this.sale = sale;
        }
        public string Header { get; set; }
    }
}
