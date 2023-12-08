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
        public List<clsInvoices> lstInvoice;
        /// <summary>
        /// Gets Distinct InvoiceNumbers with statment
        /// </summary>
        /// <returns> list of string for combobox</returns>
        public List<clsInvoices> GetInvoices()
        {
            try
            {
                //access database
                List<clsInvoices> lstInvoice = new List<clsInvoices>();
                clsDataAccess db = new clsDataAccess();
                DataSet ds = new DataSet();
                int iRetVal = 0;
                //execute statment
                ds = db.ExecuteSQLStatement(clsSearchSQL.GetInvoice(), ref iRetVal);
                //loop through each
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoices invoice = new clsInvoices();
                    invoice.sInvoiceNum = dr[0].ToString();
                    invoice.sInvoiceDate = dr[1].ToString();
                    invoice.sTotalCost = dr[2].ToString();
                    lstInvoice.Add(invoice);
                }
                return lstInvoice;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Gets Distinct InvoiceNumbers with statment
        /// </summary>
        /// <returns> list of string for combobox</returns>
        public List<string> GetDistinctInvoiceNumbers()
        {
            try
            {
                //access database
                List<string> lstInvoiceNum = new List<string>();
                clsDataAccess db = new clsDataAccess();
                DataSet ds = new DataSet();
                int iRetVal = 0;
                //execute statment
                ds = db.ExecuteSQLStatement(clsSearchSQL.GetDistinctInvoiceNumber(), ref iRetVal);
                //loop through each
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

        /// <summary>
        /// Gets Distinct InvoiceDate with sql statement
        /// </summary>
        /// <returns> list of string for combobox</returns>
        public List<string> GetDistinctInvoiceDates()
        {
            try
            {
                //access database
                List<string> lstInvoiceDate = new List<string>();
                clsDataAccess db = new clsDataAccess();
                DataSet ds = new DataSet();
                int iRetVal = 0;
                //execute statment
                ds = db.ExecuteSQLStatement(clsSearchSQL.GetDistinctInvoiceDate(), ref iRetVal);
                //loop through each
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

        /// <summary>
        /// Gets Distinct InvoiceCosts with sql statement
        /// </summary>
        /// <returns> list of string for combobox</returns>
        public List<string> GetDistinctInvoiceCosts()
        {
            try
            {
                //access database
                List<string> lstInvoiceTotalCost = new List<string>();
                clsDataAccess db = new clsDataAccess();
                DataSet ds = new DataSet();
                int iRetVal = 0;
                //execute statment
                ds = db.ExecuteSQLStatement(clsSearchSQL.GetDistinctInvoiceCost(), ref iRetVal);
                //loop through each
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

        /// <summary>
        /// Get Invoice based on InvoiceNumbers
        /// </summary>
        /// <param name="InvoiceNum">invoice num </param>
        /// <returns> list of string for combobox</returns>
        public List<clsInvoices> FilterInvoiceNumbers(string InvoiceNum)
        {
            //****** Future work
            try
            {
                //access database
                List<clsInvoices> lstInvoiceByNumber = new List<clsInvoices>();
                clsDataAccess db = new clsDataAccess();
                DataSet ds = new DataSet();
                int iRetVal = 0;
                //execute statment
                ds = db.ExecuteSQLStatement(clsSearchSQL.FilterInvoiceNumber(InvoiceNum), ref iRetVal);
                //loop through each
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoices invoice = new clsInvoices();
                    invoice.sInvoiceNum = dr[0].ToString();
                    invoice.sInvoiceDate = dr[0].ToString();
                    invoice.sInvoiceNum = dr[0].ToString();
                    lstInvoiceByNumber.Add(invoice);
                }
                return lstInvoiceByNumber;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Get Invoice based on InvoiceDate
        /// </summary>
        /// <param name="InvoiceDate">date of invoice</param>
        /// <returns> list of string for combobox</returns>
        public List<clsInvoices> FilterInvoiceDates(string InvoiceDate)
        {
            //****** Future work
            try
            {
                //access database
                List<clsInvoices> lstInvoiceByNumber = new List<clsInvoices>();
                clsDataAccess db = new clsDataAccess();
                DataSet ds = new DataSet();
                int iRetVal = 0;
                //execute statment
                ds = db.ExecuteSQLStatement(clsSearchSQL.FilterInvoiceDate(InvoiceDate), ref iRetVal);
                //loop through each
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoices invoice = new clsInvoices();
                    invoice.sInvoiceNum = dr[0].ToString();
                    invoice.sInvoiceDate = dr[0].ToString();
                    invoice.sTotalCost = dr[0].ToString();
                    lstInvoiceByNumber.Add(invoice);
                }
                return lstInvoiceByNumber;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Get Invoice based on InvoiceCost
        /// </summary>
        /// <param name="InvoiceCost">Cost of invoice</param>
        /// <returns> list of string for gid</returns>
        public List<clsInvoices> FilterInvoiceCosts(string InvoiceCost)
        {
            //****** Future work
            try
            {
                //access database
                List<clsInvoices> lstInvoiceByNumber = new List<clsInvoices>();
                clsDataAccess db = new clsDataAccess();
                DataSet ds = new DataSet();
                int iRetVal = 0;
                //execute statment
                ds = db.ExecuteSQLStatement(clsSearchSQL.FilterInvoiceCost(InvoiceCost), ref iRetVal);
                //loop through each
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoices invoice = new clsInvoices();
                    invoice.sInvoiceNum = dr[0].ToString();
                    invoice.sInvoiceDate = dr[0].ToString();
                    invoice.sTotalCost = dr[0].ToString();
                    lstInvoiceByNumber.Add(invoice);
                }
                return lstInvoiceByNumber;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Get Invoice based on invoice num and date
        /// </summary>
        /// <param name="InvoiceNum">invoice num </param>
        /// <param name="InvoiceDate">date of invoice</param>
        /// <returns> list of string for grid</returns>
        public List<clsInvoices> FilterInvoiceNumbersDates(string InvoiceNum, string InvoiceDate)
        {
            //****** Future work
            try
            {
                //access database
                List<clsInvoices> lstInvoiceByNumber = new List<clsInvoices>();
                clsDataAccess db = new clsDataAccess();
                DataSet ds = new DataSet();
                int iRetVal = 0;
                //execute statment
                ds = db.ExecuteSQLStatement(clsSearchSQL.FilterInvoiceNumbersDate(InvoiceNum, InvoiceDate), ref iRetVal);
                //loop through each
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoices invoice = new clsInvoices();
                    invoice.sInvoiceNum = dr[0].ToString();
                    invoice.sInvoiceDate = dr[0].ToString();
                    invoice.sTotalCost = dr[0].ToString();
                    lstInvoiceByNumber.Add(invoice);
                }
                return lstInvoiceByNumber;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Get Invoice based on invoice cost and date
        /// </summary>
        /// <param name="InvoiceCost">invoice cost of product</param>
        /// <param name="InvoiceDate">date of invoice</param>
        /// <returns> list of string for grid</returns>
        public List<clsInvoices> FilterInvoiceCostDates(string InvoiceCost, string InvoiceDate)
        {
            //****** Future work
            try
            {
                //access database
                List<clsInvoices> lstInvoiceByNumber = new List<clsInvoices>();
                clsDataAccess db = new clsDataAccess();
                DataSet ds = new DataSet();
                int iRetVal = 0;
                //execute statment
                ds = db.ExecuteSQLStatement(clsSearchSQL.FilterInvoiceCostDate(InvoiceCost, InvoiceDate), ref iRetVal);
                //loop through each
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoices invoice = new clsInvoices();
                    invoice.sInvoiceNum = dr[0].ToString();
                    invoice.sInvoiceDate = dr[0].ToString();
                    invoice.sTotalCost = dr[0].ToString();
                    lstInvoiceByNumber.Add(invoice);
                }
                return lstInvoiceByNumber;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Get Invoice based on invoice Num and Costs
        /// </summary>
        /// <param name="InvoiceNum">invoice num </param>
        /// <param name="InvoiceCost">Cost of invoice</param>
        /// <returns> list of string for grid</returns>
        public List<clsInvoices> FilterInvoiceNumbersCosts(string InvoiceNum, string InvoiceCost)
        {
            //****** Future work
            try
            {
                //access database
                List<clsInvoices> lstInvoiceByNumber = new List<clsInvoices>();
                clsDataAccess db = new clsDataAccess();
                DataSet ds = new DataSet();
                int iRetVal = 0;
                //execute statment
                ds = db.ExecuteSQLStatement(clsSearchSQL.FilterInvoiceNumbersCost(InvoiceNum, InvoiceCost), ref iRetVal);
                //loop through each
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoices invoice = new clsInvoices();
                    invoice.sInvoiceNum = dr[0].ToString();
                    invoice.sInvoiceDate = dr[0].ToString();
                    invoice.sTotalCost = dr[0].ToString();
                    lstInvoiceByNumber.Add(invoice);
                }
                return lstInvoiceByNumber;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Get Invoice based on invoice num and date and cost
        /// </summary>
        /// <param name="InvoiceNum">invoice num </param>
        /// <param name="InvoiceDate">date of invoice</param>
        /// <param name="InvoiceCost">Cost of invoice</param>
        /// <returns> list of string for grid</returns>
        public List<clsInvoices> FilterInvoiceNumbersDates(string InvoiceNum, string InvoiceDate, string InvoiceCost)
        {
            //****** Future work
            try
            {
                //access database
                List<clsInvoices> lstInvoiceByNumber = new List<clsInvoices>();
                clsDataAccess db = new clsDataAccess();
                DataSet ds = new DataSet();
                int iRetVal = 0;
                //execute statment
                ds = db.ExecuteSQLStatement(clsSearchSQL.FilterInvoiceNumbersDate(InvoiceNum, InvoiceDate, InvoiceCost), ref iRetVal);
                //loop through each
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsInvoices invoice = new clsInvoices();
                    invoice.sInvoiceNum = dr[0].ToString();
                    invoice.sInvoiceDate = dr[0].ToString();
                    invoice.sTotalCost = dr[0].ToString();
                    lstInvoiceByNumber.Add(invoice);
                }
                return lstInvoiceByNumber;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }
}
