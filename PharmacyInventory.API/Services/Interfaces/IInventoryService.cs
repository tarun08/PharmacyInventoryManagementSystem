using PharmacyInventory.API.Models;

namespace PharmacyInventory.API.Services.Interfaces
{
    public interface IInventoryService
    {
        List<Medicine> GetAll(string? searchName, int limit, int offset);

        public bool Add(List<Medicine> medicines);

    }
}
