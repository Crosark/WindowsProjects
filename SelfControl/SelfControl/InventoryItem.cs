using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfControl
{
    public class InventoryItem
    {
        public string item { get; set; }
        public string thumb { get; set; }
        public bool low { get; set; }
        public string quantity { get; set; }
        public System.Windows.Visibility status { get; set; }
    }
}
