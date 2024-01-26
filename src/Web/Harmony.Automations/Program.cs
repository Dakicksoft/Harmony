using Harmony.Application.Configurations;
using Harmony.Automations.Extensions;
using Harmony.Automations.Services;
using Harmony.Automations.Services.Hosted;
using Harmony.Infrastructure.Extensions;
using System.Reflection;
namespace Harmony.Automations
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSwaggerGen();
            builder.Services.AddMongoDbRepositories();
            builder.Services.AddDbSeed();
            builder.Services.AddEndpointConfiguration(builder.Configuration);
            builder.Services.AddApplicationLayer();
            builder.Services.Configure<BrokerConfiguration>(builder.Configuration.GetSection("BrokerConfiguration"));
            builder.Services.AddAutomationServices();

            builder.Services.AddMongoDb(builder.Configuration);
            builder.Services.AddMessaging(builder.Configuration);

            // Add Hangfire services.
            //builder.Services.AddHangfire(configuration => configuration
            //    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            //    .UseSimpleAssemblyNameTypeSerializer()
            //    .UseRecommendedSerializerSettings()
            //    .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));

            //// Add the processing server as IHostedService
            //builder.Services.AddHangfireServer();
            builder.Services.AddHostedService<AutomationNotificationsConsumerHostedService>();
            builder.Services.AddMemoryCache();

            // gRPC services
            builder.Services.AddGrpc();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // app.UseHangfireDashboard();

            app.UseRouting();

            app.UseAuthorization();

            app.MapGrpcService<AutomationService>();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.ConfigureSwagger();

            await app.SeedDatabase(builder.Configuration);

            app.Run();
        }
    }
}
