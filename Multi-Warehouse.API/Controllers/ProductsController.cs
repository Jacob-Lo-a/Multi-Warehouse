using Microsoft.AspNetCore.Mvc;
using Multi_Warehouse.Core.DTOs;
using Multi_Warehouse.Core.interfaces;

namespace Multi_Warehouse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IReportService _reportService;
        private readonly ISftpService _sftpService;

        public ProductsController(IProductService productService, IReportService reportService, ISftpService sftpService)
        {
            _productService = productService;
            _reportService = reportService;
            _sftpService = sftpService;
        }

        [HttpGet("with-stocks")]
        public async Task<IActionResult> GetProductsWithStocks()
        {
            var result = await _productService.GetProductsWithStocksAsync();
            return Ok(result);
        }

        [HttpPost("stock-in")]
        public async Task<IActionResult> StockIn([FromBody] StockInRequest request)
        {
            await _productService.StockInAsync(request);
            return Ok("入庫成功");
        }

        [HttpGet("export-inventory-report")]
        public async Task<IActionResult> ExportInventory()
        {
            var fileBytes = await _reportService.ExportInventoryReportAsync();

            return File(
                fileBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "InventoryReport.xlsx"
            );
        }

        [HttpPost("low-inventory-report")]
        public async Task<IActionResult> LowInventoryReport()
        {
            await _reportService.DailySalesReportAsync();

            return Ok(new { message = "報告已成功上傳" });
        }
    }
}
