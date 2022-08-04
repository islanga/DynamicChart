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
    }
}
