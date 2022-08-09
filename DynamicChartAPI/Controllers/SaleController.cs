using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DynamicChartAPI.Models;
using DynamicChartAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace DynamicChartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        protected readonly DynamicChartDbContext _context;

        public SaleController(DynamicChartDbContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Sales>> Get() => await _context.sales.FromSqlRaw("GetSales").ToListAsync();

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Sales), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var sale = await _context.sales.FromSqlRaw($"GetSaleById {id}").ToListAsync();
            return sale == null ? NotFound() : Ok(sale);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(Sales sale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _context.Database.ExecuteSqlRawAsync("SaveSale @p0, @p1", parameters: new Object[] { sale.SaleMonth, sale.Sale });

            return CreatedAtAction(nameof(Get), new { id = sale.SaleID }, sale);
        }
    }
}
