public class ExecutionContext
{
    private readonly ILogger<ExecutionContext> _logger;
    private readonly List<string> _steps = new();

    public ExecutionContext(ILogger<ExecutionContext> logger)
    {
        _logger = logger;
    }

    public void Step(string message)
    {
        _steps.Add(message);
        _logger.LogInformation("➡️ {Message}", message);
    }

    public void Dump()
    {
        _logger.LogInformation("📊 EXECUTION PATH:");
        foreach (var step in _steps)
        {
            _logger.LogInformation("   ↓ {Step}", step);
        }
    }
}