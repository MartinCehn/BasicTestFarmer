namespace BasicTestFarmer.Models
{
    public class shoplist
    {
        public int ShoppingID { get; set; }
        public int? ProductID { get; set; }
        public int Price { get; set; }
        public int TrueQuantity { get; set; }
        public int? Quantity { get; set; }
        public int? total { get; set; }
        public string ProductName { get; set; }
    }
}