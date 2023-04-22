


using GroupProject.Main;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GroupProject.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        //wndMain mainWindow = new wndMain();

        // clsItemsLogic clsLogic = new clsItemsLogic();\

        clsItemsLogic itemLogic = new clsItemsLogic();
        
        //wndItems itemsWindow = new wndItems();

        clsMainSQL sqlClass = new clsMainSQL();

        clsMainLogic mainClass = new clsMainLogic();

        public DataSet invoice;

        public DataTable datatable = new DataTable();

        public string rCode;
        public string rItemDesc;
        public int rCost;



        public wndItems()
        {
            // updateDataGrid();
            
            InitializeComponent();
            updateDataGrid();
        }

        // the main window needs to be looking into this to know if anything has changed.
        // bool BHasItemsBeenChanged; // set to true when an item has been added/edited/deleted. Use by main window to know
        // if needs to refresh item list.
        // bool HasItemsBeenChanged; // property

        /* private void updateDataGrid()
         {
             DataTable list = mainClass.getItems();

             for (int i = 0; i < list.Rows.Count; i++)
             {
                 gameDataGrid.Items.Add(list.Rows[i][1]);
             }
         }*/

        private void updateDataGrid()
        {
            try
            {
                if (gameDataGrid != null)
                {
                    var datSet = itemLogic.fillTable();
                    gameDataGrid.ItemsSource = datSet.Tables[0].DefaultView;
                }
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.Message); }
        }

        /// <summary>
        /// method for edit item 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editItemBtn_Click(object sender, RoutedEventArgs e)
        {
           // DataRowView drv = gameDataGrid.CurrentRow.DataBoundItem as DataRowView;
            //DataRow[] rowsToUpdate = new DataRow[] { drv.Row };
            //gameDataGrid.Rows[0].Selected = true;
            
        }       

        /// <summary>
        /// method for adding items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addItemBtn_Click(object sender, RoutedEventArgs e)
        {
            // gameDataGrid.Rows.Add()
            if (costTextBox != null && descTextBox !=null && codeTextBox != null)
            {
                
                
                
                // List<string> game = new List<string>();
                // DataGrid.Items.Add
                rCode = codeTextBox.Text;
                rCost = int.Parse(costTextBox.Text);
                rItemDesc = descTextBox.Text;

                insertItem();
                //game.Add(rCode, )
            }
            else
            {
                MessageBox.Show("Please fill in the data to enter.");
            }
        }
        private void insertItem()
        {
            var result = itemLogic.insertGame(rCode,rItemDesc, rCost);
            MessageBox.Show(result);
        }

        /// <summary>
        /// method for deleting items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteItemBtn_Click(object sender, RoutedEventArgs e)
        {

        }


        /// <summary>
        /// method for saving items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveItemBtn_Click(object sender, RoutedEventArgs e)
        {
          

        }

        private void mainReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
           // mainWindow.ShowDialog();

          
        }

        


        private void mainform_Load(object sender, RoutedEventArgs e)
        {

        }
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                //Would write to a file or database here.
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine +
                                             "HandleError Exception: " + ex.Message);
            }
        }

    }
}
