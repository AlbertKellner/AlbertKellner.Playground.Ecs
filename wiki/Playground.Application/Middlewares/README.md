# Middlewares

Custom middlewares are registered in `RegisterCustomMiddlewareInitializer`.

## ExecutionTimeMiddleware
Adds the execution time header.
```csharp
public async Task InvokeAsync(HttpContext context)
{
    ExecutionTimeContext.Start();
    context.Response.OnStarting(() =>
    {
        context.Response.Headers["Execution-Time"] = ExecutionTimeContext.GetFormattedExecutionTime();
        return Task.CompletedTask;
    });
    await _next(context);
}
```

## BearerTokenMiddleware
Reads the JWT token and stores the `UserId` in the context.
```csharp
public async Task InvokeAsync(HttpContext context)
{
    if (context.Request.Headers.TryGetValue("Authorization", out var auth) && auth.ToString().StartsWith("Bearer "))
    {
        var token = auth.ToString()["Bearer ".Length..].Trim();
        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
        if (jwtToken.Payload.TryGetValue("UserId", out var userId))
            UserAuthorizationContext.SetUserId(userId.ToString()!);
    }
    await _next(context);
}
```

## CorrelationIdMiddleware
Ensures each request has a valid `CorrelationId`.
```csharp
public async Task InvokeAsync(HttpContext context)
{
    Guid correlationId;
    if (context.Request.Headers.TryGetValue("CorrelationId", out var value) && Guid.TryParse(value, out correlationId))
    {
        CorrelationContext.SetCorrelationId(correlationId);
    }
    else
    {
        correlationId = Guid.NewGuid();
        CorrelationContext.SetCorrelationId(correlationId);
    }
    context.Response.OnStarting(() =>
    {
        context.Response.Headers["CorrelationId"] = correlationId.ToString();
        return Task.CompletedTask;
    });
    await _next(context);
}
```
