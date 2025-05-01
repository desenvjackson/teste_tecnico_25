using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, Unit>
    {
        private readonly ISaleRepository _saleRepository;

        public DeleteSaleHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<Unit> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.SaleId);
            if (sale == null)
                throw new InvalidOperationException("Venda não encontrada.");

            await _saleRepository.DeleteAsync(request.SaleId);
            return Unit.Value;
        }
    }
}