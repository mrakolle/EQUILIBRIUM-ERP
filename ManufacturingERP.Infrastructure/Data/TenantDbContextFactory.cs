using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using ManufacturingERP.Infrastructure.MultiTenancy;


namespace ManufacturingERP.Infrastructure.Data;

public class TenantDbContextFactory
{
    private readonly string _connectionString;

    public TenantDbContextFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("MasterDb")!;
    }

    public TenantDbContext Create(string schema)
    {
        var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using (var cmd = connection.CreateCommand())
        {
            cmd.CommandText = $"CREATE SCHEMA IF NOT EXISTS \"{schema}\"";
            cmd.ExecuteNonQuery();
        }

        using (var cmd = connection.CreateCommand())
        {
            cmd.CommandText = $"SET search_path TO \"{schema}\", public";
            cmd.ExecuteNonQuery();
        }

        var options = new DbContextOptionsBuilder<TenantDbContext>()
            .UseNpgsql(connection)
            .Options;

        return new TenantDbContext(options, schema);
    }
}


/*public class TenantDbContextFactory : IDesignTimeDbContextFactory<TenantDbContext>
{
    public TenantDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = config.GetConnectionString("MasterDb");

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new Exception("MasterDb connection string not found");

        var optionsBuilder = new DbContextOptionsBuilder<TenantDbContext>();

        optionsBuilder.UseNpgsql(connectionString);

        return new TenantDbContext(optionsBuilder.Options, "public");
    }
}*/