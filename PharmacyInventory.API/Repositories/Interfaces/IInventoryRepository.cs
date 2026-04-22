using PharmacyInventory.API.Models;

namespace PharmacyInventory.API.Repositories.Interfaces
{
    public interface IInventoryRepository
    {
        List<Medicine> GetAll();
        bool Save(List<Medicine> medicines);
    }
}
