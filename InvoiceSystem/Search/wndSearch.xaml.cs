using InvoiceSystem.Items;
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

        /// <summary>
        /// Initialize window objects
        /// </summary>
        public wndSearch()
        {
            InitializeComponent();
            clsSearchLogic SearchLogic = new clsSearchLogic();
            SearchInvoiceNumber.ItemsSource = SearchLogic.GetDistinctInvoiceNumbers();
            SearchInvoiceDate.ItemsSource = SearchLogic.GetDistinctInvoiceDates();
            SearchTotalCosts.ItemsSource = SearchLogic.GetDistinctInvoiceCosts();
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
        /// find the invoice number in database and updates grid
        /// </summary>
        private void SearchInvoiceNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //call sql methods to update grid.
        }

        /// <summary>
        /// find the date in database and updates grid
        /// </summary>
        private void SearchInvoiceDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //call sql methods to update grid.

        }

        /// <summary>
        /// find the total cost in database and updates grid
        /// </summary>
        private void SearchTotalCosts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //call sql methods to update grid.
        }

        /// <summary>
        /// close then open main and send invoice ID
        /// </summary>
        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            //**** This will pass the selected Invoice ID to the main method.******
            this.Hide();
            Main.wndMain main = new Main.wndMain();
            main.ShowDialog();
        }
    }
}
