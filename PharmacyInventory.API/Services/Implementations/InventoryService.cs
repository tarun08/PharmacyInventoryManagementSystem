using PharmacyInventory.API.Models;
using PharmacyInventory.API.Services.Interfaces;
using System.Text.Json;

namespace PharmacyInventory.API.Services.Implementations
{
    public class InventoryService : IInventoryService
    {
        private static string JsonFilePath = Path.Join("Store", "medicines.json");

        List<Medicine> IInventoryService.GetAll(string? searchName, int limit, int offset)
        {
            List<Medicine>? medicines = GetAllMedicines();

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
            List<Medicine>? existingMedicines = GetAllMedicines();

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

            List<Medicine>? mergedList = medicineMap?.Values?.ToList();

            return UpdateMedicines(mergedList);
        }


        private List<Medicine>? GetAllMedicines()
        {
            using (StreamReader reader = new StreamReader(JsonFilePath))
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                string jsonString = reader.ReadToEnd();

                return JsonSerializer.Deserialize<List<Medicine>>(jsonString, options);
            }
        }

        private bool UpdateMedicines(List<Medicine>? medicinesToUpdate)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                using (FileStream stream = new FileStream(JsonFilePath, FileMode.Create))
                {
                    JsonSerializer.Serialize(stream, medicinesToUpdate, options);
                }
                return true;
            }
            catch( Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
