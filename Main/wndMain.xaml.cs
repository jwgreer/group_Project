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



        public wndMain()
        {
            InitializeComponent();
            
            updatecb();
            loadLatestInvoiceNum();

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
                
            }
        }

        /*
        private void getPrice()
        {
            costTextBox.Text = mainClass.getPrice();
        }
        */

        private void btnEdit_Invoice_Click(object sender, RoutedEventArgs e)
        {
            updateDataGrid();
        }

        private void loadLatestInvoiceNum()
        {
            string latestInvoiceNum = mainClass.getLatestInvoice("");
            invoiceNumber.Content = latestInvoiceNum;
        }

    }
}
