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
        
        clsMainSQL sqlQuery = new clsMainSQL();
        clsDataAccess db = new clsDataAccess();
        DataSet ds;
        public int AddInvoice()
        {
            try { 
                /*Values, TotalPrice will be added At another time*/
                /*NEEDS TO BE CLEANED UP, PLEASE IGNORE*/
                int InvoiceNumber;
                /*Insert Date and Blank Cost*/
                string InsertStatement = sqlQuery.InsertIntoInvoice();
                db.ExecuteNonQuery(InsertStatement);
                
                /*Make Method that if User Presses Cancel, Lastest Invoice Will be Deleted*/
                string GrabInvoiceNumber = sqlQuery.GrabLastestInvoiceID();
                InvoiceNumber = Convert.ToInt32(db.ExecuteScalarSQL(GrabInvoiceNumber));
                /*TestDelete so it doesnt save until i got everything working*/
                string DeleteTest = sqlQuery.DeleteTest(InvoiceNumber);
                db.ExecuteNonQuery(DeleteTest);
                
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
            try { 
           int iRef = 0;

            ds = db.ExecuteSQLStatement("SELECT * FROM LineItems", ref iRef);

            DataRow DR = ds.Tables[0].NewRow();
            DR[0] = InvoiceNumber;
            DR[1] = ItemCount;

            string ItemCodeQuery;
            string ItemCode;

            ItemCodeQuery = sqlQuery.GrabbingItemCode(ItemDesc);
            ItemCode = db.ExecuteScalarSQL(ItemCodeQuery);
            DR[2] = ItemCode;

            /*Display On DataGrid*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public int TotalPrice()
        {
            //Calculate the Total Price from all the Items in the List
            //Grab Invoice Number with LineItem and for loop with The Line Numbers in the Invoice
            //Add up the Item Codes Prices and Return
            //Add Total Price to Invoice Table where Invoice = InvoiceNumber
            return 0;
        }

        /*Method for Search to Call
         *Invoice Number will be Sent over to main and reopen Main Window
         *This will then Grab all sql information needed and prompt Main to Display
         */
    }
}
