using Microsoft.AspNetCore.Mvc;

namespace CourseServiceCatalog.Shares
{
    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(ResponseDto<T> responce)
        {
            return new ObjectResult(responce)
            {
                StatusCode = responce.StatusCode
            };
        }
    }
}