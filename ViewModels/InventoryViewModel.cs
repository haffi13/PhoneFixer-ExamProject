using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Models;

namespace ViewModels 
{
    //Change name of TabItem to ITab item
    public class InventoryViewModel : BaseViewModel, ITabItem
    {
        public string Header { get; set; }
        //public object Content { get; set; }

    }
}
