using InvoiceSystem.Main;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
        /// Creates new database object 
        /// </summary>
        clsDataAccess db;
        clsItemsLogic logic = new clsItemsLogic();

        /// <summary>
        /// 
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
                if(itemDataGrid.SelectedItem != null)
                {
                    int index = itemDataGrid.SelectedIndex;

                    logic.DeleteItem(index);

                    descTextBox.Clear();
                    costTextBox.Clear();
                    deleteItemButton.IsEnabled = false;
                    saveButton.IsEnabled = false;

                    itemListModified = true;
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

                addItemButton.IsEnabled = true;

                if(itemDataGrid.SelectedIndex == -1)
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
