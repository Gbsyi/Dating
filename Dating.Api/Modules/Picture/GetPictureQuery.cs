using Dating.Api.CqrsUtils;
using Dating.Infrastructure;
using Dating.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dating.Api.Modules.Picture;

public sealed record GetPictureQuery([FromRoute]Guid PictureId) : IHttpRequest;

internal sealed class GetPictureQueryHandler : IHttpRequestHandler<GetPictureQuery>
{
    private readonly IDatingDbContext _context;
    private readonly IWebHostEnvironment _environment;
    
    public GetPictureQueryHandler(IDatingDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public async Task<IResult> Handle(GetPictureQuery request, CancellationToken cancellationToken)
    {
        var picture = await _context.Pictures.Where(x => x.Id == request.PictureId)
            .FirstOrDefaultAsync(cancellationToken);
        if (picture is null)
        {
            return Results.NotFound();
        }
        
        var picturePath = Path.Combine(_environment.WebRootPath, "pictures", $"{picture.Id}.jpg");
        var pictureStream = new FileStream(picturePath, FileMode.Open);
        
        return Results.File(pictureStream);
    }
}