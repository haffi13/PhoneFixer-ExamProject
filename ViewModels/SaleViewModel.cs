using ViewModels.DialogServices;

namespace ViewModels
{
    public class SaleViewModel : BaseViewModel, ITabItem
    {
        private readonly IDialogService dialogService;
        public string Header { get; set; }

        public SaleViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
        }
    }
}
