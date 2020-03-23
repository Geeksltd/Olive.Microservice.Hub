namespace Website
{
    using Domain;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Olive;
    using Olive.Entities.Data;
    using System;
    using System.Linq;
    using System.Globalization;
    using Olive.PassiveBackgroundTasks;
    using System.Threading.Tasks;
    using System.IO;
    using System.Reflection;
    using Microsoft.Extensions.FileProviders;

    public abstract class BaseStartup : Olive.Mvc.Startup
    {
        public BaseStartup(IHostingEnvironment env, IConfiguration config, ILoggerFactory factory) : base(env, config, factory)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.DefaultThreadCurrentUICulture =
                CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = new CultureInfo("en-GB");

            Subdomains = config["GeeksSubdomain"]?.Split(",") ?? new string[0];
        }

        protected override CultureInfo GetRequestCulture() => new CultureInfo("en-GB");

        string[] Subdomains;

        public override void ConfigureServices(IServiceCollection services)
        {

            if (Subdomains.Any())
                services.AddCors(c => c.AddPolicy("AllowSubdomains", builder =>
                {
                    var domainProtocol = $"http{"s".OnlyWhen(Environment.IsProduction())}://*.";
                    var domains = from d in Subdomains
                                  let trimmed = d.TrimStart("*").TrimStart(".")
                                  select domainProtocol.WithSuffix(d);


                    builder.WithOrigins(domains.ToArray())
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .SetIsOriginAllowedToAllowWildcardSubdomains();
                }));

            base.ConfigureServices(services);

            services.AddDataAccess(x => x.SqlServer());
            services.AddScheduledTasks<BackgroundTask>();

        }

        public override void Configure(IApplicationBuilder app)
        {
            if (Subdomains.Any())
                app.UseCors("AllowSubdomains");

            app.UseStaticFiles();

            base.Configure(app);

            app.UseGlobalSearch<GlobalSearchSource>();

            var dllPath = Path.Join(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Olive.Microservice.Hub.dll");

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new ManifestEmbeddedFileProvider(Assembly.LoadFrom(dllPath), "Website\\wwwroot")
            });

            Console.Title = Microservice.Me.Name;

            RegisterConfigFiles();

            AppContentService.HubApi.DefaultConfig(config => config.Cache(CachePolicy.CacheOrFreshOrNull));
        }

        protected virtual void RegisterConfigFiles()
        {
            Feature.DataProvider.Register();
            Service.DataProvider.Register();
            Board.DataProvider.Register();
        }

        public override async Task OnStartUpAsync(IApplicationBuilder app)
        {
            await base.OnStartUpAsync(app);
            StructureDeserializer.Load();
        }

        protected override void ConfigureRequestHandlers(IApplicationBuilder app)
        {
            app.Use(RedirectSmartPhone);
            app.UseFeaturesViewPageMiddleware();
            base.ConfigureRequestHandlers(app);
        }

        static async Task RedirectSmartPhone(Microsoft.AspNetCore.Http.HttpContext context, Func<Task> next)
        {
            if (context.Request.Path.Value == "/" && context.Request.IsSmartPhone())
                context.Response.Redirect("/root");
            else await next();
        }

        protected override void ConfigureAuthentication(AuthenticationBuilder auth)
        {
            base.ConfigureAuthentication(auth);

            auth.AddGoogle(config =>
            {
                config.ClientId = Config.Get("Authentication:Google:ClientId");
                config.ClientSecret = Config.Get("Authentication:Google:ClientSecret");

                ConfigureDataProtectionProvider(config);
            });

        }

        protected abstract void ConfigureDataProtectionProvider(Microsoft.AspNetCore.Authentication.Google.GoogleOptions config);

        protected override void ConfigureExceptionPage(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage(); // even in production
        }
    }
}