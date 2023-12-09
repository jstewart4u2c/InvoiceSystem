using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace InvoiceSystem.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        #region Attributes
        /// <summary>
        /// Boolean variable to be accessed by main to check if something has been changed
        /// </summary>
        public static bool itemListModified;
        #endregion

        /// <summary>
        /// Opens new logic class
        /// </summary>
        clsItemsLogic logic = new clsItemsLogic();

        /// <summary>
        /// Initialize window items, fill datagrid, and disable the delete button by default
        /// </summary>
        /// <exception cref="Exception"></exception>
        public wndItems()
        {
            try
            {
                InitializeComponent();

                //Fill dataGrid using logic class
                logic.FillItemGrid(itemDataGrid);

                //editItemButton.IsEnabled = false;
                deleteItemButton.IsEnabled = false;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// when Main Menu is clicked, hide current window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// When user clicks search, hide this window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void SearchMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// When current row is changed, update text boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private void DataGrid_Select(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (itemDataGrid.SelectedItem != null)
                {
                    //re-enable delete button
                    deleteItemButton.IsEnabled = true;
                    saveButton.IsEnabled = true;

                    //Set read only to false so user can edit
                    descTextBox.IsReadOnly = false;
                    costTextBox.IsReadOnly = false;

                    //Set a variable for currently selected item
                    DataRowView selected = (DataRowView)itemDataGrid.SelectedItem;

                    logic.UpdateSelectedText(selected, descTextBox, costTextBox);
                }
                else
                {
                    descTextBox.Text = "";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Upon clicking add item, pull text from text boxes and add a new row to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Clear current description and cost
                descTextBox.Clear();
                costTextBox.Clear();

                itemDataGrid.SelectedIndex = -1;

                //Remove read only from text boxes
                descTextBox.IsReadOnly = false;
                costTextBox.IsReadOnly = false;

                //Disable item grid
                itemDataGrid.IsEnabled = false;

                //set isEnabled on buttons
                saveButton.IsEnabled = true;
                deleteItemButton.IsEnabled = false;
                addItemButton.IsEnabled = false;

                //set static variable to true
                itemListModified = true;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// When user clicks delete item, remove the current selected row from the table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //ensure a valid line item is selected
                if (itemDataGrid.SelectedItem != null)
                {
                    //get index from selected item
                    int index = itemDataGrid.SelectedIndex;

                    //create list for potential invoice conflicts
                    List<int> invoices = new List<int>();
                    invoices = logic.GetInvoices(index, invoices);

                    //if the invoices list after database retrieval has data, show associated invoice conflicts
                    if (invoices != null && invoices.Count > 0)
                    {
                        string invoiceErrorMessage = "This item is on the following invoices:\n";

                        foreach (var invoiceNum in invoices)
                        {
                            invoiceErrorMessage += $"{invoiceNum}\n";
                        }

                        MessageBox.Show(invoiceErrorMessage, "Associated Invoices", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    //otherwise, delete item as usual
                    else
                    {
                        logic.DeleteItem(index);

                        descTextBox.Clear();
                        costTextBox.Clear();
                        deleteItemButton.IsEnabled = false;
                        saveButton.IsEnabled = false;

                        itemListModified = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// When save button is clicked during an add or edit event, pull text from
        /// text boxes and save accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Set strings to use in logic method
                string newDesc = descTextBox.Text;
                string newCost = costTextBox.Text;

                //Re-enable item grid
                itemDataGrid.IsEnabled = true;

                logic.AddEditData(newDesc, newCost, itemDataGrid);

                //enable add item button (since it was disabled after pushing it once)
                addItemButton.IsEnabled = true;

                if (itemDataGrid.SelectedIndex == -1)
                {
                    descTextBox.IsReadOnly = true;
                    costTextBox.IsReadOnly = true;
                    saveButton.IsEnabled = false;
                }

                itemListModified = true;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }
    }
}
