using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SolarCoffee.Data.Model;

namespace SolarCoffee.Data
{
    public class SolarDbContext : IdentityDbContext
    {
        public SolarDbContext(DbContextOptions options) : base(options)
        {
        }

        protected SolarDbContext()
        {
        }

        public virtual DbSet<Customer> Customers{get;set;} 

    }
}