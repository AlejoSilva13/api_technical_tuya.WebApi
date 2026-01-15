using api_technical_tuya.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using api_technical_tuya.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace api_technical_tuya.WebApi.Test.Integration
{
    public class OrdersControllerTests
    {
        private HttpClient CreateIsolatedClient()
        {
            var dbName = $"TestDb_Orders_{Guid.NewGuid()}";
            var factory = new CustomWebApplicationFactory()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                        if (descriptor is not null) services.Remove(descriptor);

                        services.AddDbContext<AppDbContext>(options =>
                            options.UseInMemoryDatabase(dbName));

                        var spTemp = services.BuildServiceProvider();
                        using var scope = spTemp.CreateScope();
                        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                        db.Database.EnsureCreated();
                    });
                });

            return factory.CreateClient();
        }
        private sealed record CreateOrderResult(Guid OrderId, string Status);

        [Fact]
        public async Task Post_ShouldCreateOrder_WhenCustomerExists()
        {
            using var client = CreateIsolatedClient();

            var customerRes = await client.PostAsJsonAsync("/api/customers", new { Name = "Diego", Email = "diego@example.com" });
            var customer = await customerRes.Content.ReadFromJsonAsync<CustomerDto>();

            var orderReq = new { CustomerId = customer!.Id, Total = 150m };
            var orderRes = await client.PostAsJsonAsync("/api/orders", orderReq);

            orderRes.StatusCode.Should().Be(HttpStatusCode.Created);

            var created = await orderRes.Content.ReadFromJsonAsync<CreateOrderResult>();
            created!.Status.Should().Be("Created");
        }

        [Fact]
        public async Task GetById_ShouldReturnOrder_WhenExists()
        {
            using var client = CreateIsolatedClient();

            var customerRes = await client.PostAsJsonAsync("/api/customers", new { Name = "Diego", Email = "diego@example.com" });
            var customer = await customerRes.Content.ReadFromJsonAsync<CustomerDto>();

            var createOrder = await client.PostAsJsonAsync("/api/orders", new { CustomerId = customer!.Id, Total = 199.90m });
            var created = await createOrder.Content.ReadFromJsonAsync<CreateOrderResult>();

            var getRes = await client.GetAsync($"/api/orders/{created!.OrderId}");
            getRes.StatusCode.Should().Be(HttpStatusCode.OK);

            var order = await getRes.Content.ReadFromJsonAsync<OrderDto>();
            order!.CustomerId.Should().Be(customer.Id);
            order.Total.Should().Be(199.90m);
        }

    }
}
