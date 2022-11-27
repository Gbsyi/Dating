using MediatR;

namespace Dating.Api.CqrsUtils;

public interface IHttpRequestHandler<in TRequest> : IRequestHandler<TRequest, IResult> 
    where TRequest : IHttpRequest
{
    
}
