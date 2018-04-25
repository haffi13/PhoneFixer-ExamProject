﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.DialogServices;

namespace ViewModels
{
    public class CustomerViewModel : BaseViewModel, ITabItem
    {
        IDialogService dialogService;
        public CustomerViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
        }
        public string Header { get; set; }
    }
}
