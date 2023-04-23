using GroupProject.Main;
using System;
using System.Collections.Generic;
using System.Data;
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
      public class clsItemsLogic
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

         clsMainSQL sqlClass = new clsMainSQL();
         clsItemsSQL sqlItemClass = new clsItemsSQL();
         DataTable _dataTable = new DataTable();


        /// <summary>
        /// Connection string to the DB
        /// </summary>
        public string sConnectionString;
        public clsItemsLogic()
        {
            // Connect to the "mdb" database
            sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\Invoice.mdb";
            System.Diagnostics.Debug.WriteLine("test: " + Directory.GetCurrentDirectory());

        }
        public DataTable getItems()
        {
            int count = 0;
            clsMainSQL clsData = new clsMainSQL();
            var query = sqlClass.getItems();
            var dataset = clsData.ExecuteSQLStatement(query, ref count);
            return dataset.Tables[0];
        }


        public DataSet fillTable()
        {
            var count = 0;
            var query = sqlItemClass.getItemsDesc();
            var dataset = sqlClass.ExecuteSQLStatement(query, ref count);
            return dataset;
        }

        public string insertGame(string itemCode, string itemDesc, int Cost)
        {
            var query = sqlItemClass.InsertItemDesc(itemCode, itemDesc, Cost);
            var result = sqlClass.ExecuteNonQuery(query);
            return result.ToString();
        }

        public string DeleteGame(string sItemCode)
        {
            var query = sqlItemClass.DeleteItemDesc(sItemCode);
            var result = sqlClass.ExecuteNonQuery(query);
            return result.ToString();
        }
        public string DeleteLineGame(string sItemCode)
        {
            var query = sqlItemClass.DeleteLineItem(sItemCode);
            var result = sqlClass.ExecuteNonQuery(query);
            return result.ToString();
        }

        public string updateGame(string sItemCode, string sItemDesc, int sCost)
        {
            var query = sqlItemClass.UpdateItemDesc(sItemCode, sItemDesc, sCost);
            var result = sqlClass.ExecuteNonQuery(query);
            return result.ToString();
        }

        // public string updateGame();

        // public DataTable DataTable
        //{
        //    get { return _dataTable; }
        //    set { SetProperty(ref _dataTable, value); }
        // }

    }



}
