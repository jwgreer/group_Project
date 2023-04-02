using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Main
{
    class clsMainSQL
    {
        //- UPDATE Invoices SET TotalCost = 1200 WHERE InvoiceNum = 123
        /// <summary>
        /// SQL query will update invoice data and set total cost
        /// </summary>
        /// <param name="sInvoiceID"></param>
        /// <param name="sTotalCost"
        /// <returns></returns>
        public string UpdateInvoiceData(string sInvoiceID, string sTotalCost)
        {
            string sSQL = "Update * FROM Invoices SET TotalCost = " + sTotalCost + "WHERE InvoiceNum = " + sInvoiceID;

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
        public string InstertItems(string sInvoiceID, string sLineItemNum, string sItemCode)
        {
            string sSQL = "INSERT INTO LineItems(InvoiceNum, LineItemNum, ItemCode) Values("+sInvoiceID+", "+sLineItemNum+", "+sItemCode+")";
            
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
        public string SelectLineItems(string sLineItems)
        {
            string sSQL = "SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost FROM LineItems, ItemDesc Where LineItems.ItemCode = ItemDesc.ItemCode And LineItems.InvoiceNum =" + sLineItems;

            return sSQL;
        }

        //- DELETE FROM LineItems WHERE InvoiceNum = 5000
        /// <summary>
        /// This query will delete line items matching a invoice number 
        /// </summary>
        /// <param name="sInvoiceNum"></param>
        /// <returns></returns>
        public string DeleteLineItems(string sInvoiceNum)
        {
            string sSQL = "DELETE * FROM LineItems WHERE InvoiceNum =" + sInvoiceNum;

            return sSQL;
        }
    }
}
