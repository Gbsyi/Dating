using MediatR;

namespace Dating.Api.CqrsUtils;

public interface IHttpRequest : IRequest<IResult>
{
    
}