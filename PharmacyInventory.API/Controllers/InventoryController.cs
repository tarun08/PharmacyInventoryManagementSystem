using Microsoft.AspNetCore.Mvc;
using PharmacyInventory.API.Models;
using PharmacyInventory.API.Services.Interfaces;

namespace PharmacyInventory.API.Controllers
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

        [HttpGet]
        public ActionResult<List<Medicine>> Get([FromQuery] string? searchName, [FromQuery] int offset = 0, [FromQuery] int limit = 10)
        {
            try
            {
                var medicines = _inventoryService.GetAll(searchName, limit, offset);
        
                if (medicines == null || !medicines.Any())
                {
                    return NotFound("No medicines found.");
                }
        
                return Ok(medicines);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving medicines.");
            }
        }

        [HttpPost]
        public ActionResult<bool> Post([FromBody] List<Medicine> medicines)
        {
            if (medicines == null || !medicines.Any())
            {
                return BadRequest("Medicine list cannot be empty.");
            }
            
            try
            {
                bool success = _inventoryService.Add(medicines);
        
                if (success)
                {
                    return Ok(true);
                }        
                else
                {
                    return StatusCode(500, "Failed to add medicines.");
                }                
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
