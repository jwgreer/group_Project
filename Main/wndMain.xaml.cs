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
       // wndSearch seachWindow = new wndSearch();

        wndItems itemsWindow = new wndItems();

        clsMainSQL sqlClass = new clsMainSQL();

        clsMainLogic mainClass = new clsMainLogic();

        public DataSet invoice;

        public DataTable datatable = new DataTable();

        public string varForInvoiceDate = "";

        public string itemName = "";

        public string itemCost = "";

        public string itemCode = "";

        public int invoiceNum;

        public int lineItem;

        public int totalInvoiceCost;

        bool invoiceMode;

        public string selectedItemCode = "";

        public int testitems;

        public wndMain()
        {
            InitializeComponent();
            
            updatecb();
            loadLatestInvoiceNum();
            invoiceNumber.Visibility = Visibility.Hidden;
            invoiceDate.Visibility = Visibility.Hidden;
            totalCost.Visibility = Visibility.Hidden;
            

            invoice = new DataSet();
        }




        private void loopThroughforTotal()
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
            totalInvoiceCost = count;

            MessageBox.Show(count.ToString());
        }




        private void updateDataGrid()
        {
            var datSet = mainClass.fillTable(invoiceNumber.Content.ToString());
            dataGrid.ItemsSource = datSet.Tables[0].DefaultView;
        }



        private void updatecb()
        {
            DataTable list = mainClass.getItems();

            for (int i = 0; i < list.Rows.Count; i++)
            {
                cmbItems.Items.Add(list.Rows[i][1]);
            }
        }



        private void getCost(string item)
        {

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
           //seachWindow.ShowDialog();
            
        }

        private void btnEdit_Items_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
            itemsWindow.ShowDialog();
            
        }

        private void cmbItems_Selection(object sender, SelectionChangedEventArgs e)
        {
            if (cmbItems.SelectedItem != null)
            {
                var selectedItem = cmbItems.SelectedItem.ToString();
                getPrices();
                getCode();
                itemName = cmbItems.SelectedItem.ToString();

            }
        }


     

        private void getPrices()
        {
            if (cmbItems.SelectedItem != null)
            {
                var description = cmbItems.SelectedItem.ToString();
                costTextBox.Text = mainClass.getPrice(description);
                itemCost = costTextBox.Text;
            }
        }

        private void getCode()
        {
            if (cmbItems.SelectedItem != null)
            {
                var passIn = cmbItems.SelectedItem.ToString();
                itemCodeLabel.Content = mainClass.getCode(passIn);
                itemCode = itemCodeLabel.Content.ToString();
            }
        }
        
        private void insertItem()
        {
            var result = mainClass.insertItem(invoiceNum, lineItem, itemCode);
            
        }


        private void btnEdit_Invoice_Click(object sender, RoutedEventArgs e)
        {
            invoiceMode= true;
            if (invoiceMode == true)
            {
                disableButtons();
                updateDataGrid();
                invoiceNumber.Visibility = Visibility.Visible;
                invoiceDate.Visibility = Visibility.Visible;
                totalCost.Visibility = Visibility.Visible;
                getInvoiceDate();
                getTotalCost();
                getTotalRowCount();
                loopThroughforTotal();
            }
            
        }

        private void loadLatestInvoiceNum()
        {
            string latestInvoiceNum = mainClass.getLatestInvoice("");
            invoiceNumber.Content = latestInvoiceNum;
            varForInvoiceDate = latestInvoiceNum;
            invoiceNum = int.Parse(latestInvoiceNum);
        }

        private void getInvoiceDate()
        {
            string getDate = mainClass.getInvoiceDate(varForInvoiceDate);
            invoiceDate.Content = getDate;
        }

        private void getTotalCost()
        {
            string getCost = mainClass.getInvoiceCost(varForInvoiceDate);
            totalCost.Content = getCost;
        }

        public void disableButtons()
        {
            btnEdit_Invoice.IsEnabled=false;
            btnCreate_Invoice.IsEnabled=false;
            btnSearch.IsEnabled = false;
            btnEdit_Items.IsEnabled = false;
        }

        public void enableButtons()
        {
            btnEdit_Invoice.IsEnabled = true;
            btnCreate_Invoice.IsEnabled = true;
            btnSearch.IsEnabled = true;
            btnEdit_Items.IsEnabled = true;
        }
        private void btnCreate_Invoice_Click(object sender, RoutedEventArgs e)
        {
            invoiceMode = false;
            if (invoiceMode == false)
            {
                invoiceNumber.Visibility = Visibility.Visible;
                invoiceDate.Visibility = Visibility.Visible;
                invoiceDate.Content = DateTime.Now.ToString("MM/dd/yyyy");
                invoiceNumber.Content = "TBD";
                dataGrid.ItemsSource = null;
                disableButtons();
            }
        }

        private void btnSave_Invoice_Click(object sender, RoutedEventArgs e)
        {
            loadLatestInvoiceNum();
            invoiceNumber.Visibility = Visibility.Hidden;
            invoiceDate.Visibility = Visibility.Hidden;
            totalCost.Visibility = Visibility.Hidden;
            dataGrid.ItemsSource = null;
            enableButtons();
            

            var updateCost = mainClass.updateTotalCost(totalInvoiceCost, invoiceNum);
        }

        private void btnAdd_Item_Click(object sender, RoutedEventArgs e)
        {
            insertItem();
            updateDataGrid();
            getTotalRowCount();
            
        }

        private void getSelectedRow()
        {
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

        private void getTotalRowCount()
        {
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
            lineItem = rowCount;
            MessageBox.Show("Total row count is " + lineItem);
        }

        private void clicked(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedCells.Count >= 0)
            {
                // Get the row index of the selected cells
                int rowIndex = dataGrid.Items.IndexOf(dataGrid.SelectedCells[0].Item);
                // Get the contents of the cells in the selected row
                TextBlock textBlock = dataGrid.Columns[0].GetCellContent(dataGrid.Items[rowIndex]) as TextBlock;
                string value1 = textBlock != null ? textBlock.Text : "";
                // Display value1 in a message box
                itemCode= value1;
                //if (int.TryParse(value1, out testitems))
                //{
                //    MessageBox.Show(testitems.ToString());
                //}
            }

            getSelectedRow();

            getTotalRowCount();
            


        }

        private void btnRemove_Item_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = dataGrid.SelectedItem;
            if(selectedItem != null)
            {
                mainClass.removeItems(itemCode);
                updateDataGrid();
            }
        }
    }
}
