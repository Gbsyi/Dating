using MediatR;

namespace Dating.Api.CqrsUtils;

public static class CqrsExtensions
{
    public static IEndpointRouteBuilder MediateGet<TRequest>(this IEndpointRouteBuilder app, string template) where TRequest : IHttpRequest
    {
        app.MapGet(template, async (IMediator mediator, [AsParameters] TRequest request) => await mediator.Send(request));
        
        return app;
    }
    
    public static IEndpointRouteBuilder MediatePost<TRequest>(this IEndpointRouteBuilder app, string template) where TRequest : IHttpRequest
    {
        app.MapPost(template,
            async (IMediator mediator, [AsParameters] TRequest request) => await mediator.Send(request));
        
        return app;
    }
    
    public static IEndpointRouteBuilder MediatePost<TRequest, TResult>(this IEndpointRouteBuilder app, string template) where TRequest : IHttpRequest
    {
        app.MapPost(template,
                async (IMediator mediator, [AsParameters] TRequest request) => await mediator.Send(request))
            .Produces<TResult>();
        
        return app;
    }
}