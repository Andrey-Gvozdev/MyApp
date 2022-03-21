using JavaScriptEngineSwitcher.ChakraCore;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using React.AspNet;

namespace ClientApp;
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMemoryCache();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddReact();
        services.AddJsEngineSwitcher(options => options.DefaultEngineName = ChakraCoreJsEngine.EngineName).AddChakraCore();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseBrowserLink();
        }

        app.UseReact(config =>
        {
            config
              .AddScript("~/js/main.5ca9131f.js")
              .SetJsonSerializerSettings(new JsonSerializerSettings
              {
                  StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                  ContractResolver = new CamelCasePropertyNamesContractResolver(),
              });
        });

        app.UseHttpsRedirection();

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseRouting();

/*        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });*/
    }
}
