using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace GroupProject.Items
{

    /// <summary>
    /// Class for the business logic of Items
    /// Contains Methods for connecting DB
    /// Queries from DB using Item Code, Item Description and Item Cost from clsItemsSQL class
    /// Adding, Editing, deleting items
    /// Checking to see if item is already on a Invoice
    /// 
    /// </summary>
    class clsItemsLogic
    {
        //invoice class
        //clsInvoice class
        //list<items>


        /// create a method that Get all items then return List<clsItem>
        /// create a method for adding an Item(clsItem)- from the folder common
        /// create a method to edit item(clsItem clsOlditem, clsItem clsNewItem)- Passing in old item then updating with new Item
        /// create a method that delete item - pass in item from clsItem then delete the item
        /// create a method  that needs to have a check that item isnt
        /// already on a invoice. isItemOnInvoice(clsItem)
        /// 

        /// <summary>
        /// Connection string to the DB
        /// </summary>
        private string sConnectionString;
        clsItemsLogic()
        {
            // Connect to the "mdb" database
            sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\Invoice.mdb";
        }
    }


}
