using PharmacyInventory.API.Models;
using PharmacyInventory.API.Services.Interfaces;
using System.Text.Json;

namespace PharmacyInventory.API.Services.Implementations
{
    public class InventoryService : IInventoryService
    {
        private static string JsonFilePath = Path.Join("Store", "medicines.json");
        bool IInventoryService.Add(List<Medicine> medicines)
        {
            throw new NotImplementedException();
        }

        List<Medicine> IInventoryService.GetAll(string? searchName, int limit, int offset)
        {
            List<Medicine>? medicines;

            using (StreamReader reader = new StreamReader(JsonFilePath))
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                string jsonString = reader.ReadToEnd();

                medicines = JsonSerializer.Deserialize<List<Medicine>>(jsonString, options);

                medicines = medicines?
                            .Where(x => string.IsNullOrEmpty(searchName) ||
                                        ( !string.IsNullOrWhiteSpace(x.FullName) &&
                                         x.FullName.Contains(searchName.Trim(), StringComparison.OrdinalIgnoreCase)))
                            .Skip(offset * limit)
                            .Take(limit).ToList();
            }

            return medicines ?? new List<Medicine>();
        }
    }
}
