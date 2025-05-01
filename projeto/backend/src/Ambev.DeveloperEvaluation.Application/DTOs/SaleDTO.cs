using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.DTOs
{
    public class SaleDTO
    {
        public Guid SaleId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid BranchId { get; set; }
        public DateTime SaleDate { get; set; }
        public string Client { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;
        public decimal TotalValue { get; set; }
        public string Number { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public List<SaleItemDTO> Items { get; set; } = new();
    }
}
