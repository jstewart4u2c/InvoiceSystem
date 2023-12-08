using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Reflection;

    /// <summary>
    /// Class used to access the database.
    /// </summary>
	public class clsSearchSQL
	{
    /// <summary>
    /// Gets Distinct InvoiceNmm with sql statement
    /// </summary>
    /// <returns>  string SQL </returns>
    public static string GetInvoice()
    {
        try
        {
            string SQL = "SELECT * FROM Invoices ORDER BY InvoiceNum";
            return SQL;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }
    /// <summary>
    /// Gets Distinct InvoiceNmm with sql statement
    /// </summary>
    /// <returns>  string SQL </returns>
    public static string GetDistinctInvoiceNumber()
    {
        try
        {
            string SQL = "SELECT DISTINCT(InvoiceNum) FROM Invoices ORDER BY InvoiceNum";
            return SQL;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }

    /// <summary>
    /// Gets Distinct InvoiceDate with sql statement
    /// </summary>
    /// <returns>  string SQL </returns>
    public static string GetDistinctInvoiceDate()
    {
        try
        {
            string SQL = "SELECT DISTINCT(InvoiceDate) From Invoices order by InvoiceDate";
            return SQL;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }

    /// <summary>
    /// Gets Distinct InvoiceCosts with sql statement
    /// </summary>
    /// <returns>  string SQL </returns>
    public static string GetDistinctInvoiceCost()
    {
        try
        {
            string SQL = "SELECT DISTINCT(TotalCost) From Invoices order by TotalCost";
            return SQL;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }

    /// <summary>
    /// Get Invoice based on InvoiceNumbers
    /// </summary>
    /// <returns> string SQL </returns>
    public static string FilterInvoiceNumber(string InvoiceNum)
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

    /// <summary>
    /// Get Invoice based on InvoiceDate
    /// </summary>
    /// <returns> string SQL/returns>
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
    /// <summary>
    /// Get Invoice based on InvoiceCost
    /// </summary>
    /// <returns> string SQL</returns>
    public static string FilterInvoiceCost(string InvoiceCost)
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

    /// <summary>
    /// Get Invoice based on invoice num and date
    /// </summary>
    /// <param name="InvoiceNum">invoice num </param>
    /// <param name="InvoiceDate">date of invoice</param>
    /// <returns> string SQL</returns>
    public static string FilterInvoiceNumbersDate(string InvoiceNum, string InvoiceDate)
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

    /// <summary>
    /// Get Invoice based on invoice cost and date
    /// </summary>
    /// <param name="InvoiceCost">invoice cost of product</param>
    /// <param name="InvoiceDate">date of invoice</param>
    /// <returns> string SQL</returns>
    public static string FilterInvoiceCostDate(string InvoiceCost, string InvoiceDate)
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

    /// <summary>
    /// Get Invoice based on invoice Num and Costs
    /// </summary>
    /// <param name="InvoiceNum">invoice num </param>
    /// <param name="InvoiceCost">Cost of invoice</param>
    /// <returns>string SQL</returns>
    public static string FilterInvoiceNumbersCost(string InvoiceNum, string InvoiceCost)
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

    /// <summary>
    /// Get Invoice based on invoice num and date and cost
    /// </summary>
    /// <param name="InvoiceNum">invoice num </param>
    /// <param name="InvoiceDate">date of invoice</param>
    /// <param name="InvoiceCost">Cost of invoice</param>
    /// <returns> string SQL</returns>
    public static string FilterInvoiceNumbersDate(string InvoiceNum, string InvoiceDate, string InvoiceCost)
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

}

