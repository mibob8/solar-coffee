namespace SolarCoffee.Data.Model
{
    public class SalesOrderItem
    {
        public int Id { get; set; } 
        public int Quantity { get; set; }
        public Product Product{get;set;}
    }
}