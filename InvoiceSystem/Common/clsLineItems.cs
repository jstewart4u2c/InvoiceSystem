using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Common
{
    /// <summary>
    /// gets/sets LineItems objects
    /// </summary>
    /// <returns> string of object</returns>
    internal class clsLineItems
    {
        public string sInvoiceNum { get; set; }
        public string sLineItemNumber { get; set; }
        public string sItemCode { get; set; }
    }
}
