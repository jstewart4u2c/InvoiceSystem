using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Reflection;
using System.Web;

public class clsMainSQL
{
   
    public clsMainSQL()
    {
       
    }
    /*Add Try Catch*/
    public string GrabbingItemCode(string sItemDesc)
    {
        string sSQL = "SELECT ItemCode FROM ItemDesc WHERE ItemDesc = \"" + sItemDesc + "\"";
        return sSQL;
    }

    public string GrabItemPrice(string sItemDesc)
    {
        string sSQL = "SELECT Cost FROM ItemDesc WHERE ItemDesc = \"" + sItemDesc + "\"";
        return sSQL;
    }

    public string InsertIntoInvoice()
    {
        var TodaysDate = DateOnly.FromDateTime(DateTime.Now);
        int TotalCost = 0;
        string sSQL = "INSERT INTO Invoices(InvoiceDate,TotalCost) VALUES(\"" + TodaysDate +  "\"," + TotalCost + ")";
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

    /*Inner Join Statement For LineItem and ItemDesc to Display on DataGrid*/
    /*Delete From Invoice if Canceled*/
    /*Delete A item in LineItem*/

}
