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
            return _inventoryService.GetAll(searchName, limit, offset);
        }

        [HttpPost]
        public ActionResult<bool> Post([FromBody] List<Medicine> medicine)
        {
            return _inventoryService.Add(medicine);
        }
    }
}
