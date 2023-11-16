using InvoiceSystem.Items;
using InvoiceSystem.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InvoiceSystem.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        public bool MenuEnabled = false;
        clsMainSQL db = new clsMainSQL();
        public wndMain()
        {
            InitializeComponent();
        }

        /*Navigate to Search Window*/
        private async void NavToSearchWnd(object sender, RoutedEventArgs e)
        {
           /*Check if User is Creating or Editing*/
           if(!MenuEnabled) {
                /*Close Main Window*/
           this.Hide();
           wndSearch search = new wndSearch();
           search.ShowDialog();
           this.Show();

                /*When Search Window is Closed Gather Current ID Selected*/
            }//End IF
            else
            {
                ErrorTextBox.Visibility = Visibility.Visible;
                ErrorTextBox.Text = "Please Make Sure to Save or Cancel Your Current Invoice";
                
                await Task.Delay(5000);
                ErrorTextBox.Visibility = Visibility.Hidden;
            }//End Else
        }//End Nav Search Button

        private async void NavToItemWnd(object sender, RoutedEventArgs e)
        {
            /*Check if User is Creating or Editing*/
            /*Close Main Window*/
            if (!MenuEnabled) {
                this.Hide();
                wndItems items = new wndItems();
                items.ShowDialog();
                this.Show();
            /*When Item Window is Closed, Update Tables*/
            }
            else
            {
                ErrorTextBox.Visibility = Visibility.Visible;
                ErrorTextBox.Text = "Please Make Sure to Save or Cancel Your Current Invoice";

                await Task.Delay(5000);
                ErrorTextBox.Visibility = Visibility.Hidden;     
            }//End Else
        }//End Nav Item Button

        private void CreateInvoiceClick(object sender, RoutedEventArgs e)
        {
            /*Enable All Items*/
            ChangeEnableStatus();
            /*Show Cancel Button*/
            CancelInvoiceButton.Visibility = Visibility.Visible;

            /*Load DropDown Menu*/
            DataSet ds;

            int iRet = 0;

            //Grab All Items
            ds = db.ExecuteSQLStatement("SELECT * FROM ItemDesc", ref iRet);

            //Loop and add them to Drop Down
            for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                SelectItemDropBox.Items.Add(ds.Tables[0].Rows[i][1].ToString());
            }

            /*When Clicked Again, Wipe Fields*/

        }//End EnableMenu Button

        private void SaveInvoiceClick(object sender, RoutedEventArgs e)
        {
            /*Disable Menu Until User Decides to Update*/
            ChangeEnableStatus();
            /*Allow User to Press Update*/
            UpdateInvoiceButton.Visibility = Visibility.Visible;
            CancelInvoiceButton.Visibility = Visibility.Hidden;
        }

        private void UpdateInvoiceClick(object sender, RoutedEventArgs e)
        {
            /*Enable All Items*/
            ChangeEnableStatus();
            CancelInvoiceButton.Visibility = Visibility.Visible;
            
            /*Set MenuEnabled so User cannot change windows*/
            MenuEnabled = true;
        }

        private void CancelCurrentButton(object sender, RoutedEventArgs e)
        {
            ChangeEnableStatus();
            //UpdateInvoiceButton.Visibility = Visibility.Visible;
            CancelInvoiceButton.Visibility = Visibility.Hidden;

        }

        /*Returns The Opposite Bool Value so We can Enable and Disable Menu*/
        public void ChangeEnableStatus()
        {
            MenuEnabled = !MenuEnabled;
            CreateOrEditContentGrid.IsEnabled = !CreateOrEditContentGrid.IsEnabled;
            AddedItemsDataGrid.IsEnabled = !AddedItemsDataGrid.IsEnabled;
            SaveInvoiceButton.IsEnabled = !SaveInvoiceButton.IsEnabled;
            DeleteItemButton.IsEnabled = !DeleteItemButton.IsEnabled;
            TotalCostTextBox.IsEnabled = !TotalCostTextBox.IsEnabled;
        }

    }
}
