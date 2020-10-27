using System;

namespace SolarCoffee.Data.Model
{
    public class ProductInventory
    {
        public int Id { get; set; }
        public DateTime UpdateOn { get; set; }
        public DateTime CreateOn { get; set; }
        public int QuantityOnHand { get; set; }
        public int IdealQuantity { get; set; }
        public Product Product { get; set; }
    }
}