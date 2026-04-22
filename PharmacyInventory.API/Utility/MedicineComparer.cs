using PharmacyInventory.API.Models;

namespace PharmacyInventory.API.Utility
{
    public class MedicineComparer : IEqualityComparer<Medicine>
    {
        public bool Equals(Medicine x, Medicine y)
        {
            return string.Equals(x?.FullName, y?.FullName, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(Medicine obj)
        {
            return obj.FullName?.ToLower().GetHashCode() ?? 0;
        }
    }
}
