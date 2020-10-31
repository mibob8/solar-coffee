using System.Data.Common;
using System.Collections.Generic;
using SolarCoffee.Services.Product;

namespace SolarCoffee.Services.Customer
{
    public interface ICustomerService
    {
        List<Data.Model.Customer> GetAllCustomer();
        ServiceResponse<Data.Model.Customer>  CreateCustomer(Data.Model.Customer customer); 
        ServiceResponse<bool> DeleteCustomer(int id);
        Data.Model.Customer GetById(int id);
    }
}