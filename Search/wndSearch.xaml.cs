using GroupProject.Main;
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
using System.ComponentModel;
using GroupProject.Common;
using System.Drawing;

namespace GroupProject.Search
{
    /// <summary>
    /// Interaction logic for Search Window
    /// Once the Search Window opens, displays all invoices into the Data Grid using a list of invoice objects.
    /// Search window allows the user to search Invoices based on a few filters selected in a dropdown box:
    /// Invoice Number, Invoice Date, and Total Cost
    /// The user may use use each filter individually or may use invoice date and total Cost filters together.
    /// Once the user has selected an invoice, they can close the Search Window and by using a local variable that selected invoice gets passed to the Main Window for viewing.
    /// The user may also reset the search window to its inital state by selecting the respective button at any time.
    /// </summary>
    public partial class wndSearch1 : Window
    {
        /// <summary>
        /// Search logic class
        /// </summary>
        private clsSearchLogic clsSearchLogic;
        /// <summary>
        /// sSelectedInvoiceID - Local variable. If a user has selected an invoice in the Data grid store that Invoice Num, otherwise variable is empty.
        /// </summary>
        private string sSelectedInvoiceID = "";
        /// <summary>
        /// Currently Selected Invoice
        /// </summary>
        clsInvoice clsSelectedInvoice;
        /// <summary>
        /// Variable for updating the list for UI elements
        /// </summary>
        private List<clsInvoice> invoiceList;

