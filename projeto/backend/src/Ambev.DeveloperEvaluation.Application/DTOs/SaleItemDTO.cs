using System;

namespace Ambev.DeveloperEvaluation.Application.DTOs
{
    public class SaleItemDTO
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty; 
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalValue { get; set; } 
    }
}
