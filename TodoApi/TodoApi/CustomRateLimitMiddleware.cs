using AspNetCoreRateLimit;
using Microsoft.Extensions.Options;

namespace TodoApi;

public class CustomRateLimitMiddleware: IpRateLimitMiddleware
{
    public CustomRateLimitMiddleware(
        RequestDelegate next,
        IProcessingStrategy processingStrategy,
        IOptions<IpRateLimitOptions> options,
        IIpPolicyStore policyStore,
        IRateLimitConfiguration config,
        ILogger<IpRateLimitMiddleware> logger 
    ) : base(next, processingStrategy, options, policyStore, config, logger)
    {
    }

    protected override async void LogBlockedRequest(HttpContext httpContext, ClientRequestIdentity identity, RateLimitCounter counter,
        RateLimitRule rule)
    {
        httpContext.Response.StatusCode = 429;
        await httpContext.Response.WriteAsync("Custom message: Too many requests. Please try again later.");
    }
}

//var logger = httpContext.RequestServices.GetRequiredService<ILogger<CustomRateLimitMiddleware>>();
// logger.LogInformation("Blocked request: {Path} exceeded rate limit {Limit} for period {Period}.",
//     httpContext.Request.Path, rule.Limit, rule.Period);