using System;
using System.ComponentModel.DataAnnotations;

namespace SolarCoffee.Data.Model
{
    public class CustomerAddress
    {
        public int Id { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime UpdateOn { get; set; }

        [MaxLength(100)]
        public string AddressLine1 { get; set; }
        [MaxLength(100)]
        public string AddressLine2 { get; set; }
        [MaxLength(100)]
        public string City { get; set; }
        [MaxLength(2)]
        public string State { get; set; }
        [MaxLength(10)]
        public string PostalCode { get; set; }
        [MaxLength(32)]
        public string Country { get; set; }
    }
}