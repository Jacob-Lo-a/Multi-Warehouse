using Multi_Warehouse.Core.interfaces;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Multi_Warehouse.API.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _repo;
        private readonly ISftpService _sftpService;

        public ReportService(IReportRepository repo, ISftpService sftpService)
        {
            _repo = repo;
            _sftpService = sftpService;
        }

        public async Task<byte[]> ExportInventoryReportAsync()
        {
            var data = await _repo.GetInventoryReportAsync();

            using var workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet("庫存總表");

            //  樣式
            var headerStyle = workbook.CreateCellStyle();
            var font = workbook.CreateFont();
            font.IsBold = true;
            headerStyle.SetFont(font);
            headerStyle.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            headerStyle.FillPattern = FillPattern.SolidForeground;

            var numberStyle = workbook.CreateCellStyle();
            numberStyle.Alignment = HorizontalAlignment.Right;

            //  取得所有倉庫
            var allWarehouses = data
                .SelectMany(p => p.WarehouseStocks)
                .Select(w => w.WarehouseName)
                .Distinct()
                .ToList();

      
            var header = sheet.CreateRow(0);
            int col = 0;

            header.CreateCell(col).SetCellValue("產品名稱");
            header.GetCell(col++).CellStyle = headerStyle;

            header.CreateCell(col).SetCellValue("類別");
            header.GetCell(col++).CellStyle = headerStyle;

            header.CreateCell(col).SetCellValue("供應商");
            header.GetCell(col++).CellStyle = headerStyle;

            foreach (var wh in allWarehouses)
            {
                header.CreateCell(col).SetCellValue(wh);
                header.GetCell(col++).CellStyle = headerStyle;
            }

            header.CreateCell(col).SetCellValue("總庫存");
            header.GetCell(col).CellStyle = headerStyle;

         
            int rowIndex = 1;

            foreach (var item in data)
            {
                var row = sheet.CreateRow(rowIndex++);
                col = 0;

                row.CreateCell(col++).SetCellValue(item.ProductName);
                row.CreateCell(col++).SetCellValue(item.CategoryName);
                row.CreateCell(col++).SetCellValue(item.SupplierName);

                // 倉庫數量
                foreach (var wh in allWarehouses)
                {
                    var stock = item.WarehouseStocks
                        .FirstOrDefault(x => x.WarehouseName == wh);

                    int qty = stock?.Quantity ?? 0;

                    var cell = row.CreateCell(col++);
                    cell.SetCellValue(qty);
                    cell.CellStyle = numberStyle;
                }

                var totalCell = row.CreateCell(col);
                totalCell.SetCellValue(item.TotalQuantity);
                totalCell.CellStyle = numberStyle;
            }
        
            using var stream = new MemoryStream();
            workbook.Write(stream);

            return stream.ToArray();
        }
        public async Task DailySalesReportAsync()
        {
            var data = await _repo.GetLowInventoryReportAsync();

            using var workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet("庫存總表");

            //  樣式
            var headerStyle = workbook.CreateCellStyle();
            var font = workbook.CreateFont();
            font.IsBold = true;
            headerStyle.SetFont(font);
            headerStyle.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            headerStyle.FillPattern = FillPattern.SolidForeground;

            var numberStyle = workbook.CreateCellStyle();
            numberStyle.Alignment = HorizontalAlignment.Right;

            //  取得所有倉庫
            var allWarehouses = data
                .SelectMany(p => p.WarehouseStocks)
                .Select(w => w.WarehouseName)
                .Distinct()
                .ToList();


            var header = sheet.CreateRow(0);
            int col = 0;

            header.CreateCell(col).SetCellValue("產品名稱");
            header.GetCell(col++).CellStyle = headerStyle;

            header.CreateCell(col).SetCellValue("類別");
            header.GetCell(col++).CellStyle = headerStyle;

            header.CreateCell(col).SetCellValue("供應商");
            header.GetCell(col++).CellStyle = headerStyle;

            foreach (var wh in allWarehouses)
            {
                header.CreateCell(col).SetCellValue(wh);
                header.GetCell(col++).CellStyle = headerStyle;
            }

            header.CreateCell(col).SetCellValue("總庫存");
            header.GetCell(col).CellStyle = headerStyle;


            int rowIndex = 1;

            foreach (var item in data)
            {
                var row = sheet.CreateRow(rowIndex++);
                col = 0;

                row.CreateCell(col++).SetCellValue(item.ProductName);
                row.CreateCell(col++).SetCellValue(item.CategoryName);
                row.CreateCell(col++).SetCellValue(item.SupplierName);

                // 倉庫數量
                foreach (var wh in allWarehouses)
                {
                    var stock = item.WarehouseStocks
                        .FirstOrDefault(x => x.WarehouseName == wh);

                    int qty = stock?.Quantity ?? 0;

                    var cell = row.CreateCell(col++);
                    cell.SetCellValue(qty);
                    cell.CellStyle = numberStyle;
                }

                var totalCell = row.CreateCell(col);
                totalCell.SetCellValue(item.TotalQuantity);
                totalCell.CellStyle = numberStyle;
            }

            using var stream = new MemoryStream();
            workbook.Write(stream);

            var fileBytes = stream.ToArray();

            var fileName = $"DailySales_{DateTime.Now:yyyyMMdd}.xlsx";

            await _sftpService.UploadReportAsync(fileBytes, fileName);
        }
    }
}
