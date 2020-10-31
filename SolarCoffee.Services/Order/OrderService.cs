using System.Runtime.Serialization;
using System.ComponentModel;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffee.Data;
using SolarCoffee.Data.Model;
using SolarCoffee.Services.Product;
using SolarCoffee.Services.Inventory;

namespace SolarCoffee.Services.Order
{
    public class OrderService : IOrderService
    {

        private readonly SolarDbContext context;
        private readonly ILogger<OrderService> logger;
        private readonly IProductService productService;
        private readonly IInventoryService inventoryService;

        public OrderService(SolarDbContext context, ILogger<OrderService> logger, IProductService productService, IInventoryService inventoryService)
        {
            this.context = context;
            this.logger = logger;
            this.productService = productService;
            this.inventoryService = inventoryService;
        }

        public ServiceResponse<bool> GenerateInvoiceForOrder(SalesOrder order)
        {
            logger.LogInformation($"generating new order");

            foreach (var item in order.SalesOrderItem)
            {
                item.Product = productService.GetProductById(item.Product.Id);
                item.Quantity = item.Quantity;

                var inventoryId = inventoryService.GetByProductId(item.Product.Id);

                inventoryService.UpdateUnitsAvalible(inventoryId.Id, -item.Quantity);
            }

            try
            {
                context.SalesOrders.Add(order);
                context.SaveChanges();

                return new ServiceResponse<bool>
                {
                    isSuccess = true,
                    Data = true,
                    Message = "Open order created",
                    Time = DateTime.UtcNow,
                };
            }
            catch (System.Exception e)
            {
                return new ServiceResponse<bool>
                {
                    isSuccess = false,
                    Data = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow,
                };
            }
        }

        public List<SalesOrder> GetOrders()
        {
            return context.SalesOrders
            .Include(order => order.Customer)
                .ThenInclude(customer => customer.PrimaryAddress)
            .Include(order => order.SalesOrderItem)
                .ThenInclude(item => item.Product)
            .ToList();
        }

        public ServiceResponse<bool> MarkFulfilled(int id)
        {
            var order = context.SalesOrders.Find(id);
            order.UpdateOn = DateTime.UtcNow;
            order.IsPaid = true;

            try
            {
                context.SalesOrders.Update(order);
                context.SaveChanges();

                return new ServiceResponse<bool>
                {
                    isSuccess = true,
                    Data = true,
                    Message = "Order closed",
                    Time = DateTime.UtcNow,
                };
            }
            catch (System.Exception e)
            {
                return new ServiceResponse<bool>
                {
                    isSuccess = false,
                    Data = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow,
                };
            }
        }
    }
}