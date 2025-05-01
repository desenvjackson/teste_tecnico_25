using Microsoft.AspNetCore.Mvc;
using MediatR;
using Serilog;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ISaleRepository _saleRepository;

        public SalesController(IMediator mediator, ISaleRepository saleRepository)
        {
            _mediator = mediator;
            _saleRepository = saleRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSaleCommand command)
        {
            var id = await _mediator.Send(command);
            Log.Information("Evento: VendaCriada - Venda {SaleId} criada com sucesso.", id);

            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var sales = await _saleRepository.GetAllAsync(cancellationToken); 
            return Ok(sales);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale == null)
            {
                return NotFound();
            }

            return Ok(sale);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSaleCommand command)
        {
            Log.Information("Evento: VendaModificada - Venda {SaleId} modificada com sucesso.", id);
            command.SaleId = id;
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Log.Information("Evento: VendaCancelada - Venda {SaleId} cancelada com sucesso.", id);
            await _mediator.Send(new DeleteSaleCommand(id));
            return NoContent();
        }
    }
}