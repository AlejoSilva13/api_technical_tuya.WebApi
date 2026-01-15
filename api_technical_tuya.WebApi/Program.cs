using api_technical_tuya.Application.Interfaces;
using api_technical_tuya.Application.UseCases.Customer.DeleteCustomer;
using api_technical_tuya.Application.UseCases.Customer.GetCustomer;
using api_technical_tuya.Application.UseCases.Customer.UpdateCustomer;
using api_technical_tuya.Application.UseCases.Customers.CreateCustomer;
using api_technical_tuya.Application.UseCases.Orders.CreateOrder;
using api_technical_tuya.Application.UseCases.Orders.GetOrder;
using api_technical_tuya.Domain.Interfaces;
using api_technical_tuya.Infrastructure;
using api_technical_tuya.Infrastructure.Repositories;
using api_technical_tuya.WebApi.Filters;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped < IUnitOfWork, UnitOfWork>();


builder.Services.AddScoped<CreateCustomerHandler>();
builder.Services.AddScoped<UpdateCustomerHandler>();
builder.Services.AddScoped<DeleteCustomerHandler>();
builder.Services.AddScoped<GetAllCustomersHandler>();
builder.Services.AddScoped<GetCustomerIdHandler>();
builder.Services.AddScoped<CreateOrderHandler>();
builder.Services.AddScoped<CreateOrderHandler>();
builder.Services.AddScoped < GetOrderHandler>();



var app = builder.Build();
app.UseMiddleware<GlobalExceptionMiddleware>();

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
