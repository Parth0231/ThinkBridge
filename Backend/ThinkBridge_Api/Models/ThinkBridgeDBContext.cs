using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThinkBridge_Api.Models
{
    public class ThinkBridgeDBContext : DbContext
    {
        public ThinkBridgeDBContext(DbContextOptions<ThinkBridgeDBContext> options)
              : base(options)
        {
        }

        public DbSet<Inventory> Inventory { get; set; }
    }
}
