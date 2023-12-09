using InvoiceSystem.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace InvoiceSystem.Main
{
    internal class clsMainLogic
    {
        //Values And Connections
        clsMainSQL sqlQuery = new clsMainSQL();
        clsDataAccess db = new clsDataAccess();
        DataSet ds;
        clsInvoices invoices = new clsInvoices();
        int count;
        public int TotalPrice;
        string CurrentInvoice;

/*********ADDING STUFF*********/
        /// <summary>
        /// Adding A Invoice, Enables The Bottom Half Of Screen
        /// Grabs Invoice Number
        /// </summary>
        /// <returns></returns>
        public string AddInvoice()
        {
            try
            {
                /*Values, TotalPrice will be added At another time*/
                /*Insert Date and Blank Cost*/
                string InsertStatement = sqlQuery.InsertIntoInvoice();
                db.ExecuteNonQuery(InsertStatement);
                TotalPrice = 0;
                /*Make Method that if User Presses Cancel, Lastest Invoice Will be Deleted*/
                string GrabInvoiceNumber = sqlQuery.GrabLastestInvoiceID();
                CurrentInvoice = (db.ExecuteScalarSQL(GrabInvoiceNumber));

                return CurrentInvoice;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Adding Items User Added To The LineItems Table 
        /// </summary>
        /// <param name="ItemDesc"></param>
        /// <param name="InvoiceNumber"></param>
        /// <param name="ItemCount"></param>
        /// <param name="ItemPrice"></param>
        public void AddToLineItemsTable(string ItemDesc, string InvoiceNumber, int ItemCount, string ItemPrice)
        {
            try
            {
                int iRet = 0;
                ds = db.ExecuteSQLStatement(sqlQuery.GrabLineItem(InvoiceNumber), ref iRet);
                DataRow newRow = ds.Tables[0].NewRow();

                //Need ItemCode 
                string ItemCode = db.ExecuteScalarSQL(sqlQuery.GrabItemCode(ItemDesc));
                newRow[0] = InvoiceNumber;
                newRow[1] = ItemCount;

                newRow[2] = ItemCode;

                ds.Tables[0].Rows.Add(newRow);

                db.ExecuteNonQuery(sqlQuery.AddItemToLineItems(InvoiceNumber, ItemCount, ItemCode));
                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


/********UPDATE************/

        /// <summary>
        /// Once Use Presses Save, Update Invoice Table
        /// </summary>
        /// <param name="InvoiceNum"></param>
        public void UpdateTotalPrice(string InvoiceNum)
        {
            try
            {
                db.ExecuteScalarSQL(sqlQuery.UpdateInvoiceTable(TotalPrice, InvoiceNum));
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updating The DataGrid To Match The Current Correct Data
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <exception cref="Exception"></exception>
        public void UpdateDataGrid(DataGrid dataGrid)
        {
            try
            {
                TotalPrice = 0;
                List<clsCurrentOrder> CurrentOrder = new List<clsCurrentOrder>();
                int iRef = 0;

                //Grab ItemDesc And ItemPrice of Current Order Items
                ds = db.ExecuteSQLStatement(sqlQuery.GrabOrderDetails(CurrentInvoice), ref iRef);
                //if the data is not null, add all to dataGrid
                if (ds != null)
                {

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {

                        clsCurrentOrder itemInfo = new clsCurrentOrder();

                        itemInfo.sCount = dr[0].ToString();
                        itemInfo.sCost = "$" + dr[1].ToString();
                        itemInfo.sItemDesc = dr[2].ToString();
                        CurrentOrder.Add(itemInfo);

                        TotalPrice += Convert.ToInt32(dr[1]);
                    }
                    dataGrid.ItemsSource = CurrentOrder;
                }
                else { dataGrid.ItemsSource = null; }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

//******DELETE METHODS*****/
        /// <summary>
        /// Deleting The Selected Item
        /// </summary>
        /// <param name="index"></param>
        public void DeleteItemOffList(int index)
        {
            try
            {
                int iRef = 0;
                ds = db.ExecuteSQLStatement(sqlQuery.GrabLineItem(CurrentInvoice), ref iRef);

                string SelectedIndex = ds.Tables[0].Rows[index]["LineItemNum"].ToString();
                db.ExecuteNonQuery(sqlQuery.DeleteAItem(SelectedIndex,CurrentInvoice));

                //Need To Change Indexes To Correctly Show the Number Of Items 
                //Everything After Deleted Goes Down One 
                db.ExecuteNonQuery(sqlQuery.UpdateLineItemNumber(SelectedIndex, CurrentInvoice));

                ds.Tables[0].Rows[index].Delete();
                ds.AcceptChanges();

            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public void DeleteAll()
        {
            db.ExecuteNonQuery(sqlQuery.DeleteNewAddOnToLineItems(CurrentInvoice));
            db.ExecuteNonQuery(sqlQuery.DeleteNewAddOnToInvoice(CurrentInvoice));
        }

//*****METHODS THAT TALK TO SEARCH WINDOW******/
        public void UpdateCurrentInvoice(string InvoiceNum)
        {
            CurrentInvoice = InvoiceNum;
            
        }
    }
}
