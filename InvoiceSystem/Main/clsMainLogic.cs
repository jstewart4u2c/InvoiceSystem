using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InvoiceSystem.Main
{
    internal class clsMainLogic
    {
        /*
         * Figure out total Price 
         * 
         */
        clsMainSQL db;
        DataSet ds;


        public int AddInvoice()
        {
            try { 
                /*Values, TotalPrice will be added At another time*/
                int iRef = 0;
                int InvoiceNumber;
                var TodaysDate = DateOnly.FromDateTime(DateTime.Now);
                double totalPrice = 0;

                db = new clsMainSQL();
                //Grab Data
                ds = db.ExecuteSQLStatement("SELECT * FROM Invoices", ref iRef);

                DataRow DR = ds.Tables[0].NewRow();
                DataRow Lastrow = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1];

                int ConvertLastRow = Convert.ToInt32(Lastrow[0]);

                DR[0] = ConvertLastRow++;
                InvoiceNumber = ConvertLastRow;

                DR[1] = TodaysDate.ToString();
                DR[2] = totalPrice;
                /**TODO SAVE CHANGES*/
                return InvoiceNumber;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
            return 0;
        }

        public void AddToLineItemsTable(string ItemDesc, string InvoiceNumber, int ItemCount)
        {
           /*
            * Fill In LineItems 
            * Invoice Number, Grab From Main 
            * LineItemNumber, Item Count
            * ItemCode, Query, Match with ItemDesc
            */
           int iRef = 0;
           db = new clsMainSQL();

            ds = db.ExecuteSQLStatement("SELECT * FROM LineItems", ref iRef);

            DataRow DR = ds.Tables[0].NewRow();
            DR[0] = InvoiceNumber;
            DR[1] = ItemCount;

            string ItemCodeQuery;
            ItemCodeQuery = "SELECT ItemCode FROM ItemDesc WHERE ItemDesc = \"" + ItemDesc + "\"";

        }

        public int TotalPrice()
        {
            //Calculate the Total Price from all the Items in the List
            //Grab Invoice Number with LineItem and for loop with The Line Numbers in the Invoice
            //Add up the Item Codes Prices and Return
            //Add Total Price to Invoice Table where Invoice = InvoiceNumber
            return 0;
        }
    }
}
