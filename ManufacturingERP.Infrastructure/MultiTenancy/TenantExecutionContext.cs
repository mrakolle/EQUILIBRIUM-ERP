namespace ManufacturingERP.Infrastructure.MultiTenancy;

public class TenantExecutionContext
{
    private readonly ITenantProvider _tenantProvider;

    public TenantExecutionContext(ITenantProvider tenantProvider)
    {
        _tenantProvider = tenantProvider;
    }

    public async Task RunAsync(string schema, Func<Task> action)
    {
        var originalSchema = _tenantProvider.Schema;

        try
        {
            _tenantProvider.Schema = schema;

            await action();
        }
        finally
        {
            _tenantProvider.Schema = originalSchema;
        }
    }
}