        /// <summary>
        /// Constructor
        /// </summary>
        public wndSearch1()
        {
            try
            {
                InitializeComponent();
                clsSearchLogic = new clsSearchLogic();
                // Method for displaying all invoices from clsSearch.cs
                UpdateAllInvoiceLists();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Getter/Setter for SelectedInvoiceID property the main window can access to get selected invoice ID
        /// </summary>
        public string SelectedInvoiceID
        {
            get
            {
                return sSelectedInvoiceID;
            }
            set
            {
                sSelectedInvoiceID = value;

            }
        }

        /// <summary>
        /// Method for display unfiltered Invoices in all dropdown lists
        /// Binds combo box using the List of Invoices
        /// </summary>
        private void UpdateAllInvoiceLists()
        {
            try
            {
                UpdateInvoiceNumList(clsSearchLogic.GetDistinctInvoiceNumbers());

                UpdateInvoiceDateList(clsSearchLogic.GetDistinctInvoiceDates());

                UpdateInvoiceCostList(clsSearchLogic.GetDistinctInvoiceCharges());

                invoiceList = clsSearchLogic.GetAllInvoices();
                UpdateInvoiceGrid(invoiceList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Method for update the combobox list for Invoice Num
        /// </summary>
        /// <param name="invoiceList"></param>
        private void UpdateInvoiceNumList(List<clsInvoice> invoiceList)
        {
            try
            {
                cbInvoiceNum.ItemsSource = invoiceList;
                cbInvoiceNum.DisplayMemberPath = "invoiceNum";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Method for update the combobox list for Invoice Num
        /// </summary>
        /// <param name="invoiceList"></param>
        private void UpdateInvoiceDateList(List<clsInvoice> invoiceList)
        {
            try
            {
                cbInvoiceDate.ItemsSource = invoiceList;
                cbInvoiceDate.DisplayMemberPath = "invoiceDate";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Method for update the combobox list for Invoice Num
        /// </summary>
        /// <param name="invoiceList"></param>
        private void UpdateInvoiceCostList(List<clsInvoice> invoiceList)
        {
            try
            {
                cbInvoiceTotal.ItemsSource = invoiceList;
                cbInvoiceTotal.DisplayMemberPath = "invoiceCost";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Method for updating the Data grid only
        /// </summary>
        /// <param name="invoiceList"></param>
        private void UpdateInvoiceGrid(List<clsInvoice> invoiceList)
        {
            try
            {
                gridInvoices.ItemsSource = invoiceList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method for the Invoice Number dropdown box selection.
        /// When a user selects an invoice number, filter the results in the Invoice Data Grid by the chosen number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // If only the Invoice number filter has been selected
                if (cbInvoiceNum.SelectedIndex != -1)
                {
                    // Create Invoice reference
                    clsSelectedInvoice = (clsInvoice)cbInvoiceNum.SelectedItem;
                    invoiceList = new List<clsInvoice>();

                    // Search by Invoice Num
                    invoiceList = clsSearchLogic.SelectInvoiceNumber(clsSelectedInvoice.invoiceNum);

                    // Update other UI elements to reflect filter
                    UpdateInvoiceDateList(invoiceList);
                    UpdateInvoiceCostList(invoiceList);
                    UpdateInvoiceGrid(invoiceList);
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Method for Invoice Date dropdown box selection
        /// When a user selects an invoice by date, filter the results in the Invoice Data Grid by that date.
        /// May work alongside with Invoice Total Cost dropdown box selection for filtering results. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxInvoiceDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // If Number is already filtered do nothing
                if (cbInvoiceNum.SelectedIndex != -1)
                {

                }
                // If Cost and date are selected
                else if (cbInvoiceDate.SelectedIndex != -1 && cbInvoiceTotal.SelectedIndex != -1)
                {
                    // Create Invoice reference
                    clsSelectedInvoice = (clsInvoice)cbInvoiceDate.SelectedItem;
                    invoiceList = new List<clsInvoice>();

                    // Search by Invoice date and Cost
                    invoiceList = clsSearchLogic.SelectInvoiceCost(clsSelectedInvoice.invoiceCost, clsSelectedInvoice.invoiceDate);

                    // Update other UI elements to reflect filter
                    UpdateInvoiceNumList(invoiceList);
                    UpdateInvoiceGrid(invoiceList);
                    UpdateInvoiceDateList(invoiceList);
                }
                // if only Date is selected
                else if (cbInvoiceDate.SelectedIndex != -1)
                {
                    // Create Invoice reference
                    clsSelectedInvoice = (clsInvoice)cbInvoiceDate.SelectedItem;
                    invoiceList = new List<clsInvoice>();

                    // Search by Invoice date
                    invoiceList = clsSearchLogic.SelectInvoiceDate(clsSelectedInvoice.invoiceDate);

                    // Update other UI elements to reflect filter
                    UpdateInvoiceNumList(invoiceList);
                    UpdateInvoiceCostList(invoiceList);
                    UpdateInvoiceGrid(invoiceList);
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Method for Invoice Total Cost dropdown box selection
        /// When a user selects an invoice by the total Cost, filter the results in the Invoice Data Grid by that amount,
        /// Sorted from smalled to largest.
        /// May work alongside with Invoice Date dropdown box selection for filtering results.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxInvoiceTotal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // If Number is already filtered do nothing
                if (cbInvoiceNum.SelectedIndex != -1)
                {

                }
                // If Cost and date are selected
                else if (cbInvoiceDate.SelectedIndex != -1 && cbInvoiceTotal.SelectedIndex != -1)
                {
                    // Create Invoice reference
                    clsSelectedInvoice = (clsInvoice)cbInvoiceTotal.SelectedItem;
                    invoiceList = new List<clsInvoice>();
                    
                    // Search by Invoice date and Cost
                    invoiceList = clsSearchLogic.SelectInvoiceCost(clsSelectedInvoice.invoiceCost, clsSelectedInvoice.invoiceDate);

                    // Update other UI elements to reflect filter
                    UpdateInvoiceNumList(invoiceList);
                    UpdateInvoiceGrid(invoiceList);
                    UpdateInvoiceCostList(invoiceList);
                }
                // if only Cost is selected
                else if (cbInvoiceTotal.SelectedIndex != -1)
                {
                    // Create Invoice reference
                    clsSelectedInvoice = (clsInvoice)cbInvoiceTotal.SelectedItem;
                    invoiceList = new List<clsInvoice>();

                    // Search by Invoice cost
                    invoiceList = clsSearchLogic.SelectInvoiceCost(clsSelectedInvoice.invoiceCost);

                    // Update other UI elements to reflect filter
                    UpdateInvoiceNumList(invoiceList);
                    UpdateInvoiceDateList(invoiceList);
                    UpdateInvoiceGrid(invoiceList);
                }

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Method for selecting an invoice for viewing on the main screeen.
        /// As long as a user has selected an invoice in the Invoice Data Grid,
        /// Pass that invoice number using the local variable to the main window for viewing.
        /// The search window is also hidden.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInvoiceSelect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Method for reseting the search window to its initial state.
        /// When a user presses this button,
        /// Clear the Invoice Data Grid of any selection and
        /// all dropdown boxes are reset to have unselected values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInvoiceClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sSelectedInvoiceID = "";
                UpdateAllInvoiceLists();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Method for canceling the invoice search.
        /// When pressed, Cancels the search by hiding the Search Window to return to the Main window.
        /// Nothing is passed to the Main Window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInvoiceCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Method for Handling Errors
        /// Top level method when called displays a message to the user with the error,
        /// If an error occurs when displaying, save a file into the C drive with the Exception message.
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }

        /// <summary>
        /// Method will display the selected Invoice number in the label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridInvoices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                clsSelectedInvoice = (clsInvoice)gridInvoices.SelectedItem;
                if (clsSelectedInvoice != null)
                {
                    lblSelectedInvoice.Content = clsSelectedInvoice.ToString();
                }
                else
                {
                    lblSelectedInvoice.Content = "";
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
            
        }
        /// <summary>
        /// Method will adjust column widths when Datagrid is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var column in ((DataGrid)sender).Columns)
            {
                column.Width = new DataGridLength(column.ActualWidth + 83);
            }
        }
    }
}
