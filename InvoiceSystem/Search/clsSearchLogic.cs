﻿using System;
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

        /// <summary>
        /// Gets Distinct InvoiceNumbers with statment
        /// </summary>
        /// <returns> list of string for combobox</returns>
        public List<clsInvoices> lstInvoice;
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
        public List<clsInvoices> FilterInvoiceNumbers(int InvoiceNum)
        {
            //****** Future work
            try
            {
                //Access database 
                //get sql FilterInvoiceNumber(int InvoiceNum);
                //loop
                return lstInvoice;
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
                //Access database 
                //get sql FilterInvoiceDate(int InvoiceNum);
                //loop
                //FilterInvoiceDate(string InvoiceDate)
                return lstInvoice;
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
        public List<clsInvoices> FilterInvoiceCosts(float InvoiceCost)
        {
            //****** Future work
            try
            {
                //Access database 
                //get sql FilterInvoiceCosts(int InvoiceNum);
                //loop
                //FilterInvoiceCost(float InvoiceCost)
                return lstInvoice;
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
        public List<clsInvoices> FilterInvoiceNumbersDates(int InvoiceNum, string InvoiceDate)
        {
            //****** Future work
            try
            {
                //Access database 
                //get sql FilterInvoiceNumbersDate(int InvoiceNum, string InvoiceDate)
                //loop
                return lstInvoice;
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
        public List<clsInvoices> FilterInvoiceCostDates(float InvoiceCost, string InvoiceDate)
        {
            //****** Future work
            try
            {
                //Access database 
                //get sql FilterInvoiceCostDate(float InvoiceCost, string InvoiceDate)
                //loop

                return lstInvoice;
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
        public List<clsInvoices> FilterInvoiceNumbersCosts(int InvoiceNum, float InvoiceCost)
        {
            //****** Future work
            try
            {
                //Access database 
                //get sql FilterInvoiceNumbersCost(int InvoiceNum, float InvoiceCost)
                //loop
                
                return lstInvoice;
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
        public List<clsInvoices> FilterInvoiceNumbersDates(int InvoiceNum, string InvoiceDate, float InvoiceCost)
        {
            //****** Future work
            try
            {
                //Access database 
                //get sql FilterInvoiceNumbersDate(int InvoiceNum, string InvoiceDate, float InvoiceCost)
                //loop
                
                return lstInvoice;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }
}
