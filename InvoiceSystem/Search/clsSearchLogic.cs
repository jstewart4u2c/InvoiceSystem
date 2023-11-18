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
using System.Globalization;

namespace InvoiceSystem.Search
{
    internal class clsSearchLogic
    {
        //Get DISTINCT InvoiceNumbers
        public List<clsInvoices> lstInvoice;
        public List<string> GetDistinctInvoiceNumbers()
        {
            try
            {
                List<string> lstInvoiceNum = new List<string>();
                clsSearchSQL db = new clsSearchSQL();
                DataSet ds = new DataSet();
                int iRetVal = 0;

                ds = db.ExecuteSQLStatement("SELECT DISTINCT(InvoiceNum) FROM Invoices ORDER BY InvoiceNum", ref iRetVal);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lstInvoiceNum.Add(dr[0].ToString());
                }
                return lstInvoiceNum;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        //Get DISTINCT InvoiceDate
        public List<string> GetDistinctInvoiceDate()
        {
            try
            {
                List<string> lstInvoiceDate = new List<string>();
                clsSearchSQL db = new clsSearchSQL();
                DataSet ds = new DataSet();
                int iRetVal = 0;

                ds = db.ExecuteSQLStatement("SELECT DISTINCT(InvoiceDate) From Invoices order by InvoiceDate", ref iRetVal);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lstInvoiceDate.Add(dr[0].ToString());
                }
                return lstInvoiceDate;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        //Get DISTINCT InvoiceCost
        public List<string> GetDistinctInvoiceCost()
        {
            try
            {
                List<string> lstInvoiceTotalCost = new List<string>();
                clsSearchSQL db = new clsSearchSQL();
                DataSet ds = new DataSet();
                int iRetVal = 0;

                ds = db.ExecuteSQLStatement("SELECT DISTINCT(TotalCost) From Invoices order by TotalCost", ref iRetVal);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lstInvoiceTotalCost.Add(dr[0].ToString());
                }
                return lstInvoiceTotalCost;
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

        // Get invoice ID 

        //GetInvoices (InvoiceNumber, InvoiceDate, TotalCost) - returns List <clsInvoices>

    }
}
