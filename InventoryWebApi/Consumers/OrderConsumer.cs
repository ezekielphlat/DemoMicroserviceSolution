using InventoryWebApi.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using RMQEvent.Contract;

namespace InventoryWebApi.Consumers
{
    public class OrderConsumer : IConsumer<RMQOrder>
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public OrderConsumer(InventoryDbContext inventoryDbContext)
        {
            this._inventoryDbContext = inventoryDbContext;
        }
        public async Task Consume(ConsumeContext<RMQOrder> context)
        {
            Console.WriteLine("Order don come");
            var productInventories = new Inventory();
            productInventories.ProductId = 1;
            productInventories.ProductName = "Just a product Name" ;
            productInventories.TotalStock = 100;
            productInventories.RemainingStock = productInventories.TotalStock - 1 ;
            productInventories.LastModified = DateTime.UtcNow;
            await _inventoryDbContext.AddAsync(productInventories);
            await  _inventoryDbContext.SaveChangesAsync();

            return;
            
            
        }
    }
}
