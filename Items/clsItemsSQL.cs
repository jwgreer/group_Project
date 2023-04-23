using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Items
{

    /// <summary>
    /// SQL class for the Item window
    /// Different methods for grabbing information about items
    /// each method will return a SQL string.
    /// </summary>
     class clsItemsSQL
    {

        /// <summary>
        /// Method that Selects all item info from Item Description
        /// </summary>
        /// <returns></returns>
        public string SelectItemCode()
        {
            string sSQL = "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc";
            return sSQL;
        }

        /// <summary>
        /// Method that selects the invoice number where it matches the item code
        /// </summary>
        /// <param name="sItemCode">variable for Item Code</param>
        /// <returns></returns>
        public string SelectInvoiceNum(string sItemCode)
        {
            string sSQL = "SELECT distinct(InvoiceNum) FROM LineItems WHERE ItemCode = " + sItemCode;
            return sSQL;
        }

        /// <summary>
        /// Method that updates the items description 
        /// </summary>
        /// <param name="sItemCode">variable for Item Code</param>
        /// <param name="sItemDesc">variable for item Desc</param>
        /// <param name="sCost">variable for item Cost</param>
        /// <returns></returns>
        public string UpdateItemDesc(string sItemCode, string sItemDesc, int sCost)
        {
            string sSQL = "UPDATE ItemDesc SET ItemDesc ='" + sItemDesc + "', Cost ='"+ sCost + "' WHERE ItemCode = '" +sItemCode+"'";
            return sSQL;
        }

        /// <summary>
        /// method to insert item into item Description
        /// </summary>
        /// <param name="sItemCode">Variable for the item Code</param>
        /// <param name="sItemDesc">variable for Item Desc</param>
        /// <param name="sCost">variable for the cost</param>
        /// <returns></returns>
        public string InsertItemDesc(string ItemCode, string sItemDesc, int sCost)
        {
            string sSQL = "INSERT INTO ItemDesc(ItemCode, ItemDesc, Cost) Values('" + ItemCode+"', '" + sItemDesc + "', '" + sCost + "')";
            return sSQL;
        }

        public string getItemsDesc()
        {
            return "Select * From ItemDesc";
        }

        /// <summary>
        /// method to delete the item where the Item Code matches
        /// </summary>
        /// <param name="sItemCode">Variable for the item Code</param>
        /// <returns></returns>
        public string DeleteItemDesc(string sItemCode)
        {
            string sSQL = "Delete FROM ItemDesc WHERE ItemCode = '" + sItemCode + "'";
            return sSQL;
        }


        public string DeleteLineItem(string sItemCode)
        {
            string sSQL = "Delete FROM LineItems WHERE ItemCode = '" + sItemCode + "'";
            return sSQL;
        }
        ///- select ItemCode, ItemDesc, Cost from ItemDesc
        ///- select distinct(InvoiceNum) from LineItems where ItemCode = 'A'
        ///- Update ItemDesc Set ItemDesc = 'abcdef', Cost = 123 where ItemCode = 'A'
        ///- Insert into ItemDesc(ItemCode, ItemDesc, Cost) Values('ABC', 'blah', 321)
        ///- Delete from ItemDesc Where ItemCode = 'ABC'
        ///

    }
}
