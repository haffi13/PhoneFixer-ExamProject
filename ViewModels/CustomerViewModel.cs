﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Models;

namespace ViewModels
{
    public class CustomerViewModel : BaseViewModel, ITabItem
    {
        public string Header { get; set; }
    }
}
