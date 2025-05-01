using MediatR;
using Ambev.DeveloperEvaluation.Domain.Entities;      
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, Guid>
    {
        private readonly ISaleRepository _saleRepository;

        public CreateSaleHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = new Sale
            {
                Id          = Guid.NewGuid(),
                SaleNumber  = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(), // Exemplo de número
                SaleDate    = DateTime.UtcNow,
                Client      = request.Client,
                Branch      = request.Branch,
                Items       = new List<SaleItem>()
            };

            foreach (var dto in request.Items)
            {
                if (dto.Quantity > 20)
                    throw new InvalidOperationException("Quantidade máxima por item é 20.");

                var subtotal = dto.Quantity * dto.UnitPrice;
                var discount = dto.Quantity >= 10
                    ? subtotal * 0.20m
                    : dto.Quantity >= 4
                        ? subtotal * 0.10m
                        : 0m;

                var item = new SaleItem
                {
                    Id         = Guid.NewGuid(),
                    ProductName= dto.ProductName,
                    Quantity   = dto.Quantity,
                    UnitPrice  = dto.UnitPrice,
                    Discount   = discount,
                    TotalValue = subtotal - discount
                };

                sale.Items.Add(item);
            }

            sale.TotalValue = sale.Items.Sum(i => i.TotalValue);

            await _saleRepository.CreateAsync(sale);
            return sale.Id;
        }
    }
}
