using MediatR;
using Ambev.DeveloperEvaluation.Application.DTOs;    
using Ambev.DeveloperEvaluation.Domain.Repositories;  

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleHandler : IRequestHandler<GetSaleQuery, SaleDTO>
    {
        private readonly ISaleRepository _saleRepository;

        public GetSaleHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<SaleDTO> Handle(GetSaleQuery request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.SaleId);
            if (sale == null)
                throw new InvalidOperationException("Venda não encontrada.");

            return new SaleDTO
            {
                SaleId    = sale.Id,
                Number    = sale.SaleNumber,
                SaleDate  = sale.SaleDate,
                Client    = sale.Client,
                Branch    = sale.Branch,
                Total     = sale.TotalValue,
                Items     = sale.Items.Select(i => new SaleItemDTO
                {
                    ProductName = i.ProductName,
                    Quantity    = i.Quantity,
                    UnitPrice   = i.UnitPrice,
                    Discount    = i.Discount,
                    TotalValue  = i.TotalValue
                }).ToList()
            };
        }
    }
}
