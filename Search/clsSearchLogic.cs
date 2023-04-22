using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Search
{
    /// <summary>
    /// Business Logic class for the Search Window
    /// Contains methods for connecting to the DB
    /// Contains search/filter methods for Invoices from the DB
    /// Queries the DB using InvoiceID/InvoiceDate/InvoiceCost passed into the SQL statements in clsSearchSQL
    /// Any Invoices returned from the Queries is added to a list.
    /// List is then returned to the top level method called by the Search Window.
    /// </summary>
    class clsSearchLogic
    {
        // Invoice class
        // clsInvoice invoice
        // Invoice class List
        // List<clsInvoices>

        /// <summary>
        /// Connection string to the DB
        /// </summary>
        //private string sConnectionString;

       // clsSearchLogic() 
        //{
            // Connect to the "mdb" database
           // sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\Invoice.mdb";
        //}

        // Method for executing the passed in SQL statement
        // public DataSet ExecuteSQLStatement(string sSQL)

        // GetDistinctInvoiceNumber
        // GetDistinctInvoiceDates
        // GetDistinctInvoiceCharges
        // GetAllInvoices() - returns List<clsInvoices>
        // GetInvoices(InvoiceNumber, InvoiceDate, TotalCost) - returns List<clsInvoices>
    }
}
