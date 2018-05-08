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
    public class ServiceViewModel : BaseViewModel, ITabItem
    {
        private ObservableCollection<Service> services = new ObservableCollection<Service>();
        private Service selectedService;

        private readonly IDialogService dialogService;

        public string Header { get; set; }

        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand DeleteCommand { get; }

        public ObservableCollection<Service> Services
        {
            get { return services; }
            set
            {
                services = value;
                OnPropertyChanged();
            }
        }

        public Service SelectedService
        {
            get { return selectedService; }
            set
            {
                selectedService = value;
                OnPropertyChanged();
            }
        }
        public ServiceViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;

            RefreshService();

            AddCommand = new RelayCommand(AddService);
            EditCommand = new RelayCommand(EditService);
            DeleteCommand = new RelayCommand(DeleteService);
        }

        private void RefreshService()
        {
            Dictionary<List<Service>, string> temp = DatabaseReader.GetService();
            string errorMessage = temp.Values.FirstOrDefault();
            if (errorMessage == string.Empty)
            {
                Services = new ObservableCollection<Service>(temp.Keys.FirstOrDefault());
            }
            else
            {
                bool? result = dialogService.ShowDialog
                        (new MessageBoxDialogViewModel(Message.GetServiceError + errorMessage, Message.ServiceErrorTitle));
            }

        }
        private void AddService()
        {
            bool? result = dialogService.ShowDialog(new ServiceDialogViewModel(dialogService));
            RefreshService();
        }
        private void EditService()
        {
            if (SelectedService != null)
            {
                bool? result = dialogService.ShowDialog(new CustomerDialogViewModel(SelectedService, dialogService));
                RefreshService();
            }
        }
        private void DeleteService()
        {
            if (SelectedService != null)
            {
                string errorMessage = DatabaseWriter.DeleteService(SelectedService);
                if (errorMessage != string.Empty)
                {
                    bool? result = dialogService.ShowDialog
                        (new MessageBoxDialogViewModel(errorMessage, Message.ServiceErrorTitle));
                }
                else
                {
                    RefreshService();
                }
            }

        }
        
    }
}
