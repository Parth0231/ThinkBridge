using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThinkBridge_Api.Models;

namespace ThinkBridge_Api.Repository
{
    public interface IInventoryRepository
    {
        Task<ActionResult<IEnumerable<Inventory>>> GetInventory();

        Task<ActionResult<Inventory>> GetInventory(long? id);

        Task<ActionResult<Inventory>> PostInventory(Inventory inventory);

        Task<ActionResult<Inventory>> DeleteInventory(long id);

        Task PutInventory(Inventory inventory);
    }
}
