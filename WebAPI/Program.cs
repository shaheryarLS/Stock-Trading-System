using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Repositories.Implementations;
using Repositories.Interfaces;
using Services.Implementations;
using Services.Interfaces;
using Stock_Trading_System.Profiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register specific repositories
builder.Services.AddScoped<IStockRepository, StockRepository>();


// Register services
builder.Services.AddScoped<IStockService, StockService>();


builder.Services.AddAutoMapper(typeof(StockProfile));


var app = builder.Build();

app.MapControllers();

app.Run();
