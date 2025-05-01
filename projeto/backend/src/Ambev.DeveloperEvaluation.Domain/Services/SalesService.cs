using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public class SalesService
    {
        public void ApplyDiscounts(Sale sale)
        {
            foreach (var item in sale.Items)
            {
                if (item.Quantity > 20)
                    throw new InvalidOperationException("Não é permitido vender mais de 20 itens idênticos.");

                if (item.Quantity >= 10)
                    item.Discount = item.Quantity * item.UnitPrice * 0.20m;
                else if (item.Quantity >= 4)
                    item.Discount = item.Quantity * item.UnitPrice * 0.10m;
                else
                    item.Discount = 0;

                 item.Total = (item.Quantity * item.UnitPrice) - item.Discount;
            }
        }
    }
}
