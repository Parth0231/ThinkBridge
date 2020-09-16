using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using ThinkBridge_Api.Models;
namespace ThinkBridge_unitTest
{
    public class DummyDataDBInitializer
    {
        public DummyDataDBInitializer()
        {
        }

        public void Seed(ThinkBridgeDBContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Inventory.AddRange(
                new Inventory() { Name = "candy", Description = "candycandycandy", Price= 10,Image=null },
                new Inventory() { Name = "biscuit", Description = "biscuitbiscuit",Price = 20, Image = null },
                new Inventory() { Name = "toolkit", Description = "toolkittoolkit", Price = 30, Image = null },
                new Inventory() { Name = "tape", Description = "tapetapetapetapetape", Price = 40, Image = null }
            );           
            context.SaveChanges();
        }
    }
}
