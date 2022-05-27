using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DataAccess.EF;

public sealed class MarineDesignTimeDbContextFactory : IDesignTimeDbContextFactory<MarineContext>
{
    public MarineContext CreateDbContext(string[] args) => new(
        new LoggerFactory(),
        new OptionsWrapper<MarineDbConnectionString>(
            new MarineDbConnectionString
            {
                Value = "Server=localhost,1433;Initial Catalog=MarineDB;User ID=sa;Password=Strong_P@55w0rd"
            }
        )
    );
}