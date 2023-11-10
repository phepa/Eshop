using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Eshop.Shared.Extensions
{
    public static class DatabaseExtensions
    {
        public static void MigrateDatabase<TContext>(this IApplicationBuilder builder) where TContext : DbContext
        {
            using IServiceScope serviceScope = builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            serviceScope.ServiceProvider.GetService<TContext>()!.Database.Migrate();
        }
    }
}
