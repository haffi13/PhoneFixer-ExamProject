using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.DialogServices;
using Models;

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

        }
        private void AddService()
        {

        }
        private void EditService()
        {

        }
        private void DeleteService()
        {

        }
        
    }
}
