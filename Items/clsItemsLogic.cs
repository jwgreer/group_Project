using GroupProject.Main;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
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

        ///Creates objects
         clsMainSQL sqlClass = new clsMainSQL();
         clsItemsSQL sqlItemClass = new clsItemsSQL();
         DataTable _dataTable = new DataTable();


        /// <summary>
        /// Connection string to the DB
        /// </summary>
        public string sConnectionString;
        public clsItemsLogic()
        {
            try
            {
                // Connect to the "mdb" database
                sConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data source= " + Directory.GetCurrentDirectory() + "\\Invoice.mdb";
                System.Diagnostics.Debug.WriteLine("test: " + Directory.GetCurrentDirectory());
            }
            catch (Exception ex)
            {
                //error handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + " " +
                    MethodInfo.GetCurrentMethod().Name + " ->" + ex.Message);
            }
        }

        /// <summary>
        /// method that gets items from the DPS
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public DataTable getItems()
        {
            try
                int count = 0;
                clsMainSQL clsData = new clsMainSQL();
                var query = sqlClass.getItems();
                var dataset = clsData.ExecuteSQLStatement(query, ref count);
                return dataset.Tables[0];
            }
            catch (Exception ex)
            {
                //error handling
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + " " +
                    MethodInfo.GetCurrentMethod().Name + " ->" + ex.Message);
            }
        }

        /// <summary>
        /// method that fills the table
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public DataSet fillTable()
        {
            try
            {
                var count = 0;
                var query = sqlItemClass.getItemsDesc();
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
        /// method that passes ItemCode, itemDesc, Cost to SQL for SQL Statement
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="itemDesc"></param>
        /// <param name="Cost"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string insertGame(string itemCode, string itemDesc, int Cost)
        {
            try
            {
                var query = sqlItemClass.InsertItemDesc(itemCode, itemDesc, Cost);
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
        /// method that passes Item code to SQL to delete from the database
        /// </summary>
        /// <param name="sItemCode"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string DeleteGame(string sItemCode)
        {
            try
            {
                var query = sqlItemClass.DeleteItemDesc(sItemCode);
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
        /// deletes the line number so we can delete the game after
        /// </summary>
        /// <param name="sItemCode"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string DeleteLineGame(string sItemCode)
        {
            try
            {
                var query = sqlItemClass.DeleteLineItem(sItemCode);
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
        /// Passses params to update the game from a SQL statement
        /// </summary>
        /// <param name="sItemCode"></param>
        /// <param name="sItemDesc"></param>
        /// <param name="sCost"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string updateGame(string sItemCode, string sItemDesc, int sCost)
        {
            try
            {
                var query = sqlItemClass.UpdateItemDesc(sItemCode, sItemDesc, sCost);
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

        // public string updateGame();

        // public DataTable DataTable
        //{
        //    get { return _dataTable; }
        //    set { SetProperty(ref _dataTable, value); }
        // }

    }



}
