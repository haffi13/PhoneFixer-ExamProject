using ViewModels.DialogServices;
using Models;

namespace ViewModels
{
    public class SaleViewModel : BaseViewModel, ITabItem
    {
        private readonly IDialogService dialogService;
        private Sale sale; 

        public string Header { get; set; }

        public SaleViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;

            sale = Sale.Instance;
        }
    }
}
