namespace PharmacyInventory.API.Models
{
    public class Medicine
    {
        public Medicine()
        {
            Guid = Guid.NewGuid();
        }
        public Guid Guid { get; set; } 
        public required string FullName { get; set; }    
        public string? Notes { get; set; }      
        public required DateTime ExpiryDate { get; set; }
        public required int Quantity { get; set; }      
        public required decimal Price { get; set; }     
        public required string Brand { get; set; } 
    }
}
