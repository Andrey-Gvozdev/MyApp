using ContentApp.Domain;
using ContentApp.Infrastructure;
using ContentApp.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Rebus.Config;
using Rebus.Routing.TypeBased;

namespace ContentApp;
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ContentAppDbContext>(options =>
            options.UseSqlServer(
                this.Configuration.GetConnectionString("DefaultConnection")));

        services.AddRebus(
               rebus => rebus
                  .Logging(l => l.Console())
                  .Routing(x => x.TypeBased())
                  .Transport(t => t.UseRabbitMq(this.Configuration["ConnectionStrings:RabbitMqConnection"], this.Configuration["QueueRabbitMQ:ContentAppQueue"]))
                  .Options(c =>
                    {
                      c.SetNumberOfWorkers(1);
                      c.SetMaxParallelism(1);
                  }));

        services.AddHostedService<EventSubscriber>();

        services.AutoRegisterHandlersFromAssemblyOf<PageRenderedHandler>();

        services.AddControllers();

        services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
        });

        services.AddTransient<IPageRepository, PageRepository>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.ApplicationServices.UseRebus();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
