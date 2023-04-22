using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Main
{

   /// <summary>
    /// Main logic class for main window
    /// This will contain the methods for connecting to the DB
    /// Contains buttons to Create, save, and edit an invoice
    /// Item list will need to be updated anytime the page loads
    /// Invoices edited or created will get added to the invoice list after being saved
    /// Datagrid will display items on invoice
    /// </summary>

   // class clsMainLogic
//{

    class clsMainLogic
    {

        public clsMainSQL sqlClass = new clsMainSQL();


        private string sCode;

        private string sItemDesc;

        private int sCost;
        
        /// <summary>
        /// Getter and setter for code
        /// </summary>
        public string Code { get { return sCode; } set { sCode = value; } }


        /// <summary>
        /// getter and Setter for Item Desc
        /// </summary>
        public string ItemDesc { get { return sItemDesc; } set { sItemDesc = value; } }

        /// <summary>
        /// getter and setter for Cost
        /// </summary>
        public int Cost { get { return sCost; } set { sCost = value; } }

        public DataTable getItems()
        {
            int count = 0;
            clsMainSQL clsData = new clsMainSQL();
            var query = sqlClass.getItems();
            var dataset = clsData.ExecuteSQLStatement(query, ref count);
            return dataset.Tables[0];
        }

        public string getPrice(string item)
        {
            var query = sqlClass.getPrice(item);
            return sqlClass.ExecuteScalarSQL(query);
        }

        public DataSet fillTable(string invoiceNum)
        {
            var count = 0;
            var query = sqlClass.SelectLineItems(invoiceNum);
            var dataset = sqlClass.ExecuteSQLStatement(query, ref count);
            return dataset;
        }

        public string getLatestInvoice(string invoiceNum)
        {
            var query = sqlClass.SelectMaxInvoiceNum(invoiceNum);
            return sqlClass.ExecuteScalarSQL(query);
        }

        // invoice class
        // item class
        // invoice class list
        // list <clsInvoice>
        // item class list
        // item <clsItems>

        //string to conncet to DB


        //GetAllItems
        //SaveNewInvoice
        //EditInvoice
        //GetInvoice

        //When Item window is closed, on load Main window will need to refresh combo boxes with items in it
        //cmbItem Refreshonload


        // }


    }
}
