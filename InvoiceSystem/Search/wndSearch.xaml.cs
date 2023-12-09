using InvoiceSystem.Common;
using InvoiceSystem.Items;
using InvoiceSystem.Main;
using System;
using System.Collections.Generic;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InvoiceSystem.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        clsSearchLogic SearchLogic = new clsSearchLogic();
        bool bCost = false;
        bool bNum = false;
        bool bDate = false;
        bool bClear = false;
        bool SelectedID = false;
        List<string> lsNum = new List<string>();
        List<string> lsDate = new List<string>();
        List<string> lsTemp = new List<string>();
        /// <summary>
        /// Initialize window objects
        /// </summary>
        public wndSearch()
        {
            InitializeComponent();
            SearchLogic = new clsSearchLogic();
            SearchInvoiceNumber.ItemsSource = SearchLogic.GetDistinctInvoiceNumbers();
            SearchInvoiceDate.ItemsSource = SearchLogic.GetDistinctInvoiceDates();
            SearchTotalCosts.ItemsSource = SearchLogic.GetDistinctInvoiceCosts();
            SearchInvoice.ItemsSource = SearchLogic.GetInvoices();
        }

        /// <summary>
        /// Go to main Window
        /// </summary>
        private void NavMainMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
                Main.wndMain main = new Main.wndMain();
                main.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Go to update window
        /// </summary>
        private void NavUpdateItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
                wndItems items = new wndItems();
                items.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        ///  set bool to true
        /// </summary>
        private void SearchInvoiceNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                bNum = true;
                LoadData();


            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// set bool to true
        /// </summary>
        private void SearchInvoiceDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                bDate = true;
                LoadData();


            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        ///  set bool to true
        /// </summary>
        private void SearchTotalCosts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                bCost = true;

                LoadData();


            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Close then open main and send invoice ID
        /// </summary>
        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchInvoice.SelectedItem != null)
            {
                var selectedItem = (clsInvoices)SearchInvoice.SelectedItem;

                string sNumID = selectedItem.sInvoiceNum;

                clsInvoicesPass.sSelectedInvoiceNum = sNumID;
                clsInvoicesPass.IsUpdating = true;
                
                this.Hide();
                
            }
        }

        /// <summary>
        /// close then open main and send invoice ID
        /// </summary>
        private void LoadData()
        {
            //See if clear is false
            if (!bClear)
            {
                //Set grid data
                if (bNum)
                {
                    SearchInvoice.ItemsSource = SearchLogic.FilterInvoiceNumbers(SearchInvoiceNumber.SelectedItem.ToString());
                    if (bCost)
                    {
                        SearchInvoice.ItemsSource = SearchLogic.FilterInvoiceNumbersCosts(SearchInvoiceNumber.SelectedItem.ToString(), SearchTotalCosts.SelectedItem.ToString());
                        if (bDate)
                        {
                            SearchInvoice.ItemsSource = SearchLogic.FilterInvoiceNumbersCostDates(SearchInvoiceNumber.SelectedItem.ToString(), SearchInvoiceDate.SelectedItem.ToString(), SearchTotalCosts.SelectedItem.ToString());
                        }
                    }
                    else if (bDate)
                    {
                        SearchInvoice.ItemsSource = SearchLogic.FilterInvoiceNumberDates(SearchInvoiceNumber.SelectedItem.ToString(), SearchInvoiceDate.SelectedItem.ToString());

                    }
                }
                else if (bCost)
                {
                    SearchInvoice.ItemsSource = SearchLogic.FilterInvoiceCosts(SearchTotalCosts.SelectedItem.ToString());
                    if (bDate)
                    {
                        SearchInvoice.ItemsSource = SearchLogic.FilterInvoiceCostDates(SearchTotalCosts.SelectedItem.ToString(), SearchInvoiceDate.SelectedItem.ToString());
                    }
                }
                else if (bDate)
                {
                    SearchInvoice.ItemsSource = SearchLogic.FilterInvoiceDates(SearchInvoiceDate.SelectedItem.ToString());
                }
                //Set combo boxes using string arrays
                var invoiceNumbers = new List<string>();
                var invoiceDates = new List<string>();
                var totalCosts = new List<string>();

                foreach (var item in SearchInvoice.ItemsSource)
                {
                    var invoice = item as clsInvoices;
                    if (invoice != null)
                    {
                        invoiceNumbers.Add(invoice.sInvoiceNum);
                        invoiceDates.Add(invoice.sInvoiceDate);
                        totalCosts.Add(invoice.sTotalCost);
                    }
                }

                SearchInvoiceNumber.ItemsSource = invoiceNumbers;
                SearchInvoiceDate.ItemsSource = invoiceDates;
                SearchTotalCosts.ItemsSource = totalCosts;
            }
        }

        private void btClearGrid_Click(object sender, RoutedEventArgs e)
        {
            // Reset the gid and combo boxes.
            bClear = true;
            SearchLogic = new clsSearchLogic();
            SearchInvoiceNumber.SelectedItem = null;
            SearchInvoiceDate.SelectedItem = null;
            SearchTotalCosts.SelectedItem = null;
            SearchInvoice.ItemsSource = SearchLogic.GetInvoices();
            SearchInvoiceNumber.ItemsSource = SearchLogic.GetDistinctInvoiceNumbers();
            SearchInvoiceDate.ItemsSource = SearchLogic.GetDistinctInvoiceDates();
            SearchTotalCosts.ItemsSource = SearchLogic.GetDistinctInvoiceCosts();
            bCost = false;
            bNum = false;
            bDate = false;
            bClear = false;
        }
    }
}
