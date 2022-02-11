using System.Linq;
using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MyApp;
using MyApp.Contracts.Events;
using Rebus.Bus;

namespace MyAppIntegrationTests;
public class MyAppFactory
        : WebApplicationFactory<Startup>
{
    IConfiguration Configuration { get; set; }
    
    public Mock<IBus> mockBus = new();
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<ApplicationDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddSingleton(mockBus.Object);

            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("InMemoryDbForTesting"));

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ApplicationDbContext>();

                db.Database.EnsureCreated();
            }
        });

        builder.ConfigureAppConfiguration(config =>
        {
            Configuration = new ConfigurationBuilder()
              .AddJsonFile("integrationsettings.json")
              .Build();

            config.AddConfiguration(Configuration);
        });
    }
}