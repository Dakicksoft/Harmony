﻿using Harmony.Application.Contracts.Persistence;

namespace Harmony.Server.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        internal static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", typeof(Program).Assembly.GetName().Name);
                options.RoutePrefix = "swagger";
                options.DisplayRequestDuration();
            });
        }

        internal static async Task<IApplicationBuilder> InitializeDatabase(this IApplicationBuilder app, Microsoft.Extensions.Configuration.IConfiguration _configuration)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var initializers = serviceScope.ServiceProvider.GetServices<IDatabaseSeed>();

            foreach (var initializer in initializers.OrderBy(init => init.Order))
            {
                await initializer.Initialize();
            }

            return app;
        }
    }
}
