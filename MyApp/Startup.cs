using System.Text.Json.Serialization;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain.DomainModel;
using MyApp.Domain.Services;
using MyApp.Middleware;

namespace MyApp;
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                this.Configuration.GetConnectionString("DefaultConnection")));

        services.AddControllers();

        services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
        });

        services.AddTransient<IPageRepository, PageRepository>();
        services.AddTransient<IPageCrudService, PageCrudService>();
        services.AddTransient<IValidationCreativeNameService, ValidationCreativeNameService>();
        services.AddTransient<IPageSnippetListFillingService, PageSnippetListFillingService>();
        services.AddTransient<ISnippetRepository, SnippetRepository>();
        services.AddTransient<ISnippetCrudService, SnippetCrudService>();
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

        app.UseCreativeValidationMiddleware();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
