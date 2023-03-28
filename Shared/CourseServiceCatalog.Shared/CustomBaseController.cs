using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServiceCatalog.Shared
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
