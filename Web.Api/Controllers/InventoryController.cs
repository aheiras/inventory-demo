using Application;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<Inventory>>> GetAllInventory()
        {
            var inventory = await _inventoryService.GetInventoryAsync();
            return Ok(inventory);
        }

        [HttpGet("ById/{id}")]
        public async Task<ActionResult<Inventory>> GetInventoryById(int id)
        {
            var inventory = await _inventoryService.GetInventoryByIdAsync(id);
            if (inventory == null) return NotFound();
            return Ok(inventory);
        }

        [HttpPut("Add/{id}/{qty}")]
        public async Task<IActionResult> Add(int id, int qty)
        {
            var inventory = await _inventoryService.GetInventoryByIdAsync(id);
            if (id != inventory.Id) return BadRequest();
            var result = await _inventoryService.Add(id, qty);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("Remove/{id}/{qty}")]
        public async Task<IActionResult> Remove(int id, int qty)
        {
            var inventory = await _inventoryService.GetInventoryByIdAsync(id);
            if (id != inventory.Id) return BadRequest();
            var result = await _inventoryService.Remove(id, qty);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            var result = await _inventoryService.DeleteInventoryAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
