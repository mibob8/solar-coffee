using System;
using System.Collections.Generic;

namespace SolarCoffee.Data.Model
{
    public class SalesOrder
    {
        public int Id { get; set; }
        public DateTime UpdateOn { get; set; }
        public DateTime CreateOn { get; set; }
        public Customer Customer { get; set; }
        public List<SalesOrderItem> SalesOrderItem{get;set;}
        public bool IsPaid{get;set;}
    }
}