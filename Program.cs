using SalesOrderAPI.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Sales_DBContext>(options=>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("connection"));
});

builder.Services.AddTransient<ICustomerContainer,CustomerContainer>();
builder.Services.AddTransient<IInvoiceContainer,InvoiceContainer>();
builder.Services.AddTransient<IProductContainer,ProductContainer>();

var automapper=new MapperConfiguration(item=>item.AddProfile(new MappingProfile()));
IMapper mapper=automapper.CreateMapper();
builder.Services.AddSingleton(mapper);

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
