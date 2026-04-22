using Moq;
using PharmacyInventory.API.Models;
using PharmacyInventory.API.Repositories.Interfaces;
using PharmacyInventory.API.Services.Implementations;
using PharmacyInventory.API.Services.Interfaces;
using PharmacyInventory.UnitTesting.TestDataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PharmacyInventory.UnitTesting.Services
{
    public class InventoryServiceTests
    {
        private readonly Mock<IInventoryRepository> _repoMock;
        private readonly IInventoryService _service;

        public InventoryServiceTests()
        {
            _repoMock = new Mock<IInventoryRepository>();
            _service = new InventoryService(_repoMock.Object);
        }

        [Fact]
        public void Medicine_Should_Have_All_Fields_Set()
        {
            var medicine = MedicineTestFactory.Create();

            Assert.NotEqual(Guid.Empty, medicine.Guid);

            Assert.Equal("Paracetamol 500mg", medicine.FullName);
            Assert.Equal("Test Notes", medicine.Notes);
            Assert.Equal(new DateTime(2026, 12, 31), medicine.ExpiryDate);
            Assert.Equal(10, medicine.Quantity);
            Assert.Equal(10.50m, medicine.Price);
            Assert.Equal("Cipla", medicine.Brand);
        }

        [Fact]
        public void GetAll_ShouldReturn_AllMedicines_WhenSearchIsNull()
        {
            _repoMock.Setup(r => r.GetAll()).Returns(new List<Medicine>
            {
                MedicineTestFactory.Create("Paracetamol", 10),
                MedicineTestFactory.Create("Ibuprofen", 5)
            });

            var result = _service.GetAll(string.Empty, 10, 0);

            Assert.Equal(2, result.Count);
        }


        [Fact]
        public void GetAll_ShouldFilter_BySearchName()
        {
            _repoMock.Setup(r => r.GetAll()).Returns(new List<Medicine>
            {
                MedicineTestFactory.Create("Paracetamol", 10),
                MedicineTestFactory.Create("Ibuprofen", 5)
            });

            var result = _service.GetAll("para", 10, 0);

            Assert.Single(result);
            Assert.Equal("Paracetamol", result[0].FullName);
        }

        [Fact]
        public void Add_ShouldIncreaseQuantity_WhenMedicineExists()
        {
            _repoMock.Setup(r => r.GetAll()).Returns(new List<Medicine>
            {
                MedicineTestFactory.Create("Paracetamol", 10)
            });

            _repoMock.Setup(r => r.Save(It.IsAny<List<Medicine>>()))
                     .Returns(true);

            var newMeds = new List<Medicine>
            {
                MedicineTestFactory.Create("Paracetamol", 5)
            };

            var result = _service.Add(newMeds);

            Assert.True(result);
        }

        [Fact]
        public void Add_ShouldAdd_NewMedicine()
        {
            _repoMock.Setup(r => r.GetAll()).Returns(new List<Medicine>());

            _repoMock.Setup(r => r.Save(It.IsAny<List<Medicine>>()))
                     .Returns(true);

            var newMeds = new List<Medicine>
            {
                MedicineTestFactory.Create("Ibuprofen", 8)
            };

            var result = _service.Add(newMeds);

            Assert.True(result);
        }
    }
}
