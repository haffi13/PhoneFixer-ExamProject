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
        // Might skip this class and just have the Header property in both classes
        // seperately if there isn't much that can be put here for reuse.
        string Header { get; set; }
    }
}
