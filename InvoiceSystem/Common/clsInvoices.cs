using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.Common
{
    /// <summary>
    /// gets/sets invoice objects
    /// </summary>
    /// <returns> string of object</returns>
    internal class clsInvoices
    {
        public string sInvoiceNum { get; set; }
        public string sInvoiceDate { get; set; }
        public string iTotalCost { get; set; }
    }
}
