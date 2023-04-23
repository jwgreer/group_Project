using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Common
{
    /// <summary>
    /// Invoice class
    /// An Invoice object can be created with these properties:
    /// Invoice Number, Invoice Date, Total Charge
    /// Each Invoice object will be added to the Invoice List
    /// </summary>
    public class clsInvoice
    {
        // Class properties
        /// <summary>
        /// Property for the Invoice number
        /// </summary>
        public string invoiceNum { get; set; }
        /// <summary>
        /// Property for the Invoice Date
        /// </summary>
        public string invoiceDate { get; set; }
        /// <summary>
        /// Property for the Invoice Total Cost
        /// </summary>
        public string invoiceCost { get; set; }

        // Item List

        /// <summary>
        /// Constructor with default values for num and date
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="invoiceDate"></param>
        /// <param name="invoiceCost"></param>
        public clsInvoice(string invoiceNum = "", string invoiceDate = "", string invoiceCost = "")
        {
            this.invoiceNum = invoiceNum;
            this.invoiceDate = invoiceDate;
            this.invoiceCost = invoiceCost;
        }
        /// <summary>
        /// Override of the ToString method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{invoiceNum}";
        }
    }
}
