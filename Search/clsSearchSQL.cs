using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;

namespace GroupProject.Search
{
    /// <summary>
    /// SQL class for the Search Window
    /// Different methods for filtering search criteria.
    /// Each method returns an SQL string.
    /// </summary>
    class clsSearchSQL
    {

        /// <summary>
        /// This SQL gets all data on an invoice for a given InvoiceID.
        /// </summary>
        /// <param name="sInvoiceID">The InvoiceID for the invoice to retrieve all data.</param>
        /// <returns>All data for the given invoice.</returns>
        public string SelectInvoiceData(string sInvoiceID)
        {
            string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + sInvoiceID;

            return sSQL;
        }
        /// <summary>
        /// This SQL gets all invoices.
        /// </summary>
        /// <returns>All data for the given invoice.</returns>
        public string SelectAllInvoices()
        {
            string sSQL = "SELECT * FROM Invoices";

            return sSQL;
        }
        /// <summary>
        /// This SQL gets all data on an invoice for a given InvoiceID and InvoiceDate
        /// </summary>
        /// <param name="sInvoiceID">The InvoiceID for the invoice to retrieve all data.</param>
        /// <param name="sInvoiceDate">The InvoiceDate for the invoice to retrieve all data.</param>
        /// <returns>All data for the given invoice.</returns>
        public string SelectInvoiceData(string sInvoiceID, string sInvoiceDate)
        {
            string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + sInvoiceID + $" AND InvoiceDate = #{sInvoiceDate}# ";

            return sSQL;
        }
        /// <summary>
        /// This SQL gets all data on an invoice for a given InvoiceID and InvoiceDate and TotalCost
        /// </summary>
        /// <param name="sInvoiceID">The InvoiceID for the invoice to retrieve all data.</param>
        /// <param name="sInvoiceDate">The InvoiceDate for the invoice to retrieve all data.</param>
        /// <param name="sTotalCost">The TotalCost for the invoice to retrieve all data.</param>
        /// <returns>All data for the given invoice.</returns>
        public string SelectInvoiceData(string sInvoiceID, string sInvoiceDate, string sTotalCost)
        {
            string sSQL = $"SELECT * FROM Invoices WHERE InvoiceNum = {sInvoiceID} AND InvoiceDate = #{sInvoiceDate}# AND TotalCost = {sTotalCost}";

            return sSQL;
        }
        /// <summary>
        /// This SQL gets all data on an invoice for a given TotalCost
        /// </summary>
        /// <param name="sTotalCost">The sTotalCost for the invoice to retrieve all data.</param>
        /// <returns>All data for the given invoice.</returns>
        public string SelectInvoiceCost(string sTotalCost)
        {
            string sSQL = $"SELECT * FROM Invoices WHERE TotalCost = {sTotalCost}";

            return sSQL;
        }
        /// <summary>
        /// This SQL gets all data on an invoice for a given TotalCost and InvoiceDate
        /// </summary>
        /// <param name="sTotalCost">The sTotalCost for the invoice to retrieve all data.</param>
        /// <param name="sInvoiceDate">The InvoiceDate for the invoice to retrieve all data.</param>
        /// <returns>All data for the given invoice.</returns>
        public string SelectInvoiceCost(string sTotalCost, string sInvoiceDate)
        {
            string sSQL = $"SELECT * FROM Invoices WHERE TotalCost = {sTotalCost} and InvoiceDate = #{sInvoiceDate}#";

            return sSQL;
        }
        /// <summary>
        /// This SQL gets all data on an invoice for a given InvoiceDate
        /// </summary>
        /// <param name="sInvoiceDate">The InvoiceDate for the invoice to retrieve all data.</param>
        /// <returns>All data for the given invoice.</returns>
        public string SelectInvoiceDate(string sInvoiceDate)
        {
            string sSQL = $"SELECT * FROM Invoices WHERE InvoiceDate = #{sInvoiceDate}#";

            return sSQL;
        }
        /// <summary>
        /// This SQL gets all data on an invoice for a given sInvoiceDate
        /// </summary>
        /// <param name="sInvoiceDate">The sInvoiceDate for the invoice to retrieve all data.</param>
        /// <returns>All data for the given invoice.</returns>
        public string SelectDistinctInvoiceDate(string sInvoiceDate)
        {
            string sSQL = $"SELECT DISTINCT({sInvoiceDate}) From Invoices order by {sInvoiceDate}";

            return sSQL;
        }
        /// <summary>
        /// This SQL gets all data on an invoice for a given sTotalCost
        /// </summary>
        /// <param name="sTotalCost">The sTotalCost for the invoice to retrieve all data.</param>
        /// <returns>All data for the given invoice.</returns>
        public string SelectDistinctInvoiceCost(string sTotalCost)
        {
            string sSQL = $"SELECT DISTINCT({sTotalCost}) From Invoices order by {sTotalCost}";

            return sSQL;
        }
    }
}
