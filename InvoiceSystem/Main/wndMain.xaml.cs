using InvoiceSystem.Items;
using InvoiceSystem.Search;
using System;
using System.Collections.Generic;
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
           wndSearch search = new wndSearch();
           search.Show();
           Visibility = Visibility.Hidden;
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
            wndItems items = new wndItems();
            items.Show();
            Visibility = Visibility.Hidden;
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

        private void EnableMenuClick(object sender, RoutedEventArgs e)
        {
            /*Enable All Items*/
            CreateOrEditContentGrid.IsEnabled = true;
            AddedItemsDataGrid.IsEnabled = true;
            SaveInvoiceButton.IsEnabled = true;
            DeleteItemButton.IsEnabled = true;
            TotalCostTextBox.IsEnabled = true;
            CancelInvoiceButton.Visibility = Visibility.Visible;
            /*Set MenuEnabled so User cannot change windows*/
            MenuEnabled = true;

            /*When Clicked Again, Wipe Fields*/

        }//End EnableMenu Button

        private void SaveInvoiceClick(object sender, RoutedEventArgs e)
        {
            /*Disable Menu Until User Decides to Update*/
            MenuEnabled = false;
            CreateOrEditContentGrid.IsEnabled = false;
            AddedItemsDataGrid.IsEnabled = false;
            SaveInvoiceButton.IsEnabled = false;
            DeleteItemButton.IsEnabled = false;
            TotalCostTextBox.IsEnabled = false;
            /*Allow User to Press Update*/
            UpdateInvoiceButton.Visibility = Visibility.Visible;
            CancelInvoiceButton.Visibility = Visibility.Hidden;
        }

        private void UpdateInvoiceClick(object sender, RoutedEventArgs e)
        {
            /*Enable All Items*/
            CreateOrEditContentGrid.IsEnabled = true;
            AddedItemsDataGrid.IsEnabled = true;
            SaveInvoiceButton.IsEnabled = true;
            DeleteItemButton.IsEnabled = true;
            TotalCostTextBox.IsEnabled = true;
            CancelInvoiceButton.Visibility = Visibility.Visible;
            
            /*Set MenuEnabled so User cannot change windows*/
            MenuEnabled = true;
        }

        private void CancelCurrentButton(object sender, RoutedEventArgs e)
        {
            MenuEnabled = false;
            CreateOrEditContentGrid.IsEnabled = false;
            AddedItemsDataGrid.IsEnabled = false;
            SaveInvoiceButton.IsEnabled = false;
            DeleteItemButton.IsEnabled = false;
            TotalCostTextBox.IsEnabled = false;
            //UpdateInvoiceButton.Visibility = Visibility.Visible;
            CancelInvoiceButton.Visibility = Visibility.Hidden;

        }
    }
}
