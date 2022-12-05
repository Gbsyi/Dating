using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dating.Api.Modules.Picture;

[ApiController]
[Route("picture")]
public class PictureController : ControllerBase
{
    private readonly ISender _sender;

    public PictureController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{pictureId}")]
    public Task<IResult> DownloadPicture(Guid pictureId, CancellationToken cancellationToken)
    {
        return _sender.Send(new GetPictureQuery(pictureId), cancellationToken);
    }
    
    [HttpPost("upload")]
    public Task<IResult> UploadPicture(IFormFile file, CancellationToken cancellationToken)
    {
        return _sender.Send(new UploadPicturesCommand(file), cancellationToken);
    }
}