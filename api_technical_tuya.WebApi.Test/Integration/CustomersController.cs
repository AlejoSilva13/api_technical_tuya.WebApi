using api_technical_tuya.Application.Dtos;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using api_technical_tuya.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace api_technical_tuya.WebApi.Test.Integration
{
    public class CustomersControllerTests
    {
        private HttpClient CreateIsolatedClient(out IServiceProvider sp)
        {
            var dbName = $"TestDb_Customers_{Guid.NewGuid()}";
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

            sp = factory.Services;
            return factory.CreateClient();
        }

        [Fact]
        public async Task GetAll_ShouldReturnEmptyList_WhenNoCustomers()
        {
            using var client = CreateIsolatedClient(out _);

            var response = await client.GetAsync("/api/customers");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var customers = await response.Content.ReadFromJsonAsync<List<CustomerDto>>();
            customers.Should().BeEmpty();
        }

        [Fact]
        public async Task Post_ShouldCreateCustomer_AndReturn201()
        {
            using var client = CreateIsolatedClient(out _);

            var createRes = await client.PostAsJsonAsync("/api/customers", new { Name = "Diego", Email = "diego@example.com" });
            createRes.StatusCode.Should().Be(HttpStatusCode.Created);

            var created = await createRes.Content.ReadFromJsonAsync<CustomerDto>();
            created.Should().NotBeNull();
            created!.Name.Should().Be("Diego");
        }

        [Fact]
        public async Task GetById_ShouldReturnCustomer_WhenExists()
        {
            using var client = CreateIsolatedClient(out _);

            var create = await client.PostAsJsonAsync("/api/customers", new { Name = "Diego", Email = "diego@example.com" });
            var created = await create.Content.ReadFromJsonAsync<CustomerDto>();

            var get = await client.GetAsync($"/api/customers/{created!.Id}");
            get.StatusCode.Should().Be(HttpStatusCode.OK);

            var dto = await get.Content.ReadFromJsonAsync<CustomerDto>();
            dto!.Id.Should().Be(created.Id);
            dto.Name.Should().Be("Diego");
        }

        [Fact]
        public async Task Put_ShouldUpdateCustomer_AndReturn200()
        {
            using var client = CreateIsolatedClient(out _);

            var create = await client.PostAsJsonAsync("/api/customers", new { Name = "Inicial", Email = "init@example.com" });
            var created = await create.Content.ReadFromJsonAsync<CustomerDto>();

            var updateReq = new { Name = "Actualizado", Email = "new@example.com" };
            var put = await client.PutAsJsonAsync($"/api/customers/{created!.Id}", updateReq);
            put.StatusCode.Should().Be(HttpStatusCode.OK);

            var updated = await put.Content.ReadFromJsonAsync<CustomerDto>();
            updated!.Name.Should().Be("Actualizado");
            updated.Email.Should().Be("new@example.com");
        }

        [Fact]
        public async Task Delete_ShouldReturn204_AndRemoveCustomer()
        {
            using var client = CreateIsolatedClient(out _);

            var create = await client.PostAsJsonAsync("/api/customers", new { Name = "A Borrar", Email = "del@example.com" });
            var created = await create.Content.ReadFromJsonAsync<CustomerDto>();

            var del = await client.DeleteAsync($"/api/customers/{created!.Id}");
            del.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var getAll = await client.GetAsync("/api/customers");
            var list = await getAll.Content.ReadFromJsonAsync<List<CustomerDto>>();
            list!.Any(x => x.Id == created.Id).Should().BeFalse();
        }
    }
}
