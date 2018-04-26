﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.DialogServices;

namespace ViewModels
{
    public class MessageBoxDialogViewModel : BaseViewModel, IDialogRequestClose
    {
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        private string message;
        
        public RelayCommand ConfirmCommand { get; }
        public RelayCommand CancelCommand { get; }

        // Bound to the TextBlock in the MessageBoxDialogView.
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
            }
        }


        public MessageBoxDialogViewModel(string message)
        {
            Message = message;
            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void Confirm()
        {
            
        }
        private void Cancel()
        {

        }
    }
}
