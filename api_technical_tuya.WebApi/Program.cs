using api_technical_tuya.Application.UseCases.Customer.DeleteCustomer;
using api_technical_tuya.Application.UseCases.Customer.GetCustomer;
using api_technical_tuya.Application.UseCases.Customer.UpdateCustomer;
using api_technical_tuya.Application.UseCases.Customers.CreateCustomer;
using api_technical_tuya.Application.UseCases.Orders.CreateOrder;
using api_technical_tuya.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Infrastructure(DbContext, repos, uow, clock)
builder.Services.AddInfrastructure(builder.Configuration);

// Application handlers (si usas inyección directa de handlers)
builder.Services.AddScoped<CreateCustomerHandler>();
builder.Services.AddScoped<UpdateCustomerHandler>();
builder.Services.AddScoped<DeleteCustomerHandler>();
builder.Services.AddScoped<GetAllCustomersHandler>();
builder.Services.AddScoped<GetCustomerIdHandler>();
builder.Services.AddScoped<CreateOrderHandler>();


var app = builder.Build();
//app.UseMiddleware<GlobalExceptionMiddleware>();

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
public partial class Program { }
