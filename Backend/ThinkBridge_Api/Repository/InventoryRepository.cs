using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using ThinkBridge_Api.Models;

namespace ThinkBridge_Api.Repository
{
    public class InventoryRepository:IInventoryRepository
    {
        ThinkBridgeDBContext db;
        public InventoryRepository(ThinkBridgeDBContext _db)
        {
            db = _db;
        }

        public async Task<ActionResult<IEnumerable<Inventory>>> GetInventory()
        {
            if (db != null)
            {
                return await db.Inventory.ToListAsync();
            }

            return null;
        }



        public async Task<ActionResult<Inventory>> GetInventory(long? id)
        {
            return await db.Inventory.Where(w => w.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ActionResult<Inventory>> PostInventory(Inventory inventory)
        {
            if (db != null)
            {
                await db.Inventory.AddAsync(inventory);
                await db.SaveChangesAsync();             
            }

            return inventory;
        }

        public async Task<ActionResult<Inventory>> DeleteInventory(long id)
        {
            int result = 0;
            Inventory resp = null;
            if (db != null)
            {
                //Find the post for specific post id
                resp = await db.Inventory.FirstOrDefaultAsync(x => x.Id == id);

                if (resp != null)
                {
                    //Delete that post
                    db.Inventory.Remove(resp);

                    //Commit the transaction
                    result = await db.SaveChangesAsync();
                    return resp;
                }
                else { throw new Exception("bad request"); }
              
            }

            return resp;
        }


        public async Task PutInventory(Inventory inventory)
        {
            //Delete that post
            db.Inventory.Update(inventory);

            //Commit the transaction
            await db.SaveChangesAsync();

        }
      
    }
}
