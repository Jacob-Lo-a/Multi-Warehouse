using Hangfire;
using Microsoft.EntityFrameworkCore;
using Multi_Warehouse.API.Repositories;
using Multi_Warehouse.API.Services;
using Multi_Warehouse.Core;
using Multi_Warehouse.Core.interfaces;
using Multi_Warehouse.Core.Models;
using NLog.Web;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IStockLogRepository, StockLogRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();

builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISftpService, SftpService>();

builder.Services.Configure<Sftpsettings>(
    builder.Configuration.GetSection("SftpSettings"));

builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services.AddDbContext<AdvancedWmsDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("AdvancedWmsDb"))
    );

builder.Services.AddHangfire(config => config.UseSqlServerStorage(builder.Configuration.GetConnectionString("AdvancedWmsDb")));
builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHangfireDashboard();

app.MapControllers();

RecurringJob.AddOrUpdate<IReportService>(
    "Daily-Sales-Report",
    service => service.DailySalesReportAsync(),
    Cron.Daily(3) // 每天凌晨 3 點執行
);
app.Run();
