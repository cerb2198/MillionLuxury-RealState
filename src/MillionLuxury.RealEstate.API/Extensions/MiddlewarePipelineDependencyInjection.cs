using Hangfire;

namespace MillionLuxury.RealEstate.API.Extensions;

public static class MiddlewarePipelineDependencyInjection
{
    public static IApplicationBuilder ConfigureMiddlewarePipeline(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseProblemDetailsHandling();

        app.UseSwaggerConfiguration();

        if (!env.IsDevelopment())
        {
            app.UseHttpsRedirection();
        }

        app.UseCors(env.EnvironmentName);

        app.UseAuthentication();

        app.UseAuthorization();

        app.AddHangfireDashboard(env);

        return app;
    }

    public static IApplicationBuilder ConfigureApplication(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.ConfigureMiddlewarePipeline(env);

        if (app is WebApplication webApp)
            webApp.AddEndpoints();

        return app;
    }
}
