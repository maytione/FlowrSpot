using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FlowrSpot.Infrastructure.Data
{
    public static class MigrationExtension
    {
        public static async Task UpdateDb(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInit>();
            await initialiser.InitDatabase();
        }
    }
}
