using GroupProject.Main;
using System;
using System.Collections.Generic;
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
        wndMain mainWindow = new wndMain();

        public wndSearch1()
        {
            InitializeComponent();
            // Method for displaying all invoices from clsSearch.cs
        }

        // sSelectedInvoiceID - Local variable. If a user has selected an invoice in the Data grid store that Invoice Num, otherwise variable is empty.

        // SelectedInvoiceID - Property main window can access to get the selected invoice ID

        /// <summary>
        /// Method for the Invoice Number dropdown box selection.
        /// When a user selects an invoice number, filter the results in the Invoice Data Grid by the chosen number
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
            this.Close();
            mainWindow.ShowDialog();
            
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

        }
    }
}
