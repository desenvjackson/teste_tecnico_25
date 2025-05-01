using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public static class SalesEventLogger
    {
        public static void Log(string eventType, Sale sale)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {eventType}: Venda {sale.Id} no valor de {sale.Total:C}");
        }
    }
}
