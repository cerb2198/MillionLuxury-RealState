using Hangfire;

namespace MillionLuxury.RealEstate.API.Extensions;

public static class HangfireDashboardDependecyInjection
{
    private const string HangfireDashboardPath = "/hangfire";
    public static void AddHangfireDashboard(
        this IApplicationBuilder applicationBuilder,
        IWebHostEnvironment env
        )
    {
        if (env.IsDevelopment())
        {
            applicationBuilder.UseHangfireDashboard(HangfireDashboardPath);
        }
    }
}
