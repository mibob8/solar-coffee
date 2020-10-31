using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SolarCoffee.Data;
using SolarCoffee.Services.Product;

namespace SolarCoffee.Services.Customer
{
    public class CustomerService : ICustomerService
    {

        private readonly SolarDbContext solarDb;

        public CustomerService(SolarDbContext solarDb)
        {
            this.solarDb = solarDb;
        }

        public ServiceResponse<Data.Model.Customer> CreateCustomer(Data.Model.Customer customer)
        {
            try
            {
                solarDb.Customers.Add(customer);
                solarDb.SaveChanges();

                return new ServiceResponse<Data.Model.Customer>
                {
                    isSuccess = true,
                    Message = "New customer added",
                    Time = DateTime.UtcNow,
                    Data = customer,

                };
            }
            catch (System.Exception e)
            {
                return new ServiceResponse<Data.Model.Customer>
                {
                    isSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow,
                    Data = customer,
                };



            }
        }

        public ServiceResponse<bool> DeleteCustomer(int id)
        {
            var customerToDelete = solarDb.Customers.Find(id);
            var now = DateTime.UtcNow;

            if (customerToDelete == null)
            {
                return new ServiceResponse<bool>
                {
                    Time = now,
                    Message = "Customer not found",
                    isSuccess = false,
                    Data = false,
                };
            }

            try
            {
                solarDb.Customers.Remove(customerToDelete);
                solarDb.SaveChanges();

                return new ServiceResponse<bool>
                {
                    Time = now,
                    Message = "Customer removed",
                    isSuccess = true,
                    Data = true,
                };

            }
            catch (System.Exception e)
            {
                return new ServiceResponse<bool>
                {
                    isSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow,
                    Data = false,
                };
            }

        }

        public List<Data.Model.Customer> GetAllCustomer()
        {
            return solarDb.Customers
            .Include(customer => customer.PrimaryAddress)
            .OrderBy(customer => customer.LastName)
            .ToList();
        }

        public Data.Model.Customer GetById(int id)
        {
           return solarDb.Customers.FirstOrDefault(x=>x.Id == id);
        }
    }
}