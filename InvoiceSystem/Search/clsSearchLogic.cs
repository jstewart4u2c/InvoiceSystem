using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Reflection;
using System.Data;
using System.Xml.Linq;
using InvoiceSystem.Common;

namespace InvoiceSystem.Search
{
    internal class clsSearchLogic
    {
        //Get DISTINCT InvoiceNumbers
        public List<clsInvoices> lstInvoices;
        public List<clsInvoices> GetDistinctInvoiceNumbers()
        {
            try
            {
                clsSearchSQL db = new clsSearchSQL();
                DataSet ds = new DataSet();
                int iRetVal = 0;

                ds = db.ExecuteSQLStatement( "SELECT DISTINCT(InvoiceNum) FROM Invoices ORDER BY InvoiceNum", ref iRetVal);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoices invoices = new clsInvoices();
                    invoices.iInvoiceNum = Convert.ToInt32(dr[0]);
                    invoices.sInvoiceDate = dr[1].ToString();
                    invoices.iTotalCost = Convert.ToDouble(dr[2]);
                    lstInvoices.Add(invoices);
                }
                return lstInvoices;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        //Get DISTINCT InvoiceDate
        public static string GetDistinctInvoiceDate()
        {
            try
            {
                string sSQL = "SELECT DISTINCT(InvoiceDate) From Invoices order by InvoiceDate";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        //Get DISTINCT InvoiceCost
        public static string GetDistinctInvoiceCost()
        {
            try
            {
                string sSQL = "SELECT DISTINCT(TotalCost) From Invoices order by TotalCost";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        //Get Invoice based on InvoiceNumbers
        public static string FilterInvoiceNumbers(int InvoiceNum)
        {
            try
            {
                string sSQL = "SELECT* FROM Invoices WHERE InvoiceNum = " + InvoiceNum.ToString();
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        //Get Invoice based on InvoiceDate
        public static string FilterInvoiceDate(string InvoiceDate)
        {
            try
            {
                string sSQL = ("SELECT* FROM Invoices WHERE InvoiceDate = #" + InvoiceDate + "#");
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        //Get Invoice based on InvoiceCost
        public static string FilterInvoiceCost(float InvoiceCost)
        {
            try
            {
                string sSQL = ("SELECT* FROM Invoices WHERE TotalCost = " + InvoiceCost.ToString());
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public static string FilterInvoiceNumbersDate(int InvoiceNum, string InvoiceDate)
        {
            try
            {
                string sSQL = ("SELECT* FROM Invoices WHERE InvoiceNum = " + InvoiceNum.ToString() + " AND + InvoiceDate = #" + InvoiceDate + "#");
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public static string FilterInvoiceCostDate(float InvoiceCost, string InvoiceDate)
        {
            try
            {
                string sSQL = ("SELECT* FROM Invoices WHERE TotalCost = " + InvoiceCost.ToString() + " AND + InvoiceDate = #" + InvoiceDate + "#");
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public static string FilterInvoiceNumbersCost(int InvoiceNum, float InvoiceCost)
        {
            try
            {
                string sSQL = ("SELECT* FROM Invoices WHERE InvoiceNum = " + InvoiceNum.ToString() + " AND + TotalCost = " + InvoiceCost.ToString());
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public static string FilterInvoiceNumbersDate(int InvoiceNum, string InvoiceDate, float InvoiceCost)
        {
            try
            {
                string sSQL = ("SELECT* FROM Invoices WHERE InvoiceNum = " + InvoiceNum.ToString() + " AND + InvoiceDate = #" + InvoiceDate + "#" + " AND + TotalCost = " + InvoiceCost.ToString());
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }



        //GetInvoices (InvoiceNumber, InvoiceDate, TotalCost) - returns List <clsInvoices>

    }
}
