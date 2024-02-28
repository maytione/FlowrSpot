using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FlowrSpot.Infrastructure.Data
{
    internal class ApplicationDbContextInit
    {
        private readonly ILogger<ApplicationDbContextInit> _logger;
        private readonly ApplicationDbContext _context;

        public ApplicationDbContextInit(ILogger<ApplicationDbContextInit> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitDatabase()
        {
            try
            {
                _logger.LogInformation("Migration started...");
                await _context.Database.MigrateAsync();
                _logger.LogInformation("Migration finished.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while init database.");
                throw;
            }
        }

    }
}
