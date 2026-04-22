using PharmacyInventory.API.Models;
using PharmacyInventory.API.Repositories.Interfaces;
using PharmacyInventory.API.Services.Interfaces;
using System.Text.Json;

namespace PharmacyInventory.API.Services.Implementations
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        List<Medicine> IInventoryService.GetAll(string? searchName, int limit, int offset)
        {
            List<Medicine>? medicines = _inventoryRepository.GetAll();

            medicines = medicines?
                            .Where(x => string.IsNullOrEmpty(searchName) ||
                                        (!string.IsNullOrWhiteSpace(x.FullName) &&
                                         x.FullName.Contains(searchName.Trim(), StringComparison.OrdinalIgnoreCase)))
                            .Skip(offset * limit)
                            .Take(limit).ToList();

            return medicines ?? new List<Medicine>();
        }

        public bool Add(List<Medicine> medicinesToAdd)
        {
            List<Medicine>? existingMedicines = _inventoryRepository.GetAll();

            Dictionary<string, Medicine>? medicineMap = existingMedicines?
                                .Where(x => x.FullName != null)
                                .ToDictionary(
                                    x => x.FullName.ToLower(),
                                    x => x
                                );

            if(medicineMap != null)
            {
                foreach (var newMed in medicinesToAdd)
                {
                    if (newMed.FullName == null) continue;

                    var key = newMed.FullName.ToLower();

                    if (medicineMap.ContainsKey(key))
                    {
                        medicineMap[key].Quantity += newMed.Quantity;
                    }
                    else
                    {
                        medicineMap[key] = newMed;
                    }
                }
            }

            List<Medicine> mergedList = medicineMap == null ? new List<Medicine>() : medicineMap.Values.ToList();

            return _inventoryRepository.Save(mergedList);
        }
    }
}
