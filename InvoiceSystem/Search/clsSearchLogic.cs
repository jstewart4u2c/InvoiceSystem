using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;

namespace InvoiceSystem.Search
{
    internal class clsSearchLogic
    {


        //Get DISTINCT InvoiceNumbers
        public static string GetDistinctInvoiceNumbers()
        {
            return ("SELECT DISTINCT(InvoiceNum) From Invoices order by InvoiceNum");
        }
        //Get DISTINCT InvoiceDate
        public static string GetDistinctInvoiceDate()
        {
            return "SELECT DISTINCT(InvoiceDate) From Invoices order by InvoiceDate";
        }
        //Get DISTINCT InvoiceCost
        public static string GetDistinctInvoiceCost()
        {
            return "SELECT DISTINCT(TotalCost) From Invoices order by TotalCost";
        }


        //Get Invoice based on InvoiceNumbers
        public static string FilterInvoiceNumbers(int InvoiceNum)
        {
            return ("SELECT* FROM Invoices WHERE InvoiceNum = " + InvoiceNum.ToString());
        }
        //Get Invoice based on InvoiceDate
        public static string FilterInvoiceDate(string InvoiceDate)
        {
            return ("SELECT* FROM Invoices WHERE InvoiceDate = #" + InvoiceDate + "#");
        }
        //Get Invoice based on InvoiceCost
        public static string FilterInvoiceCost(float InvoiceCost)
        {
            return ("SELECT* FROM Invoices WHERE TotalCost = " + InvoiceCost.ToString());
        }

        public static string FilterInvoiceNumbersDate(int InvoiceNum, string InvoiceDate)
        {
            return ("SELECT* FROM Invoices WHERE InvoiceNum = " + InvoiceNum.ToString()+ " AND + InvoiceDate = #" + InvoiceDate + "#");
        }

        public static string FilterInvoiceCostDate(float InvoiceCost, string InvoiceDate)
        {
            return ("SELECT* FROM Invoices WHERE TotalCost = " + InvoiceCost.ToString()+ " AND + InvoiceDate = #" + InvoiceDate + "#");
        }

        public static string FilterInvoiceNumbersCost(int InvoiceNum, float InvoiceCost)
        {
            return ("SELECT* FROM Invoices WHERE InvoiceNum = " + InvoiceNum.ToString() + " AND + TotalCost = " + InvoiceCost.ToString());
        }

        public static string FilterInvoiceNumbersDate(int InvoiceNum, string InvoiceDate, float InvoiceCost)
        {
            return ("SELECT* FROM Invoices WHERE InvoiceNum = " + InvoiceNum.ToString() + " AND + InvoiceDate = #" + InvoiceDate + "#" + " AND + TotalCost = " + InvoiceCost.ToString());
        }



        //GetInvoices (InvoiceNumber, InvoiceDate, TotalCost) - returns List <clsInvoices>

    }
}
