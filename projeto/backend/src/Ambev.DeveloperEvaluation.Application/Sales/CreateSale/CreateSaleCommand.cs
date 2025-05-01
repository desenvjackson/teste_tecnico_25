using MediatR;
using Ambev.DeveloperEvaluation.Application.DTOs;  

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommand : IRequest<Guid>
    {
        public string Client { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;
        public List<SaleItemDTO> Items { get; set; } = new();
    }
}
