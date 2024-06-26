﻿using InvoiceSystem.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Controls;

namespace InvoiceSystem.Items
{
    public class clsItemsLogic
    {
        /// <summary>
        /// access to the database class
        /// </summary>
        private clsDataAccess db;

        private clsItemsSQL itemSQL = new clsItemsSQL();

        /// <summary>
        /// variable for the dataset
        /// </summary>
        DataSet ds;

        int count;

        /// <summary>
        /// Upon initial window opening, initialize the database into the data grid
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <exception cref="Exception"></exception>
        public void FillItemGrid(DataGrid dataGrid)
        {
            try
            {
                //Initialize Database
                db = new clsDataAccess();

                int ReturnValues = 0;

                //Execute SQL to select all from db
                ds = db.ExecuteSQLStatement(itemSQL.GetItems(), ref ReturnValues);

                //if the data is not null, add all to dataGrid
                if (ds != null)
                {
                    dataGrid.ItemsSource = ds.Tables[0].DefaultView;
                    count = ds.Tables[0].Rows.Count;
                }
                else
                {
                    dataGrid.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// When a new row is selected in the data grid, update the text boxes to match the row
        /// </summary>
        /// <param name="selected"></param>
        /// <param name="desc"></param>
        /// <param name="cost"></param>
        public void UpdateSelectedText(DataRowView selected, TextBox desc, TextBox cost)
        {
            //set description text box to current row description
            string itemDesc = selected["ItemDesc"].ToString();
            desc.Text = itemDesc;

            //set cost text box to current row item cost
            string itemCost = selected["Cost"].ToString();
            cost.Text = itemCost;
        }

        /// <summary>
        /// Method to handle both adding new database items and editing existing items
        /// </summary>
        /// <param name="newDesc"></param>
        /// <param name="newCost"></param>
        /// <param name="dataGrid"></param>
        /// <exception cref="Exception"></exception>
        public void AddEditData(string newDesc, string newCost, DataGrid dataGrid)
        {
            try
            {
                //If adding, selected index is -1
                if (dataGrid.SelectedIndex == -1)
                {
                    DataRow newRow = ds.Tables[0].NewRow();       

                    newRow[0] = Convert.ToString(++count);

                    newRow[1] = newDesc;
                    newRow[2] = newCost;

                    ds.Tables[0].Rows.Add(newRow);

                    db.ExecuteNonQuery(itemSQL.AddItem(count, newDesc, newCost));
                }
                //else, edit selected index row information
                else
                {
                    ds.Tables[0].Rows[dataGrid.SelectedIndex][1] = newDesc;
                    ds.Tables[0].Rows[dataGrid.SelectedIndex][2] = newCost;

                    string itemCode = ds.Tables[0].Rows[dataGrid.SelectedIndex][0].ToString();


                    db.ExecuteNonQuery(itemSQL.UpdateItem(itemCode, newDesc, newCost));
                }

                ds.AcceptChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Method to handle simple deletion of rows
        /// </summary>
        /// <param name="index"></param>
        /// <exception cref="Exception"></exception>
        public void DeleteItem(int index)
        {
            try
            {
                //create string to use in deletion SQL statement
                string toDelete = ds.Tables[0].Rows[index]["ItemCode"].ToString();
                db.ExecuteNonQuery(itemSQL.DeleteItem(toDelete));

                //Delete Local
                ds.Tables[0].Rows[index].Delete();
                ds.AcceptChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public List<int> GetInvoices(int index, List<int> invoices)
        {
            try
            {
                //create a string item code to use in SQL statement
                string itemCode = ds.Tables[0].Rows[index]["ItemCode"].ToString();
                int invoiceID;

                if (itemCode != null)
                {
                    int retVal = 0;

                    //create new dataset with the invoices from itemCode
                    DataSet ds1 = db.ExecuteSQLStatement(itemSQL.GetInvoicesForItem(itemCode), ref retVal);

                    //for each row, add 
                    foreach (DataRow row in ds1.Tables[0].Rows)
                    {
                        //parse invoiceNum into an int, add to invoice list
                        if (int.TryParse(row["InvoiceNum"].ToString(), out invoiceID))
                        {
                            invoices.Add(invoiceID);
                        }
                    }
                }

                return invoices;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
