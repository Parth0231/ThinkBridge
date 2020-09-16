using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThinkBridge_Api.Models;
using ThinkBridge_Api.Repository;

namespace ThinkBridge_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        IInventoryRepository inventoryRepository;
        public InventoriesController(IInventoryRepository _inventoryRepository)
        {
            inventoryRepository = _inventoryRepository;
        }



        // GET: api/Inventories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventory>>> GetInventory()
        {
            try
            {
                var inventlst = await inventoryRepository.GetInventory();
                if (inventlst == null)
                {
                    return NotFound();
                }
                else { return Ok(inventlst); }


            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        // GET: api/Inventories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetInventory(long? id)
        {
            try
            {
                if (id == null) 
                {
                    return BadRequest();
                }
                var inventory = await inventoryRepository.GetInventory(id);

                if (inventory.Value == null)
                {
                    return NotFound();
                }

                return Ok(inventory);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT: api/Inventories/5
        [HttpPut]
        public async Task<IActionResult> PutInventory(Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await inventoryRepository.PutInventory(inventory);
                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }

        // POST: api/Inventories      
        [HttpPost]
        public async Task<ActionResult<Inventory>> PostInventory(Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var postId = await inventoryRepository.PostInventory(inventory);
                    if (postId != null)
                    {
                        return Ok(postId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }

        // DELETE: api/Inventories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Inventory>> DeleteInventory(long id)
        {
            try
            {
                var result = await inventoryRepository.DeleteInventory(id);
                if (result.Value == null)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
