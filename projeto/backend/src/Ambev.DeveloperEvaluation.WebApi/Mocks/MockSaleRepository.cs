using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.WebApi.Mocks
{
    public class MockSaleRepository : ISaleRepository
    {
        private readonly List<Sale> _sales = new();

        public Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            sale.Id = Guid.NewGuid();
            _sales.Add(sale);
            return Task.FromResult(sale);
        }

        public Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sale = _sales.FirstOrDefault(s => s.Id == id);
            return Task.FromResult(sale);
        }

        public Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_sales.AsEnumerable());
        }

        public Task<Sale> UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            var existingSale = _sales.FirstOrDefault(s => s.Id == sale.Id);
            if (existingSale != null)
            {
                _sales.Remove(existingSale);
                _sales.Add(sale);
            }
            return Task.FromResult(sale);
        }

        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sale = _sales.FirstOrDefault(s => s.Id == id);
            if (sale != null)
            {
                _sales.Remove(sale);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}