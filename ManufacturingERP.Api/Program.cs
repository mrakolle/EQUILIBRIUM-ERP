using ManufacturingERP.Infrastructure.DependencyInjection;
using ManufacturingERP.Infrastructure.MultiTenancy;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// -------------------------------------------------
// 1. SERVICES (DI)
// -------------------------------------------------

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Infrastructure (DB, Tenancy, Hangfire, etc.)
builder.Services.AddInfrastructure(configuration);

// Hangfire server (ensures background worker starts)
//builder.Services.AddHangfireServer();

// -------------------------------------------------
// 2. BUILD APP
// -------------------------------------------------

var app = builder.Build();

// -------------------------------------------------
// 3. MIDDLEWARE PIPELINE
// -------------------------------------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🔥 CRITICAL: Tenant resolution MUST come early
app.UseMiddleware<TenantMiddleware>();

app.UseAuthorization();

// -------------------------------------------------
// 4. HANGFIRE DASHBOARD
// -------------------------------------------------

app.UseHangfireDashboard("/hangfire");

// -------------------------------------------------
// 5. ROUTING
// -------------------------------------------------

app.MapControllers();

// -------------------------------------------------
// 6. RUN
// -------------------------------------------------

app.Run();