using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public interface TabItem
    {
        string Header { get; set; }
        // The Content is a user control.
        object Content { get; set; } 
    }
}
