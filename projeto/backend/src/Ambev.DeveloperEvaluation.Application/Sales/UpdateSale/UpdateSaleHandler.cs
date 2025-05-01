using MediatR;
using Ambev.DeveloperEvaluation.Application.DTOs;      
using Ambev.DeveloperEvaluation.Domain.Entities;      
using Ambev.DeveloperEvaluation.Domain.Repositories;   

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, Unit>
    {
        private readonly ISaleRepository _saleRepository;

        public UpdateSaleHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<Unit> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.SaleId);
            if (sale == null)
                throw new InvalidOperationException("Venda não encontrada.");

            // Limpa itens existentes
            sale.Items.Clear();

            // Recria itens atualizados
            foreach (var dto in request.Items)
            {
                var subtotal = dto.Quantity * dto.UnitPrice;
                if (dto.Quantity > 20)
                    throw new InvalidOperationException("Não é permitido vender mais de 20 itens idênticos.");

                var discount = dto.Quantity >= 10
                    ? subtotal * 0.20m
                    : dto.Quantity >= 4
                        ? subtotal * 0.10m
                        : 0m;

                sale.Items.Add(new SaleItem
                {
                    Id         = Guid.NewGuid(),
                    SaleId     = sale.Id,
                    ProductName= dto.ProductName,
                    Quantity   = dto.Quantity,
                    UnitPrice  = dto.UnitPrice,
                    Discount   = discount,
                    TotalValue = subtotal - discount
                });
            }

            // Atualiza valor total da venda
            sale.TotalValue = sale.Items.Sum(i => i.TotalValue);

            await _saleRepository.UpdateAsync(sale);
            return Unit.Value;
        }
    }
}
