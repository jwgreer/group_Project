using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
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


        /// <summary>
        /// gets the items from the DB
        /// </summary>
        /// <returns></returns>
        public DataTable getItems()
        {
            try
            {
                int count = 0;
                clsMainSQL clsData = new clsMainSQL();
                var query = sqlClass.getItems();
                var dataset = clsData.ExecuteSQLStatement(query, ref count);
                return dataset.Tables[0];
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return new DataTable(); // add this line
        }

        /// <summary>
        /// gets the price from the DB
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string getPrice(string item)
        {
            try
            {
                var query = sqlClass.getPrice(item);
                return sqlClass.ExecuteScalarSQL(query);
            }
            catch (Exception ex)
            {
                //error handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + " " +
                    MethodInfo.GetCurrentMethod().Name + " ->" + ex.Message);
            }
        }

        // <summary>
        /// Gets the item Code from the DB
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string getCode(string itemName)
        {
            try
            {
                var query = sqlClass.getItemCode(itemName);
                return sqlClass.ExecuteScalarSQL(query);
            }
            catch (Exception ex)
            {
                //error handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + " " +
                    MethodInfo.GetCurrentMethod().Name + " ->" + ex.Message);
            }
        }

        /// <summary>
        /// this fills the data grid
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public DataSet fillTable(string invoiceNum)
        {
            try
            {
                var count = 0;
                var query = sqlClass.SelectLineItems(invoiceNum);
                var dataset = sqlClass.ExecuteSQLStatement(query, ref count);
                return dataset;
            }
            catch (Exception ex)
            {
                //error handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + " " +
                    MethodInfo.GetCurrentMethod().Name + " ->" + ex.Message);
            }
        }

        /// <summary>
        /// this craetes an invoice
        /// </summary>
        /// <param name="sInvoiceDate"></param>
        /// <param name="sTotalCost"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string createInvoice(string sInvoiceDate, string sTotalCost)
        {
            try
            {
                var query = sqlClass.InsertInvoice(sInvoiceDate, sTotalCost);
                return sqlClass.ExecuteScalarSQL(query);
            }
            catch (Exception ex)
            {
                //error handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + " " +
                    MethodInfo.GetCurrentMethod().Name + " ->" + ex.Message);
            }
        }
        /// <summary>
        /// this gets the latest invoice
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string getLatestInvoice(string invoiceNum)
        {
            try
            {
                var query = sqlClass.SelectMaxInvoiceNum(invoiceNum);
                return sqlClass.ExecuteScalarSQL(query);
            }
            catch (Exception ex)
            {
                //error handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + " " +
                    MethodInfo.GetCurrentMethod().Name + " ->" + ex.Message);
            }
        }

        /// <summary>
        /// this gets the invoice date
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string getInvoiceDate(string invoiceNum)
        {
            try
            {
                var query = sqlClass.getDate(invoiceNum);
                return sqlClass.ExecuteScalarSQL(query);
            }
            catch (Exception ex)
            {
                //error handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + " " +
                    MethodInfo.GetCurrentMethod().Name + " ->" + ex.Message);
            }
        }

        /// <summary>
        /// this gets the invoice cost
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string getInvoiceCost(string invoiceNum)
        {
            try
            {
                var query = sqlClass.getTotalCost(invoiceNum);
                return sqlClass.ExecuteScalarSQL(query);
            }
            catch (Exception ex)
            {
                //error handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + " " +
                    MethodInfo.GetCurrentMethod().Name + " ->" + ex.Message);
            }
        }

        /// <summary>
        /// this inserts an item
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="lineItemNum"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string insertItem(int invoiceNum, int lineItemNum, string itemCode)
        {
            try
            {
                var query = sqlClass.InstertItems(invoiceNum, lineItemNum, itemCode);
                var result = sqlClass.ExecuteNonQuery(query);
                return result.ToString();
            }
            catch (Exception ex)
            {
                //error handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + " " +
                    MethodInfo.GetCurrentMethod().Name + " ->" + ex.Message);
            }
        }

        /// <summary>
        /// this updates the total cost
        /// </summary>
        /// <param name="totalCost"></param>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string updateTotalCost(int totalCost, int invoiceNum)
        {
            try
            {
                var query = sqlClass.UpdateInvoiceData(totalCost, invoiceNum);
                var result = sqlClass.ExecuteNonQuery(query);
                return result.ToString();
            }
            catch (Exception ex)
            {
                //error handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + " " +
                    MethodInfo.GetCurrentMethod().Name + " ->" + ex.Message);
            }
        }

        /// <summary>
        /// this removes an item
        /// </summary>
        /// <param name="sItemCode"></param>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string removeItems(string sItemCode, int invoiceNum)
        {
            try
            {
                var query = sqlClass.DeleteLineItems(sItemCode, invoiceNum);
                var result = sqlClass.ExecuteNonQuery(query);
                return result.ToString();
            }
            catch (Exception ex)
            {
                //error handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + " " +
                    MethodInfo.GetCurrentMethod().Name + " ->" + ex.Message);
            }
        }

        /// <summary>
        /// this gets the highest line item
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string getHighestLineItem(int invoiceNum)
        {
            try
            {
                var query = sqlClass.getHighestLineItem(invoiceNum);
                var result = sqlClass.ExecuteScalarSQL(query);
                return result.ToString();
            }
            catch (Exception ex)
            {
                //error handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + " " +
                    MethodInfo.GetCurrentMethod().Name + " ->" + ex.Message);
            }
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
