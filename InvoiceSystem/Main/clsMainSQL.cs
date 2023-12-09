using InvoiceSystem.Common;
using System;
using System.Reflection;

public class clsMainSQL
{

    public clsMainSQL()
    {

    }
/**************SELECT STATEMENTS*************/
    public string GrabItemPrice(string sItemDesc)
    {   
        string sSQL = "SELECT Cost FROM ItemDesc WHERE ItemDesc = \"" + sItemDesc + "\"";
        return sSQL;
    }

    public string GrabLastestInvoiceID()
    {
        string sSQL = "SELECT MAX(InvoiceNum) FROM Invoices";
        return sSQL;
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

    public string InsertIntoInvoice()
    {
        var TodaysDate = DateOnly.FromDateTime(DateTime.Now);
        int TotalCost = 0;
        string sSQL = "INSERT INTO Invoices(InvoiceDate,TotalCost) VALUES(\"" + TodaysDate + "\"," + TotalCost + ")";
        return sSQL;
    }

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

    public string GrabAllFromItemDesc()
    {
        try
        {
            string SQL = "SELECT * FROM ItemDesc";
            return SQL;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }

    public string GrabCountForCurrentInvoice()
    {
        try
        {
            string SQL = "SELECT COUNT (LineItemNum) FROM LineItems WHERE InvoiceNum = " + clsInvoicesPass.sSelectedInvoiceNum;
            return SQL;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }

    public string GrabDateForCurrentInvoice()
    {
        try
        {
            string SQL = "SELECT InvoiceDate FROM Invoices WHERE InvoiceNum = " + clsInvoicesPass.sSelectedInvoiceNum;
            return SQL;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }
    /***********UPDATE OR INSERT STATEMENTS************/

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

    public string UpdateInvoiceDate(string InvoiceNum, DateTime InvoiceDate)
    {
        try
        {
            string SQL = "UPDATE Invoices SET InvoiceDate = \"" + InvoiceDate + "\"  WHERE InvoiceNum = " + InvoiceNum;
            return SQL;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }

    /******DELETE STATEMENTS******/
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

    /**********COPYING TABLE QUERIES***********/
    public string CheckIfCopyExist()
    {
        try
        {
            string SQL = "SELECT * FROM CopyCat";
            return SQL;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }


    public string CopyOver()
    {
        try
        {
            string SQL = "SELECT LineItems.* INTO CopyCat FROM LineItems";
            return SQL;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }

    public string DeleteCopy()
    {
        try
        {
            string SQL = "DROP TABLE CopyCat";
            return SQL;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }

    public string ResetOGTable()
    {
        try
        {
            string SQL = "SELECT CopyCat.* INTO LineItems FROM CopyCat";
            return SQL;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }

    public string DeleteLineItemsForASecond()
    {
        try
        {
            string SQL = "DROP TABLE LineItems";
            return SQL;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }
}
