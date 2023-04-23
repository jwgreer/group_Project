using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Data.OleDb;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Main
{
    class clsMainSQL
    {
        
         
        /// <summary>
        /// Connection string to the database.
        /// </summary>
        private string sConnectionString;

        /// <summary>
        /// Constructor that sets the connection string to the database
        /// </summary>
		public clsMainSQL()
        {
                sConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data source= " + Directory.GetCurrentDirectory() + "\\Invoice.mdb";          
        }

        /// <summary>
        /// This method takes an SQL statement that is passed in and executes it.  The resulting values
        /// are returned in a DataSet.  The number of rows returned from the query will be put into
        /// the reference parameter iRetVal.
        /// </summary>
        /// <param name="sSQL">The SQL statement to be executed.</param>
        /// <param name="iRetVal">Reference parameter that returns the number of selected rows.</param>
        /// <returns>Returns a DataSet that contains the data from the SQL statement.</returns>
		public DataSet ExecuteSQLStatement(string sSQL, ref int iRetVal)
        {
            try
            {
                //Create a new DataSet
                DataSet ds = new DataSet();

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {

                        //Open the connection to the database
                        conn.Open();

                        //Add the information for the SelectCommand using the SQL statement and the connection object
                        adapter.SelectCommand = new OleDbCommand(sSQL, conn);
                        adapter.SelectCommand.CommandTimeout = 0;

                        //Fill up the DataSet with data
                        adapter.Fill(ds);
                    }
                }

                //Set the number of values returned
                iRetVal = ds.Tables[0].Rows.Count;

                //return the DataSet
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method takes an SQL statement that is passed in and executes it.  The resulting single 
        /// value is returned.
        /// </summary>
        /// <param name="sSQL">The SQL statement to be executed.</param>
        /// <returns>Returns a string from the scalar SQL statement.</returns>
		public string ExecuteScalarSQL(string sSQL)
        {
            try
            {
                //Holds the return value
                object obj;

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {

                        //Open the connection to the database
                        conn.Open();

                        //Add the information for the SelectCommand using the SQL statement and the connection object
                        adapter.SelectCommand = new OleDbCommand(sSQL, conn);
                        adapter.SelectCommand.CommandTimeout = 0;

                        //Execute the scalar SQL statement
                        obj = adapter.SelectCommand.ExecuteScalar();
                    }
                }

                //See if the object is null
                if (obj == null)
                {
                    //Return a blank
                    return "";
                }
                else
                {
                    //Return the value
                    return obj.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        

        /// <summary>
        /// This method takes an SQL statement that is a non query and executes it.
        /// </summary>
        /// <param name="sSQL">The SQL statement to be executed.</param>
        /// <returns>Returns the number of rows affected by the SQL statement.</returns>
        public int ExecuteNonQuery(string sSQL)
        {
            try
            {
                //Number of rows affected
                int iNumRows;

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    //Open the connection to the database
                    conn.Open();

                    //Add the information for the SelectCommand using the SQL statement and the connection object
                    OleDbCommand cmd = new OleDbCommand(sSQL, conn);
                    cmd.CommandTimeout = 0;

                    //Execute the non query SQL statement
                    iNumRows = cmd.ExecuteNonQuery();
                }

                //return the number of rows affected
                return iNumRows;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }




        //- UPDATE Invoices SET TotalCost = 1200 WHERE InvoiceNum = 123
        /// <summary>
        /// SQL query will update invoice data and set total cost
        /// </summary>
        /// <param name="sInvoiceID"></param>
        /// <param name="sTotalCost"
        /// <returns></returns>
        public string UpdateInvoiceData(int sTotalCost, int sInvoiceID)
        {
            string sSQL = "UPDATE Invoices SET TotalCost = " + sTotalCost + " WHERE InvoiceNum = " + sInvoiceID;

            return sSQL;
        }
        //- INSERT INTO LineItems(InvoiceNum, LineItemNum, ItemCode) Values(123, 1, 'AA')

        /// <summary>
        /// This query will insert itmes into the invoice
        /// </summary>
        /// <param name="sInvoiceID"></param>
        /// <param name="sLineItemNum"></param>
        /// <param name="sItemCode"></param>
        /// <returns></returns>
        public string InstertItems(int sInvoiceID, int sLineItemNum, string sItemCode)
        {
            string sSQL = "INSERT INTO LineItems(InvoiceNum, LineItemNum, ItemCode) Values('" + sInvoiceID + "', '" + sLineItemNum + "', '" + sItemCode + "')";

            return sSQL;
        }

        //- INSERT INTO Invoices(InvoiceDate, TotalCost) Values(#4/13/2018#, 100)

        /// <summary>
        /// This query will insert a new invoice with the date and cost associated with it
        /// </summary>
        /// <param name="sInvoiceDate"></param>
        /// <param name="sTotalCost"></param>
        /// <returns></returns>
        public string InsertInvoice(string sInvoiceDate, string sTotalCost)
        {
            string sSQL = "INSERT INTO Invoices(InvoiceDate, TotalCost) Values(#"+sInvoiceDate+"#,"+sTotalCost+")";

            return sSQL;
        }

        //- SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = 123
        /// <summary>
        /// This query will select an invoice from a number and return it
        /// </summary>
        /// <param name="sInvoiceNum"></param>
        /// <returns></returns>
        public string SelectInvoiceNum(string sInvoiceNum)
        {
            string sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = " + sInvoiceNum;

            return sSQL;
        }

        //- select ItemCode, ItemDesc, Cost from ItemDesc
        /// <summary>
        /// This Query will select and item from the item code and return it
        /// </summary>
        /// <param name="sItemCode"></param>
        /// <returns></returns>
        public string SelectItem(string sItemCode)
        {
            string sSQL = "SELECT ItemCode, ItemDesc, Cost from ItemDesc WHERE ItemCode = " + sItemCode;

            return sSQL;
        }

        //- SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost FROM LineItems, ItemDesc Where LineItems.ItemCode = ItemDesc.ItemCode And LineItems.InvoiceNum = 5000
        /// <summary>
        /// This query will select line itmes, and return the entire line item
        /// </summary>
        /// <param name="sLineItems"></param>
        /// <returns></returns>
        public string SelectLineItems(string invoiceNum) //public string SelectLineItems(string sLineItems) i am running tests
        {
            string sSQL = "SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost FROM LineItems, ItemDesc Where LineItems.ItemCode = ItemDesc.ItemCode And LineItems.InvoiceNum =" + invoiceNum;

            return sSQL;
        }

        //- DELETE FROM LineItems WHERE InvoiceNum = 5000
        /// <summary>
        /// This query will delete line items matching a invoice number 
        /// </summary>
        /// <param name="sInvoiceNum"></param>
        /// <returns></returns>
        public string DeleteLineItems(string sItemCode)
        {
            string sSQL = "DELETE FROM LineItems WHERE ItemCode = '" + sItemCode + "' AND InvoiceNum = 5003";
            return sSQL;
        }

        public string getItems()
        {
            return "Select * From ItemDesc";
        }

        public string getPrice(string item)
        {
            return $"SELECT Cost FROM ItemDesc WHERE ItemDesc = '{item}'" ;
        }

        public string SelectMaxInvoiceNum(string invoiceNum)
        {
            string sSQL = "SELECT MAX(InvoiceNum) FROM Invoices";

            return sSQL;
        }

        public string getDate(string sInvoiceNum)
        {
            string sSQL = "SELECT FORMAT(InvoiceDate, 'MM/dd/yyyy') AS InvoiceDate FROM Invoices WHERE InvoiceNum = " + sInvoiceNum;

            return sSQL;
        }

        public string getTotalCost(string sInvoiceNum)
        {
            string sSQL = "SELECT TotalCost FROM Invoices WHERE InvoiceNum = " + sInvoiceNum;
            return sSQL;
        }

        public string getItemCode(string itemName)
        {
            string sSQL = $"SELECT ItemCode FROM ItemDesc WHERE ItemDesc = '{itemName}'";
            return sSQL;
        }

        
    }
}
