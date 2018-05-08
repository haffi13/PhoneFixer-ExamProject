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
        public ServiceViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
        }
        public string Header { get; set; }
    }
}
