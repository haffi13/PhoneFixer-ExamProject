using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace ViewModels 
{
    //Change name of TabItem to ITab item
    public class InventoryViewModel : BaseViewModel, TabItem
    {
        string TabItem.Header { get; set; }
        object TabItem.Content { get; set; }

    }
}
