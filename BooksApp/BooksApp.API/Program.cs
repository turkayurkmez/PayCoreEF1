using BooksApp.Application.Services;
using BooksApp.BusinessLogic.ServiceLayer.BookService;
using BooksApp.BusinessLogic.ServiceLayer.OrderServices;
using BooksApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("db");
builder.Services.AddDbContext<BooksAppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<PlaceOrderService>();
builder.Services.AddScoped<ChangePriceOfferService>();
builder.Services.AddScoped<AddReviewService>();

builder.Services.AddScoped<QueryingService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
