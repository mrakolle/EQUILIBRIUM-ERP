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

