using InvoiceSystem.Common;
using InvoiceSystem.Items;
using InvoiceSystem.Search;
using System;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace InvoiceSystem.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        public bool MenuEnabled = false;
        clsMainSQL sqlQuery = new clsMainSQL();
        clsMainLogic logic = new clsMainLogic();
        clsDataAccess db = new clsDataAccess();
        DataSet ds;
        int ItemCount;
        bool IsNew;
        public wndMain()
        {
            InitializeComponent();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

/****NAV METHODS***/
          /// <summary>
          /// Navigating To Search Window
          /// </summary>
          /// <param name="sender"></param>
          /// <param name="e"></param>
        private async void NavToSearchWnd(object sender, RoutedEventArgs e)
        {
            try
            {
                /*Check if User is Creating or Editing*/
                if (!MenuEnabled)
                {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Navigating To Item Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void NavToItemWnd(object sender, RoutedEventArgs e)
        {
            try
            {
                /*Check if User is Creating or Editing*/
                /*Close Main Window*/
                if (!MenuEnabled)
                {
                    this.Hide();
                    wndItems items = new wndItems();
                    items.ShowDialog();
                    this.Show();
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
        }

/*****Create OR Update Data******/
        /// <summary>
        /// Creating A new Invoice And Enabling The Bottom Half Of the Screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateInvoiceClick(object sender, RoutedEventArgs e)
        {
            try
            {
                IsNew = true;
                /*Clear Item Drop Box so It doesnt just keep filling it up*/
                SelectItemDropBox.Items.Clear();
                ItemCount = 0;
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
                string InvoiceNumber = logic.AddInvoice();
                /*Check if Invoice Number was Gathered Correctly*/
                if (InvoiceNumber == null)
                {
                    ErrorTextBox.Visibility = Visibility.Visible;
                    ErrorTextBox.Text = "Sorry, Something Went Wrong, Try Again";
                    ChangeEnableStatus();
                }
                else
                {
                    /*Set labels*/
                    InvoiceNumberLabel.Content = InvoiceNumber;
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

        private void AddItem(object sender, RoutedEventArgs e)
        {
            try
            {
                //Make Sure Its Not Empty 
                if(SelectItemDropBox.Text == "" || SelectItemDropBox.Text == null)
                {
                    ErrorTextBox.Text = "Please Make Sure To Select A Item";
                }
                else { 
                /*Adding Item to Datagrid*/
                string ItemDesc;
                string ItemPrice;

                ItemDesc = SelectItemDropBox.Text;
                string PriceQuery = sqlQuery.GrabItemPrice(SelectItemDropBox.Text);
                ItemPrice = db.ExecuteScalarSQL(PriceQuery);
                ItemCount++;
                string InvoiceNumber = InvoiceNumberLabel.Content.ToString();

                logic.AddToLineItemsTable(ItemDesc, InvoiceNumber, ItemCount, ItemPrice);
                logic.UpdateDataGrid(AddedItemsDataGrid);

                AddedItemsDataGrid.Columns[0].Header = "Item Number";
                AddedItemsDataGrid.Columns[1].Header = "Item Description";
                AddedItemsDataGrid.Columns[2].Header = "Cost";

                TotalCostTextBox.Text = "$" + logic.TotalPrice.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /******SAVE AND DELETE******/
        /// <summary>
        /// Saving The Invoice, Disable Everything Again
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveInvoiceClick(object sender, RoutedEventArgs e)
        {
            try
            {
                /*Disable Menu Until User Decides to Update*/
                ChangeEnableStatus();
                /*Allow User to Press Update*/
                UpdateInvoiceButton.Visibility = Visibility.Visible;
                CancelInvoiceButton.Visibility = Visibility.Hidden;

                logic.UpdateTotalPrice(InvoiceNumberLabel.Content.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        private void DeleteItemClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //Check That User Has A Item Selected 
                if (AddedItemsDataGrid.SelectedItem != null)
                {
                    int index = AddedItemsDataGrid.SelectedIndex;
                    //Delete Item
                    logic.DeleteItemOffList(index);
                    logic.UpdateDataGrid(AddedItemsDataGrid);
                }
                else
                {
                    ErrorTextBox.Text = "No Item Was Selected";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        private void CancelCurrentButton(object sender, RoutedEventArgs e)
        {
            try
            {
                ChangeEnableStatus();
                CancelInvoiceButton.Visibility = Visibility.Hidden;

                ErrorTextBox.Text = "Canceled A New Set";
                logic.DeleteAll();
                AddedItemsDataGrid.DataContext = null;
                logic.UpdateDataGrid(AddedItemsDataGrid);

                AddedItemsDataGrid.Columns[0].Header = "Item Number";
                AddedItemsDataGrid.Columns[1].Header = "Item Description";
                AddedItemsDataGrid.Columns[2].Header = "Cost";

                TotalCostTextBox.Text = "$" + logic.TotalPrice.ToString();

                InvoiceNumberLabel.Content = "";
                DateLabel.Content = "";

                SelectItemDropBox.Items.Clear();
                CostTextBox.Text = "$0";


            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

/****EXTRA METHODS*****/
        public void ChangeEnableStatus()
        {
            try
            {
                MenuEnabled = !MenuEnabled;
                CreateOrEditContentGrid.IsEnabled = !CreateOrEditContentGrid.IsEnabled;
                AddedItemsDataGrid.IsEnabled = !AddedItemsDataGrid.IsEnabled;
                SaveInvoiceButton.IsEnabled = !SaveInvoiceButton.IsEnabled;
                DeleteItemButton.IsEnabled = !DeleteItemButton.IsEnabled;
                TotalCostTextBox.IsEnabled = !TotalCostTextBox.IsEnabled;
                BeginButton.IsEnabled = !BeginButton.IsEnabled;
                UpdateInvoiceButton.IsEnabled = !UpdateInvoiceButton.IsEnabled;
            }
            catch (Exception ex)
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
                //Put in SQL FILE
                PriceQuery = sqlQuery.GrabItemPrice(SelectItemDropBox.Text);
                CurrentPrice = db.ExecuteScalarSQL(PriceQuery);
                CostTextBox.Text = "$" + CurrentPrice;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        
        public void UpdateWindow(string InvoiceNum)
        {
            logic.UpdateCurrentInvoice(InvoiceNum);
            logic.UpdateDataGrid(AddedItemsDataGrid);
        }
        
    }
}
