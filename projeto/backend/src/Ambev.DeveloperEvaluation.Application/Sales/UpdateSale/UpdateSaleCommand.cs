using MediatR;
using Ambev.DeveloperEvaluation.Application.DTOs;     

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleCommand : IRequest<Unit>
    {
        public Guid SaleId { get; set; }
        public List<SaleItemDTO> Items { get; set; } = new();
    }
}
