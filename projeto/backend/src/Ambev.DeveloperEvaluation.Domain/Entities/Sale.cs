namespace Ambev.DeveloperEvaluation.Domain.Entities
{
     public class Sale
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid BranchId { get; set; }
        public DateTime SaleDate { get; set; }  
        public string Client { get; set; } = string.Empty;  
        public string Branch { get; set; } = string.Empty;  
        public decimal TotalValue { get; set; }
        public decimal Total { get; set; }   
        public string SaleNumber { get; set; } = string.Empty; 
        public bool IsCanceled { get; set; }

        public List<SaleItem> Items { get; set; } = new();
    }
}
