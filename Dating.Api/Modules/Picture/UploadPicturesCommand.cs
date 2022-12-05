using Dating.Api.CqrsUtils;
using Dating.Domain.Models;
using Dating.Infrastructure;
using Dating.Infrastructure.Services;
using Dating.Shared.HttpErrors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dating.Api.Modules.Picture;

public sealed record UploadPicturesCommand(IFormFile File) : IHttpRequest;

internal sealed class UploadPicturesCommandHandler : IHttpRequestHandler<UploadPicturesCommand>
{
    private readonly IUserContext _userContext;
    private readonly IDatingDbContext _context;
    private readonly IWebHostEnvironment _environment;
    
    public UploadPicturesCommandHandler(IUserContext userContext, IDatingDbContext context, IWebHostEnvironment environment)
    {
        _userContext = userContext;
        _context = context;
        _environment = environment;
    }

    public async Task<IResult> Handle(UploadPicturesCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;
        if (userId is null)
        {
            return Results.Unauthorized();
        }

        var profile = await _context.Profiles.Where(x => x.Id == userId).FirstOrDefaultAsync(cancellationToken);
        if (profile is null)
        {
            return Results.BadRequest(new BadRequestVm
            {
                ErrorMessage = "Профиль не найден"
            });
        }
        var file = request.File;
        var extension = Path.GetExtension(file.FileName);
        if (extension is not "jpeg" and not "jpg")
        {
            return Results.BadRequest(new BadRequestVm
            {
                ErrorMessage = "Неверный формат фото"
            });
        }

        var picture = new Domain.Models.Picture()
        {
            Id = Guid.NewGuid()
        };

        await using var fs = new FileStream(Path.Combine(_environment.WebRootPath, "pictures", $"{picture.Id}.jpg"), FileMode.Create);
        await file.OpenReadStream().CopyToAsync(fs, cancellationToken);

        _context.Pictures.Add(picture);

        await _context.SaveChangesAsync(cancellationToken);

        return Results.Ok();
    }
}