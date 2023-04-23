using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GroupProject.Common;

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
        
        /// <summary>
        /// Invoice class - clsInvoice invoice
        /// </summary>
        private clsInvoice invoice;
        /// <summary>
        /// Class object for SQL statements
        /// </summary>
        private clsSearchSQL searchSQL;
        /// <summary>
        /// Invoice class List - List<clsInvoices>
        /// </summary>
        private List<clsInvoice> lstInvoices { get; set; }
        /// <summary>
        /// Connection string to the DB
        /// </summary>
        private string sConnectionString;
        /// <summary>
        /// Variable for number of flights taken from SQL query
        /// </summary>
        private int numOfInvoices;

        /// <summary>
        /// Constructor
        /// </summary>
        public clsSearchLogic() 
        {
            try
            {
                // Connect to the "mdb" database
                sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\Invoice.mdb";
                searchSQL = new clsSearchSQL();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Method to return Invoices by Invoice Date
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <returns></returns>
        public List<clsInvoice> SelectInvoiceDate(string invoiceDate)
        {
            try
            {
                lstInvoices = new List<clsInvoice>();
                DataSet ds;
                // Query db for invoices by specified number, date, cost
                ds = ExecuteSQLStatement(searchSQL.SelectInvoiceDate(invoiceDate), ref numOfInvoices);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lstInvoices.Add(new clsInvoice(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString()));
                }

                return lstInvoices;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Method to return Invoices by Invoice Total Cost and Date
        /// </summary>
        /// <param name="invoiceCost"></param>
        /// <param name="invoiceDate"></param>
        /// <returns>List of invoices</returns>
        public List<clsInvoice> SelectInvoiceCost(string invoiceCost, string invoiceDate)
        {
            try
            {
                lstInvoices = new List<clsInvoice>();
                DataSet ds;
                // Query db for invoices by specified number, date, cost
                ds = ExecuteSQLStatement(searchSQL.SelectInvoiceCost(invoiceCost, invoiceDate), ref numOfInvoices);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lstInvoices.Add(new clsInvoice(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString()));
                }

                return lstInvoices;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Method to return Invoices by Invoice Total Cost
        /// </summary>
        /// <param name="invoiceCost"></param>
        /// <returns>List of invoices</returns>
        public List<clsInvoice> SelectInvoiceCost(string invoiceCost)
        {
            try
            {
                lstInvoices = new List<clsInvoice>();
                DataSet ds;
                // Query db for invoices by specified number, date, cost
                ds = ExecuteSQLStatement(searchSQL.SelectInvoiceCost(invoiceCost), ref numOfInvoices);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lstInvoices.Add(new clsInvoice(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString()));
                }

                return lstInvoices;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Method to return Invoices by Invoice Number and Date
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="invoiceDate"></param>
        /// <returns>List of invoices</returns>
        public List<clsInvoice> SelectInvoiceNumberAndDate(string invoiceNum, string invoiceDate)
        {
            try
            {
                lstInvoices = new List<clsInvoice>();
                DataSet ds;
                // Query db for invoices by specified number, date, cost
                ds = ExecuteSQLStatement(searchSQL.SelectInvoiceData(invoiceNum, invoiceDate), ref numOfInvoices);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lstInvoices.Add(new clsInvoice(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString()));
                }

                return lstInvoices;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to return Invoices by Invoice Number
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns>List of invoices</returns>
        public List<clsInvoice> SelectInvoiceNumber(string invoiceNum)
        {
            try
            {
                lstInvoices = new List<clsInvoice>();
                DataSet ds;
                // Query db for invoices by specified number, date, cost
                ds = ExecuteSQLStatement(searchSQL.SelectInvoiceData(invoiceNum), ref numOfInvoices);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lstInvoices.Add(new clsInvoice(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString()));
                }

                return lstInvoices;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// GetDistinctInvoiceNumber
        /// Used when Invoice number dropdown is selected
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns>List of invoices</returns>
        public List<clsInvoice> GetDistinctInvoiceNumbers()
        {
            try
            {
                lstInvoices = new List<clsInvoice>();
                DataSet ds;
                // Query db for invoices by specified number, date, cost
                ds = ExecuteSQLStatement(searchSQL.SelectDistinctInvoiceNumber(), ref numOfInvoices);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lstInvoices.Add(new clsInvoice(ds.Tables[0].Rows[i][0].ToString()));
                }

                return lstInvoices;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// GetDistinctInvoiceDates
        /// Used when Invoice date dropdown is selected only
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <returns>List of invoices</returns>
        public List<clsInvoice> GetDistinctInvoiceDates()
        {
            try
            {
                lstInvoices = new List<clsInvoice>();
                DataSet ds;
                // Query db for invoices by specified number, date, cost
                ds = ExecuteSQLStatement(searchSQL.SelectDistinctInvoiceDate(), ref numOfInvoices);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lstInvoices.Add(new clsInvoice("", ds.Tables[0].Rows[i][0].ToString()));
                }

                return lstInvoices;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetDistinctInvoiceCharges
        /// Used when the Total Cost dropdown list has been selected only
        /// </summary>
        /// <param name="invoiceCost"></param>
        /// <returns>List of invoices</returns>
        public List<clsInvoice> GetDistinctInvoiceCharges()
        {
            try
            {
                lstInvoices = new List<clsInvoice>();
                DataSet ds;
                // Query db for invoices by specified number, date, cost
                ds = ExecuteSQLStatement(searchSQL.SelectDistinctInvoiceCost(), ref numOfInvoices);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lstInvoices.Add(new clsInvoice("", "", ds.Tables[0].Rows[i][0].ToString()));
                }

                return lstInvoices;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetInvoices(InvoiceNumber, InvoiceDate, TotalCost) - returns List<clsInvoices>
        /// Used when all three dropdown lists have been selected
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="invoiceDate"></param>
        /// <param name="invoiceCost"></param>
        /// <returns>List of invoices</returns>
        public List<clsInvoice> GetInvoices(string invoiceNum, string invoiceDate, string invoiceCost)
        {
            try
            {
                lstInvoices = new List<clsInvoice>();
                DataSet ds;
                // Query db for invoices by specified number, date, cost
                ds = ExecuteSQLStatement(searchSQL.SelectInvoiceData(invoiceNum, invoiceDate, invoiceCost), ref numOfInvoices);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lstInvoices.Add(new clsInvoice(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString()));
                }

                return lstInvoices;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// GetAllInvoices() - returns List<clsInvoices>
        /// Used when nothing is filtered
        /// </summary>
        /// <returns>List of invoices</returns>
        public List<clsInvoice> GetAllInvoices()
        {
            try
            {
                lstInvoices = new List<clsInvoice>();
                DataSet ds;
                // Query db for all invoices and return # of rows assigned to numOfInvoices variable
                ds = ExecuteSQLStatement(searchSQL.SelectAllInvoices(), ref numOfInvoices);
                // For each query, create an Invoice object and add to a list of Invoices
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lstInvoices.Add(new clsInvoice(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString()));
                }

                return lstInvoices;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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


    }
}
