using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace ViewModels
{
    public class CustomerViewModel : BaseViewModel, TabItem
    {
        string TabItem.Header { get; set; }
        object TabItem.Content { get; set; }

    }
}
