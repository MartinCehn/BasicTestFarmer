using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BasicTestFarmer.Models;

namespace BasicTestFarmer.Viewmodel
{
    public class ProductRendition
    {
        public int productID { get; set; }
        public string ProductName { get; set; }
        public int? Price { get; set; }
        public int saleprice { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Supplier { get; internal set; }
        
    }
}