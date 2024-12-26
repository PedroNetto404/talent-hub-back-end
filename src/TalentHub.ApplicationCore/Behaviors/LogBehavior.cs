using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace TalentHub.ApplicationCore.Behaviors;

public sealed class LogBehavior<TReq, TRes>(
    ILogger<LogBehavior<TReq, TRes>> logger
) :
    IPipelineBehavior<TReq, TRes>
    where TReq : notnull
    where TRes : notnull
{
    public async Task<TRes> Handle(
        TReq request,
        RequestHandlerDelegate<TRes> next,
        CancellationToken cancellationToken
    )
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            logger.LogInformation("Handling request {Request}", request);
            
            TRes response = await next();

            logger.LogInformation(
                "Handled request in {ElapsedMilliseconds}ms.\nRequest produced response {Response}",
                stopwatch.ElapsedMilliseconds,
                response
            );

            return response;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error handling request {Request}", request);
            throw;
        }
        finally
        {
            stopwatch.Stop();
            logger.LogInformation("Request handled in {ElapsedMilliseconds}ms", stopwatch.ElapsedMilliseconds);
        }
    }
}
