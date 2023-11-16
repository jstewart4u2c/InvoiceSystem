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
        public bool ItemModified;
        #endregion

        /// <summary>
        /// Creates new database object 
        /// </summary>
        clsDataAccess db;

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="Exception"></exception>
        public wndItems()
        {
            try
            {
                InitializeComponent();

                //Initialize Database
                db = new clsDataAccess();

                DataSet ds;

                int ReturnValues = 0;
                
                //Execute SQL to select all from db
                ds = db.ExecuteSQLStatement("SELECT * FROM ItemDesc", ref ReturnValues);

                //loop to grab all data and output to itemList
                for(int i=0; i < ds.Tables[0].Rows.Count; i++)
                {
                    itemList.Items.Add(ds.Tables[0].Rows[i][1].ToString() + " " + ds.Tables[0].Rows[i].ItemArray[2].ToString());
                }

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Item Object containing variables for item info
        /// </summary>
        public class Item
        {
            //Private Variables
            private int code;
            private double cost;
            private string description;
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
    }
}
