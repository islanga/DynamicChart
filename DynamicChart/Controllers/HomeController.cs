using DynamicChart.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace DynamicChart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IConfiguration _configuration;
        HttpClient _httpClient = new HttpClient();

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var baseUrl = _configuration.GetSection("ApiUrl").Value + "/api/Sale";
            List<SaleData> saleData = new List<SaleData>();

            var response = _httpClient.GetAsync(baseUrl);
            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<SaleData>>();
                display.Wait();
                saleData = display.Result;
            }
            ViewBag.Data = JsonConvert.SerializeObject(saleData.Select(prop => new { label = prop.SaleMonth, y = prop.Sale }).ToList());

            return View(saleData);
        }

        public IActionResult AddSale()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSale(SaleData saleData)
        {
            if (saleData == null)
            {
                return NotFound();
            }

            string data = JsonConvert.SerializeObject(saleData);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(_configuration.GetSection("ApiUrl").Value + "/api/Sale", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}