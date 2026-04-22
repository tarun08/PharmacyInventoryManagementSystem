using PharmacyInventory.API.Models;
using PharmacyInventory.API.Repositories.Interfaces;
using System.Text.Json;

namespace PharmacyInventory.API.Repositories.Implementations
{
    public class FileInventoryRepository : IInventoryRepository
    {
        private readonly string _path = Path.Join("Store", "medicines.json");

        public List<Medicine> GetAll()
        {
            using var reader = new StreamReader(_path);

            return JsonSerializer.Deserialize<List<Medicine>>(
                reader.ReadToEnd(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            ) ?? new List<Medicine>();
        }

        public bool Save(List<Medicine> medicines)
        {
            using var stream = new FileStream(_path, FileMode.Create);

            JsonSerializer.Serialize(stream, medicines, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            return true;
        }
    }
}
