using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Reflection;
using System.Windows.Navigation;

/// <summary>
/// Class used to access the database.
/// </summary>
public class clsItemsSQL
{
    /// <summary>
    /// SQL to retrieve all items from ItemDesc
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public string GetItems()
    {
        try
        {
            string allItemsSQL = "SELECT * FROM ItemDesc";
            return allItemsSQL;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }

    /// <summary>
    /// SQL statement to add an item to the database
    /// </summary>
    /// <param name="count"></param>
    /// <param name="newDesc"></param>
    /// <param name="newCost"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public string AddItem(int count, string newDesc, string newCost)
    {
        try
        {
            string addItemSQL = $"INSERT INTO ItemDesc (ItemCode, ItemDesc, Cost) VALUES ('{count}', '{newDesc}', '{newCost}')";
            return addItemSQL;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }

    /// <summary>
    /// SQL statement to update an existing item based off the itemcode
    /// </summary>
    /// <param name="itemCode"></param>
    /// <param name="newDesc"></param>
    /// <param name="newCost"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public string UpdateItem(int itemCode, string newDesc, string newCost)
    {
        try
        {
            string updateItemSQL = $"UPDATE ItemDesc SET ItemDesc = '{newDesc}', Cost = '{newCost}' WHERE ItemCode = '{itemCode}'";
            return updateItemSQL;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }

    /// <summary>
    /// SQL statement to delete an item
    /// </summary>
    /// <param name="toDelete"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public string DeleteItem(string toDelete)
    {
        try
        {
            string deleteItemSQL = "DELETE FROM ItemDesc WHERE ItemCode = '" + toDelete + "'";
            return deleteItemSQL;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }

    /// <summary>
    /// SQL to get all invoices for a selected item
    /// </summary>
    /// <param name="itemCode"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public string GetInvoicesForItem(string itemCode)
    {
        try
        {
            string getInvoicesSQL = "SELECT InvoiceNum FROM LineItems WHERE ItemCode = '" + itemCode + "'";
            return getInvoicesSQL;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }

}