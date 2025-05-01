using MediatR;
using Ambev.DeveloperEvaluation.Application.DTOs;   

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleQuery : IRequest<SaleDTO>
    {
        public Guid SaleId { get; set; }
    }
}
