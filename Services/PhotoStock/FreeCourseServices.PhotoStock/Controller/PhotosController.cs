using CourseServiceCatalog.Shares;
using FreeCourseServices.PhotoStock.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace FreeCourseServices.PhotoStock.Controller;

[Route("api/[controller]")]
[ApiController]
public class PhotosController : CustomBaseController
{
    [HttpPost]
    public async Task<IActionResult> PhotoSave(IFormFile? photo, CancellationToken cancellationToken)
    {
        if (photo != null && photo.Length > 0)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);

            await using (var stream = new FileStream(path,FileMode.Create))
            {
                await photo.CopyToAsync(stream,cancellationToken);
            }

            var returnPath = "photos/" + photo.FileName;

            PhotoDto photoDto = new()
            {
                Url = returnPath
            };

            return CreateActionResultInstance(ResponseDto<PhotoDto>.Success(photoDto, 200));
        }

        return CreateActionResultInstance(ResponseDto<PhotoDto>.Fail("Photo is empty", 400));
    }


    [HttpDelete]
    public IActionResult PhotoDelete(string photoUrl)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);

        if (!System.IO.File.Exists(path))
        {
            return CreateActionResultInstance(ResponseDto<NoContent>.Fail("Photo is not Fount", 404));
        }
        
        System.IO.File.Delete(path);

        return CreateActionResultInstance(ResponseDto<NoContent>.Success(204));
    }
}