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
        bool ResetAll = true;
        public wndMain()
        {
            InitializeComponent();
            UserDatePicker.SelectedDate = DateTime.Now;
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

        }

        /// <summary>
        /// Check If A Invoice is In Progress, Delete if User Wishes to Cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelInvoice(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (MenuEnabled == true)
                {
                    MessageBoxResult result =
                    MessageBox.Show
                        (
                            "Invoice in Progress, Do you still wish to quit?",
                            "Unsaved",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Warning
                        );
                    if (result == MessageBoxResult.No)
                    { e.Cancel = true; }
                    else
                    {
                        if (clsInvoicesPass.IsUpdating)
                        { logic.ResetTable();
                          db.ExecuteNonQuery(sqlQuery.DeleteCopy());
                        }
                        else {logic.DeleteAll();  }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
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
                    ResetWindow();
                    wndSearch search = new wndSearch();
                    search.ShowDialog();
                    this.Show();
                    if (clsInvoicesPass.IsUpdating)
                    {
                        UpdateWindow();
                    }

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
                    ResetWindow();
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
        /// OR Updating 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateInvoiceClick(object sender, RoutedEventArgs e)
        {

            try
            {
                BeginButton.Content = "Create Invoice";
                /*Enable All Items*/
                ChangeEnableStatus();
                /*Clear Item Drop Box so It doesnt just keep filling it up*/
                SelectItemDropBox.Items.Clear();
                /*Load DropDown Menu*/
                int iRet = 0;

                //Grab All Items
                ds = db.ExecuteSQLStatement(sqlQuery.GrabAllFromItemDesc(), ref iRet);

                //Loop and add them to Drop Down
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    SelectItemDropBox.Items.Add(ds.Tables[0].Rows[i][1].ToString());
                } 
                /*Show Cancel Button*/
                CancelInvoiceButton.Visibility = Visibility.Visible;
                if (clsInvoicesPass.IsUpdating)
                {
                    SaveInvoiceButton.IsEnabled = true;
                    SetDataGridHeaders();
                    TotalCostTextBox.IsEnabled = true;
                    //Make A Copy Of The Table 
                    db.ExecuteNonQuery(sqlQuery.CopyOver());

                    //Grab Item Count 
                    ItemCount = Convert.ToInt32(db.ExecuteScalarSQL(sqlQuery.GrabCountForCurrentInvoice()));
                }
                else {

                ResetWindow();
                ItemCount = 0;
                ContentGridLabel.Content = "Create a New Invoice";
                CostTextBox.Text = "$0";
                
                
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
                }
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }//End EnableMenu Button

        /// <summary>
        /// Updating The Current Invoice Loaded in 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateInvoiceClick(object sender, RoutedEventArgs e)
        {
            try
            {
                clsInvoicesPass.IsUpdating = true;
                /*Enable All Items*/
                ChangeEnableStatus();
                CancelInvoiceButton.Visibility = Visibility.Visible;
                ContentGridLabel.Content = "Update Invoice";
                db.ExecuteNonQuery(sqlQuery.CopyOver());

                /*Set MenuEnabled so User cannot change windows*/
                MenuEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Adding A Item to The DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddItem(object sender, RoutedEventArgs e)
        {
            ErrorTextBox.Visibility= Visibility.Hidden;
            try
            {
                //Make Sure Its Not Empty 
                if(SelectItemDropBox.Text == "" || SelectItemDropBox.Text == null)
                {
                    ErrorTextBox.Visibility = Visibility.Visible;
                    ErrorTextBox.Text = "Please Make Sure To Select A Item";
                }
                else {
                SaveInvoiceButton.IsEnabled = true;
                /*Adding Item to Datagrid*/
                string ItemDesc;
                string ItemPrice;

                ItemDesc = SelectItemDropBox.Text;
                string PriceQuery = sqlQuery.GrabItemPrice(SelectItemDropBox.Text);
                ItemPrice = db.ExecuteScalarSQL(PriceQuery);
                ItemCount++;
                string InvoiceNumber = InvoiceNumberLabel.Content.ToString();

                logic.AddToLineItemsTable(ItemDesc, InvoiceNumber, ItemCount);
                logic.UpdateDataGrid(AddedItemsDataGrid);
                SetDataGridHeaders();

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
                ErrorTextBox.Visibility = Visibility.Hidden;
                if (AddedItemsDataGrid.Items.Count == 0)
                {
                    ErrorTextBox.Visibility = Visibility.Visible;
                    ErrorTextBox.Text = "Please Make Sure To Add Items Or Cancel";
                }
                else
                {
                        /*Disable Menu Until User Decides to Update*/
                        ChangeEnableStatus();
                        /*Allow User to Press Update*/
                        UpdateInvoiceButton.Visibility = Visibility.Visible;
                        CancelInvoiceButton.Visibility = Visibility.Hidden;

                        logic.UpdateDate(UserDatePicker.SelectedDate.Value.Date);
                        logic.UpdateTotalPrice(InvoiceNumberLabel.Content.ToString());

                        if (clsInvoicesPass.IsUpdating)
                        {
                            db.ExecuteNonQuery(sqlQuery.DeleteCopy());
                            clsInvoicesPass.IsUpdating = false;
                        }

                    else
                    {
                        ErrorTextBox.Visibility= Visibility.Visible;
                        ErrorTextBox.Text = "A Date Must Be Selected";
                    } 
                }
                UpdateInvoiceButton.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Deleting A Item off The DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteItemClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ErrorTextBox.Visibility = Visibility.Hidden;
                //Check That User Has A Item Selected 
                if (AddedItemsDataGrid.SelectedItem != null)
                {
                    int index = AddedItemsDataGrid.SelectedIndex;
                    //Delete Item
                    logic.DeleteItemOffList(index);
                    logic.UpdateDataGrid(AddedItemsDataGrid);
                    SetDataGridHeaders();
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

        /// <summary>
        /// Canceling The Current Invoice
        /// For New it Deletes The Invoice All Together 
        /// For Updated, It Just Restores Previous Table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelCurrentButton(object sender, RoutedEventArgs e)
        {
            try
            {
                ErrorTextBox.Visibility = Visibility.Hidden;
                ChangeEnableStatus();
                if (clsInvoicesPass.IsUpdating)
                {
                    logic.ResetTable();
                    db.ExecuteNonQuery(sqlQuery.DeleteCopy());
                }
                else
                {
                    logic.DeleteAll();
                }
                //Hide Buttons
                CancelInvoiceButton.Visibility = Visibility.Hidden;
                UpdateInvoiceButton.Visibility = Visibility.Hidden;
                ResetWindow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

/****EXTRA METHODS*****/
        /// <summary>
        ///Changing The Status to Either Enabled or Disable Stuff
        /// </summary>
        public void ChangeEnableStatus()
        {
            try
            {
                MenuEnabled = !MenuEnabled;
                CreateOrEditContentGrid.IsEnabled = !CreateOrEditContentGrid.IsEnabled;
                TotalCostTextBox.IsEnabled = !TotalCostTextBox.IsEnabled;
                DeleteItemButton.IsEnabled = !DeleteItemButton.IsEnabled;
                BeginButton.IsEnabled = !BeginButton.IsEnabled;
                UpdateInvoiceButton.IsEnabled = !UpdateInvoiceButton.IsEnabled;
                UserDatePicker.IsEnabled = !UserDatePicker.IsEnabled;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// When A Item From the Drop Box is Selected 
        /// Set The Current Price 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Gathers The Data From The Given Invoice Number
        /// </summary>
        public void UpdateWindow()
        {
            try {
                int IRef = 0;
                string SelectedInvoice = clsInvoicesPass.sSelectedInvoiceNum.ToString();
                UserDatePicker.Text = db.ExecuteScalarSQL(sqlQuery.GrabDateForCurrentInvoice());
                logic.UpdateCurrentInvoice(SelectedInvoice);
                BeginButton.Content = "Update";
                logic.UpdateDataGrid(AddedItemsDataGrid);
                InvoiceNumberLabel.Content = clsInvoicesPass.sSelectedInvoiceNum.ToString();
                TotalCostTextBox.IsEnabled = true;
                SetDataGridHeaders();
                TotalCostTextBox.Text = "$" + logic.TotalPrice.ToString();
                clsInvoicesPass.OldTotal = logic.TotalPrice;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Makes Menu Neutral Again
        /// </summary>
       public void ResetWindow()
        {
            try
            {
                //Certain Values Dont Need to be Reset when Updates Happen
                if(ResetAll)
                {
                    clsInvoicesPass.IsUpdating = false;
                    InvoiceNumberLabel.Content = "";
                    AddedItemsDataGrid.ItemsSource = null;
                }
                SaveInvoiceButton.IsEnabled = false;
                logic.TotalPrice = 0;
                TotalCostTextBox.Text = "$" + logic.TotalPrice.ToString();
                UserDatePicker.DisplayDate = DateTime.Now;
                if (InvoiceNumberLabel.Content.ToString() != "") 
                    {
                        //Refresh The Data Grid
                        AddedItemsDataGrid.ItemsSource = null;
                        //Set All Boxes To Null
                        SelectItemDropBox.Items.Clear();
                        CostTextBox.Text = "$0";
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// Set Headers For dataGrid so they Arent Default Values 
        /// </summary>
        public void SetDataGridHeaders()
        {
            try
            {
            AddedItemsDataGrid.Columns[0].Header = "Item Number";
            AddedItemsDataGrid.Columns[1].Header = "Item Description";
            AddedItemsDataGrid.Columns[2].Header = "Cost";
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

    }
}
