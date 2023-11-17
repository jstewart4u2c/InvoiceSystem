using InvoiceSystem.Items;
using InvoiceSystem.Search;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
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
        clsMainLogic logic = new clsMainLogic();
        DataSet ds;
        bool IsDeleting = false;
        int ItemCount;
        public wndMain()
        {
            InitializeComponent();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        /*Navigate to Search Window*/
        private async void NavToSearchWnd(object sender, RoutedEventArgs e)
        {
            try { 
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
            }catch(Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }//End Nav Search Button

        private async void NavToItemWnd(object sender, RoutedEventArgs e)
        {
            try { 
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }//End Nav Item Button

        /*Create A Invoice and Add to Invoice Table*/
        private void CreateInvoiceClick(object sender, RoutedEventArgs e)
        {
            try { 
            /*Clear Item Drop Box so It doesnt just keep filling it up*/
                SelectItemDropBox.Items.Clear();
                ContentGridLabel.Content = "Create a New Invoice";
                CostTextBox.Text = "$0";
                /*Enable All Items*/
                ChangeEnableStatus();
                /*Show Cancel Button*/
                CancelInvoiceButton.Visibility = Visibility.Visible;
            
                /*Load DropDown Menu*/
                int iRet = 0;

                //Grab All Items
                ds = db.ExecuteSQLStatement("SELECT * FROM ItemDesc", ref iRet);

                //Loop and add them to Drop Down
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    SelectItemDropBox.Items.Add(ds.Tables[0].Rows[i][1].ToString());
                }
                /*Add a New Invoice Number to Invoice Table, Add Date*/
                    int InvoiceNumber = logic.AddInvoice();
                /*Check if Invoice Number was Gathered Correctly*/
                if(InvoiceNumber == 0)
                    {
                        ErrorTextBox.Visibility= Visibility.Visible;
                        ErrorTextBox.Text = "Sorry, Something Went Wrong, Try Again";
                        ChangeEnableStatus();
                    }
                else {
                    /*Set labels*/
                    InvoiceNumberLabel.Content = InvoiceNumber.ToString();
                    var TodaysDate = DateOnly.FromDateTime(DateTime.Now);
                    DateLabel.Content = TodaysDate;
            }
            /*TODO::When Clicked Again, Wipe Fields*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }//End EnableMenu Button
        /*Update Current Invoice*/
        private void UpdateInvoiceClick(object sender, RoutedEventArgs e)
        {
            try
            {
                /*Enable All Items*/
                ChangeEnableStatus();
                CancelInvoiceButton.Visibility = Visibility.Visible;
                ContentGridLabel.Content = "Update Invoice";

                /*Set MenuEnabled so User cannot change windows*/
                MenuEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        private void SaveInvoiceClick(object sender, RoutedEventArgs e)
        {
            try { 
                /*Disable Menu Until User Decides to Update*/
                ChangeEnableStatus();
                /*Allow User to Press Update*/
                UpdateInvoiceButton.Visibility = Visibility.Visible;
                CancelInvoiceButton.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        

        private void CancelCurrentButton(object sender, RoutedEventArgs e)
        {
            try { 
                ChangeEnableStatus();
                CancelInvoiceButton.Visibility = Visibility.Hidden;
            }catch(Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /*Returns The Opposite Bool Value so We can Enable and Disable Menu*/
        public void ChangeEnableStatus()
        {
            try { 
                MenuEnabled = !MenuEnabled;
                CreateOrEditContentGrid.IsEnabled = !CreateOrEditContentGrid.IsEnabled;
                AddedItemsDataGrid.IsEnabled = !AddedItemsDataGrid.IsEnabled;
                SaveInvoiceButton.IsEnabled = !SaveInvoiceButton.IsEnabled;
                DeleteItemButton.IsEnabled = !DeleteItemButton.IsEnabled;
                TotalCostTextBox.IsEnabled = !TotalCostTextBox.IsEnabled;
                BeginButton.IsEnabled = !BeginButton.IsEnabled;
                UpdateInvoiceButton.IsEnabled = !UpdateInvoiceButton.IsEnabled;
            }catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /*See What Item is Selected and Update Price*/
        private void ItemSelected(object sender, EventArgs e)
        {
            try
            {
                /*Select Price Where ItemDesc = DropBox*/
                string PriceQuery;
                string CurrentPrice;
                PriceQuery = "SELECT Cost FROM ItemDesc WHERE ItemDesc = \"" + SelectItemDropBox.Text + "\"";

                CurrentPrice = db.ExecuteScalarSQL(PriceQuery);
                CostTextBox.Text = "$" + CurrentPrice;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            try
            {
                /*Adding Item to Datagrid*/
                string ItemDesc;
                string ItemPrice;

                ItemDesc = SelectItemDropBox.Text;
                ItemPrice = CostTextBox.Text;
                ItemCount++;
                string InvoiceNumber = InvoiceNumberLabel.Content.ToString();

                logic.AddToLineItemsTable(ItemDesc, InvoiceNumber, ItemCount);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /*TODO
         * When Add Item is Clicked, Add to DataGrid
         * 
         * When Item is Added Check
         * string CurrentItem = SelectItemDropBox.Text;
                if (CurrentItem == null)
                {
                    ErrorTextBox.Visibility = Visibility.Visible;
                    ErrorTextBox.Text = "Please Select A Item";
                }
                else
                {

                }
         */

    }
}
