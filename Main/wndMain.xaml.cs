﻿using GroupProject.Items;
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

        public string itemCode = "H";

        public int invoiceNum = 5003;

        public int lineItem = 3;

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
            MessageBox.Show(result);
        }

        private void btnEdit_Invoice_Click(object sender, RoutedEventArgs e)
        {
            btnCreate_Invoice.IsEnabled = false;
            btnSearch.IsEnabled= false;
            btnEdit_Items.IsEnabled= false;
            updateDataGrid();
            invoiceNumber.Visibility = Visibility.Visible;
            invoiceDate.Visibility = Visibility.Visible;
            totalCost.Visibility = Visibility.Visible;
            getInvoiceDate();
            getTotalCost();
        }

        private void loadLatestInvoiceNum()
        {
            string latestInvoiceNum = mainClass.getLatestInvoice("");
            invoiceNumber.Content = latestInvoiceNum;
            varForInvoiceDate = latestInvoiceNum;
            //invoiceNum = latestInvoiceNum;
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

        private void btnCreate_Invoice_Click(object sender, RoutedEventArgs e)
        {
            invoiceNumber.Visibility = Visibility.Visible;
            invoiceDate.Visibility = Visibility.Visible;
            invoiceDate.Content = DateTime.Now.ToString("MM/dd/yyyy");
            invoiceNumber.Content = "TBD";
            dataGrid.ItemsSource = null;
            btnEdit_Invoice.IsEnabled = false;
            btnSearch.IsEnabled = false;
            btnEdit_Items.IsEnabled = false;
        }

        private void btnSave_Invoice_Click(object sender, RoutedEventArgs e)
        {
            loadLatestInvoiceNum();
            invoiceNumber.Visibility = Visibility.Hidden;
            invoiceDate.Visibility = Visibility.Hidden;
            totalCost.Visibility = Visibility.Hidden;
            dataGrid.ItemsSource = null;
            if (btnEdit_Invoice.IsEnabled == false) {
                btnEdit_Invoice.IsEnabled = true;
                btnSearch.IsEnabled = true;
                btnEdit_Items.IsEnabled = true;
            }
            else if(btnCreate_Invoice.IsEnabled == false)
            {
                btnCreate_Invoice.IsEnabled = true;
                btnSearch.IsEnabled = true;
                btnEdit_Items.IsEnabled = true;
            }
        }

        private void btnAdd_Item_Click(object sender, RoutedEventArgs e)
        {
            insertItem();
        }
    }
}
