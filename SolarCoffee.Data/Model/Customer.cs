using System;
namespace SolarCoffee.Data.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public DateTime UpdateOn { get; set; }
        public DateTime CreateOn { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public CustomerAddress Adress { get; set; }
    }
}