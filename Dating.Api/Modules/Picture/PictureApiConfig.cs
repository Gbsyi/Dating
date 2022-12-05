using Dating.Api.CqrsUtils;

namespace Dating.Api.Modules.Picture;

public static class PictureApiConfig
{
    [Obsolete]
    public static void AddPictureEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("picture").WithTags("Picture");

        group.MediateGet<GetPictureQuery>("/{pictureId}");
        group.MediatePost<UploadPicturesCommand>("/upload");
    }
}