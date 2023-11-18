using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Common
{
    /// <summary>
    /// gets/sets ItemDesc objects
    /// </summary>
    /// <returns> string of object</returns>
    internal class clsItemDesc
    {
        public string sItemCode { get; set; }
        public string sItemDesc { get; set; }
        public string sCost { get; set; }
    }
}
