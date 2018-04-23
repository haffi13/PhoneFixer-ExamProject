using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    // This class should be in the Model layer. Caused dependency problems.
    public interface ITabItem
    {
        string Header { get; set; }
    }
}
