using System;
using System.ComponentModel.DataAnnotations;

namespace SolarCoffee.Web.ViewModels
{
    public class ProductModel
    {
        public int Id { get; set; }
        public DateTime UpdateOn { get; set; }
        public DateTime CreateOn { get; set; }
        
        [MaxLength(64)]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsTaxable { get; set; }
        public bool IsArchived { get; set; }


    }
}