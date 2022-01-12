using Infrastructure;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain.DomainModel;
using MyApp.Domain.Services;
using MyApp.Domain.Services.CRUDServices;
using MyApp.Middleware;
using MyApp.Services;

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

        services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
        });

        services.AddTransient<ICreativeRepository, CreativeRepository>();
        services.AddTransient<IPageRepository, PageRepository>();
        services.AddTransient<IPageCrudService, PageCrudService>();
        services.AddTransient<IValidationCreativeNameService, ValidationCreativeNameService>();
        services.AddTransient<ISnippetRepository, SnippetRepository>();
        services.AddTransient<ISnippetCrudService, SnippetCrudService>();
        services.AddTransient<IIsUseSnippetValidation, IsUseSnippetValidation>();
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
