


using GroupProject.Main;
using System;
using System.Collections.Generic;
using System.Data;
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
        

        public DataSet invoice;

        public DataTable datatable = new DataTable();



        public wndItems()
        {

            InitializeComponent();
        }

        // the main window needs to be looking into this to know if anything has changed.
        // bool BHasItemsBeenChanged; // set to true when an item has been added/edited/deleted. Use by main window to know
        // if needs to refresh item list.
        // bool HasItemsBeenChanged; // property


        /// <summary>
        /// method for edit item 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editItemBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// method for adding items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addItemBtn_Click(object sender, RoutedEventArgs e)
        {

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
            this.Close();
           // mainWindow.ShowDialog();

          
        }
    }
}
