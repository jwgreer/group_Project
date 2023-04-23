using GroupProject.Items;
using GroupProject.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

namespace GroupProject.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        //test
       wndSearch1 seachWindow = new wndSearch1();
       // wndSearch seachWindow = new wndSearch();
       wndSearch1 searchWindow = new wndSearch1();
        /// <summary>
        /// object for the items window
        /// </summary>
        wndItems itemsWindow = new wndItems();

        /// <summary>
        /// object for the sql class
        /// </summary>
        clsMainSQL sqlClass = new clsMainSQL();

        /// <summary>
        /// object for the main class
        /// </summary>
        clsMainLogic mainClass = new clsMainLogic();

        /// <summary>
        /// invoice
        /// </summary>
        public DataSet invoice;

        /// <summary>
        /// creates the datatable
        /// </summary>
        public DataTable datatable = new DataTable();

        /// <summary>
        /// variable for invoice date
        /// </summary>
        public string varForInvoiceDate = "";

        /// <summary>
        /// variable to hold item name
        /// </summary>
        public string itemName = "";

        /// <summary>
        /// variable to hold item cost
        /// </summary>
        public string itemCost = "";

        /// <summary>
        /// variable to hold item code
        /// </summary>
        public string itemCode = "";

        /// <summary>
        /// variable for invoice num
        /// </summary>
        public int invoiceNum;

        /// <summary>
        /// varialbe for line item num
        /// </summary>
        public int lineItem;

        /// <summary>
        /// variable that holds total cost
        /// </summary>
        public int totalInvoiceCost;

        /// <summary>
        /// variable for invoice mode
        /// </summary>
        bool invoiceMode;

        /// <summary>
        /// variable for item code
        /// </summary>
        public string selectedItemCode = "";

        /// <summary>
        /// variable for testing
        /// </summary>
        public int testitems;

        /// <summary>
        /// main window
        /// </summary>
        public wndMain()
        {
            try
            {
                InitializeComponent();

                updatecb();
                loadLatestInvoiceNum();
                invoiceNumber.Visibility = Visibility.Hidden;
                invoiceDate.Visibility = Visibility.Hidden;
                totalCost.Visibility = Visibility.Hidden;


                invoice = new DataSet();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// cant delete
        /// </summary>
        /// <param name="item"></param>
        private void getCost(string item)
        {

        }

        /// <summary>
        /// this method loops throught the data grid and returns a total number cost
        /// </summary>
        private void loopThroughforTotal()
        {
            try
            {
                int count = 0;
                for (int i = 0; i < dataGrid.Items.Count; i++)
                {
                    DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(i);
                    if (row != null)
                    {
                        DataGridCell cell = dataGrid.Columns[2].GetCellContent(row).Parent as DataGridCell;
                        if (cell != null && cell.Content is TextBlock)
                        {
                            string value = (cell.Content as TextBlock).Text;
                            int number;
                            if (int.TryParse(value, out number))
                            {
                                
                                count += number;
                            }
                        }
                    }
                }
                //totalCost.Content = count;
                totalInvoiceCost = count;
                totalCost.Content = totalInvoiceCost;
                totalCost.Visibility = Visibility.Visible;
                MessageBox.Show(count.ToString());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }



        /// <summary>
        /// this method updates the datagrid
        /// </summary>
        private void updateDataGrid()
        {
            try
            {
                var datSet = mainClass.fillTable(invoiceNumber.Content.ToString());
                dataGrid.ItemsSource = datSet.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }


        /// <summary>
        /// this method updates the combobox
        /// </summary>
        private void updatecb()
        {
            try { 
            DataTable list = mainClass.getItems();

            for (int i = 0; i < list.Rows.Count; i++)
            {
                cmbItems.Items.Add(list.Rows[i][1]);
            }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

        }



        
        /// <summary>
        ///this is the search button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try { 
            this.Close();
            seachWindow.ShowDialog();
            //this.Close();
            searchWindow.ShowDialog();
                //seachWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// this is for the edit items window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Items_Click(object sender, RoutedEventArgs e)
        {
            try { 
            //this.Close();
            itemsWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// this method is for when a combo box item is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbItems_Selection(object sender, SelectionChangedEventArgs e)
        {
            try { 
            if (cmbItems.SelectedItem != null)
            {
                var selectedItem = cmbItems.SelectedItem.ToString();
                getPrices();
                getCode();
                itemName = cmbItems.SelectedItem.ToString();
                getHighestLineItem();

            }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }


     
        /// <summary>
        /// this gets the prices of the items
        /// </summary>
        private void getPrices()
        {
            try { 
            if (cmbItems.SelectedItem != null)
            {
                var description = cmbItems.SelectedItem.ToString();
                costTextBox.Text = mainClass.getPrice(description);
                itemCost = costTextBox.Text;
            }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// this method gets the item code from the DB
        /// </summary>
        private void getCode()
        {
            try { 
            if (cmbItems.SelectedItem != null)
            {
                var passIn = cmbItems.SelectedItem.ToString();
                itemCodeLabel.Content = mainClass.getCode(passIn);
                itemCode = itemCodeLabel.Content.ToString();
            }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        
        /// <summary>
        /// This method will insert an item
        /// </summary>
        private void insertItem()
        {
            try { 
            var result = mainClass.insertItem(invoiceNum, lineItem, itemCode);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// This button loads the latest invoice to edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Invoice_Click(object sender, RoutedEventArgs e)
        {
            try { 
            invoiceMode= true;
            if (invoiceMode == true)
            {
                disableButtons();
                updateDataGrid();
                invoiceNumber.Visibility = Visibility.Visible;
                invoiceDate.Visibility = Visibility.Visible;
                totalCost.Visibility = Visibility.Visible;
                getInvoiceDate();
                //getTotalCost();
                //getTotalRowCount();
                //loopThroughforTotal();
                
            }

                //getHighestLineItem();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }


        /// <summary>
        /// this is the method that gets the latest invoice from the DB
        /// </summary>
        private void loadLatestInvoiceNum()
        {
            try { 
            string latestInvoiceNum = mainClass.getLatestInvoice("");
            invoiceNumber.Content = latestInvoiceNum;
            varForInvoiceDate = latestInvoiceNum;
            invoiceNum = int.Parse(latestInvoiceNum);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// This gets the latest invoice date
        /// </summary>
        private void getInvoiceDate()
        {
            try { 
            string getDate = mainClass.getInvoiceDate(varForInvoiceDate);
            invoiceDate.Content = getDate;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// this gets the total cost
        /// </summary>
        private void getTotalCost()
        {
            try { 
            string getCost = mainClass.getInvoiceCost(varForInvoiceDate);
            totalCost.Content = getCost;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// this di
        /// </summary>
        public void disableButtons()
        {
            try { 
            btnEdit_Invoice.IsEnabled=false;
            btnCreate_Invoice.IsEnabled=false;
            btnSearch.IsEnabled = false;
            btnEdit_Items.IsEnabled = false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// method will enable all the buttons
        /// </summary>
        public void enableButtons()
        {
            try { 
            btnEdit_Invoice.IsEnabled = true;
            btnCreate_Invoice.IsEnabled = true;
            btnSearch.IsEnabled = true;
            btnEdit_Items.IsEnabled = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// button creates an invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Invoice_Click(object sender, RoutedEventArgs e)
        {
            try { 
            invoiceMode = false;
            if (invoiceMode == false)
            {
                totalCost.Visibility = Visibility.Visible;
                DateTime today = DateTime.Today;
                today.ToString();
                string totalcost = "0";
                invoiceNumber.Visibility = Visibility.Visible;
                invoiceDate.Visibility = Visibility.Visible;
                invoiceDate.Content = DateTime.Now.ToString("MM/dd/yyyy");
                invoiceNumber.Content = "TBH";
                dataGrid.ItemsSource = null;
                mainClass.createInvoice(today.ToString(), totalcost);
                loadLatestInvoiceNum();
               
                disableButtons();
            }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// button will save the invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnSave_Invoice_Click(object sender, RoutedEventArgs e)
        {
            try { 
            //loopThroughforTotal();
            loadLatestInvoiceNum();
            invoiceNumber.Visibility = Visibility.Hidden;
            invoiceDate.Visibility = Visibility.Hidden;
            totalCost.Visibility = Visibility.Hidden;
            dataGrid.ItemsSource = null;
            enableButtons();
            //loopThroughforTotal();

            var updateCost = mainClass.updateTotalCost(totalInvoiceCost, invoiceNum);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// method will add an item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Item_Click(object sender, RoutedEventArgs e)
        {
            try { 
            getCode();
            insertItem();
            updateDataGrid();
            getTotalRowCount();

            loopThroughforTotal();
            getHighestLineItem();

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// method will return the selected row
        /// </summary>
        private void getSelectedRow()
        {
            try { 
            if (dataGrid.SelectedItem != null)
            {
                // Get the row index of the selected item
                int rowIndex = dataGrid.Items.IndexOf(dataGrid.SelectedItem);

                // Loop through the items in the data grid
                for (int i = 0; i < dataGrid.Items.Count; i++)
                {
                    // If the current item is the selected item, display its row number
                    if (i == rowIndex)
                    {
                        MessageBox.Show($"Selected item is on row {i + 1}");
                        MessageBox.Show(itemCode);
                    }
                }
                testitems = rowIndex + 1;
            }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// method will get the total number of rows and return
        /// </summary>
        private void getTotalRowCount()
        {
            try { 
            int selectedIndex = -1; // initialize to -1 to indicate that no row is selected yet
            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                if (dataGrid.SelectedIndex == i)
                { // check if current row is selected
                    selectedIndex = i; // set selectedIndex to the index of the selected row
                    break; // exit loop early since we found the selected row
                }
            }

            int rowCount = dataGrid.Items.Count; // get total row count
                                                 //lineItem = rowCount;
                                                 //MessageBox.Show("Total row count is " + lineItem);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// method for when you click on an item in the data grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clicked(object sender, SelectionChangedEventArgs e)
        {
            try { 
            if (dataGrid.SelectedItem != null)
            {
                if (dataGrid.SelectedCells.Count >= 0)
                {
                    // Get the row index of the selected cells
                    int rowIndex = dataGrid.Items.IndexOf(dataGrid.SelectedCells[0].Item);
                    // Get the contents of the cells in the selected row
                    TextBlock textBlock = dataGrid.Columns[0].GetCellContent(dataGrid.Items[rowIndex]) as TextBlock;
                    string value1 = textBlock != null ? textBlock.Text : "";
                    // Display value1 in a message box
                    itemCode = value1;
                    //if (int.TryParse(value1, out testitems))
                    //{
                    //    MessageBox.Show(testitems.ToString());
                    //}
                }

                getSelectedRow();
            }

            getTotalRowCount();

                //getHighestLineItem();

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }


        }

        /// <summary>
        /// button will remove an item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Item_Click(object sender, RoutedEventArgs e)
        {
            try { 
            var selectedItem = dataGrid.SelectedItem;
            
            
                mainClass.removeItems(itemCode,invoiceNum);
                updateDataGrid();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }


        /// <summary>
        /// this will get the highest item number from the table in the DB
        /// </summary>
        private void getHighestLineItem()
        {
            try { 

            if (dataGrid.Items.Count > 0)
            {
                var highestLineItem = mainClass.getHighestLineItem(invoiceNum);
                //MessageBox.Show($"The highest line item number for invoice {invoiceNum} is {highestLineItem}.");

                lineItem = int.Parse(highestLineItem);
                lineItem++;
                //MessageBox.Show($"The highest line item number for invoice {invoiceNum} is {lineItem}.");
                highestRow.Content = lineItem;
            }
            else
            {
                lineItem = 1;
            }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        
    }
}
