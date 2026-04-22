using PharmacyInventory.API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharmacyInventory.UnitTesting.TestDataModels
{
    public static class MedicineTestFactory
    {
        public static Medicine Create(
            string fullName = "Paracetamol 500mg",
            int quantity = 10)
        {
            return new Medicine
            {
                FullName = fullName,
                Notes = "Test Notes",
                ExpiryDate = new DateTime(2026, 12, 31),
                Quantity = quantity,
                Price = 10.50m,
                Brand = "Cipla"
            };
        }
    }
}
