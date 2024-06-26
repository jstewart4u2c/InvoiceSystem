﻿using Microsoft.VisualBasic;

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
        public string sTotalCost { get; set; }
    }

    /// <summary>
    /// used to pass data around
    /// </summary>
    /// <returns> string of object</returns>
    public static class clsInvoicesPass
    {
        public static string sSelectedInvoiceNum { get; set; }

        public static bool IsUpdating { get; set; }

        public static int OldTotal {  get; set; }

        public static DateAndTime OldDate { get; set; }
    }
}
