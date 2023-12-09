using System;
using System.Reflection;

public class clsMainSQL
{

    public clsMainSQL()
    {

    }
    /*Add Try Catch*/

    public string GrabItemPrice(string sItemDesc)
    {
        string sSQL = "SELECT Cost FROM ItemDesc WHERE ItemDesc = \"" + sItemDesc + "\"";
        return sSQL;
    }

    public string InsertIntoInvoice()
    {
        var TodaysDate = DateOnly.FromDateTime(DateTime.Now);
        int TotalCost = 0;
        string sSQL = "INSERT INTO Invoices(InvoiceDate,TotalCost) VALUES(\"" + TodaysDate + "\"," + TotalCost + ")";
        return sSQL;
    }

    public string GrabLastestInvoiceID()
    {
        string sSQL = "SELECT MAX(InvoiceNum) FROM Invoices";
        return sSQL;
    }

    public string DeleteTest(int InvoiceID)
    {
        string sSQL = "DELETE FROM Invoices WHERE InvoiceNum = " + InvoiceID;
        return sSQL;
    }

    public string AddItemToLineItems(string InvoiceNum, int ItemCount, string ItemCode)
    {
        try
        {
            string addItemSQL = $"INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) VALUES ('{InvoiceNum}', '{ItemCount}', '{ItemCode}')";
            return addItemSQL;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }

    public string GrabItemCode(string ItemDesc)
    {
        try
        {
            string GrabCode =
                $"SELECT ItemCode FROM ItemDesc WHERE ItemDesc = \"" + (ItemDesc) + "\"";
            return GrabCode;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }

    ///<summary>
    ///Grab Correct LineItem Values From InvoiceNum
    ///</summary>
    public string GrabLineItem(string InvoiceNumber)
    {
        try
        {
            string SQL = "SELECT * FROM LineItems WHERE InvoiceNum = " + (InvoiceNumber);
            return SQL;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }

    /// <summary>
    /// Grabbing the Information needed to display in The clsMain DataGrid
    /// </summary>
    /// <param name="CurrentInvoice"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public string GrabOrderDetails(string CurrentInvoice)
    {
        try
        {
            string SQL = "SELECT LineItems.LineItemNum, ItemDesc.Cost ,ItemDesc.ItemDesc FROM ItemDesc INNER JOIN LineItems on ItemDesc.ItemCode = LineItems.ItemCode WHERE LineItems.InvoiceNum = " + CurrentInvoice;
            return SQL;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }

    public string UpdateInvoiceTable(decimal TotalPrice, string InvoiceNum)
    {
        try
        {
            string SQL = "UPDATE Invoices SET TotalCost = " + TotalPrice + " WHERE InvoiceNum = " + InvoiceNum;
            return SQL;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }

    public string UpdateLineItemNumber(string LineItemNum, string InvoiceNum)
    {
        try
        {
            string SQL = "UPDATE LineItems SET LineItemNum = LineItemNum - 1 WHERE InvoiceNum = " + InvoiceNum + " AND LineItemNum > " + LineItemNum;
            return SQL;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }
    /******DELETE STATEMENTS******/

    /// <summary>
    /// Deleting A Item Off The Current DataGrid
    /// </summary>
    /// <param name="LineItemNum"></param>
    /// <param name="InvoiceNum"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public string DeleteAItem(string LineItemNum, string InvoiceNum)
    {
        try
        {
            string SQL = "DELETE FROM LineItems WHERE LineItemNum = " + LineItemNum + " AND InvoiceNum = " + InvoiceNum;
            return SQL;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }

    public string DeleteNewAddOnToLineItems(string InvoiceNum)
    {
        try
        {
            string SQL = "DELETE FROM LineItems WHERE InvoiceNum = " + InvoiceNum;
            return SQL;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }

    public string DeleteNewAddOnToInvoice(string InvoiceNum)
    {
        try
        {
            string SQL = "DELETE FROM Invoices WHERE InvoiceNum = " + InvoiceNum;
            return SQL;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }
}
