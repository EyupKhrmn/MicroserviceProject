using CourseServiceCatalog.Shares;
using Microsoft.AspNetCore.Mvc;
using NoContent = Microsoft.AspNetCore.Http.HttpResults.NoContent;

namespace FreeCourse.Services.FakePayment.Controller;

public class FakePaymentController : CustomBaseController
{
    [HttpPost]
    public async Task<IActionResult> ReceivePayment()
    {
        return CreateActionResultInstance(ResponseDto<NoContent>.Success(200));
    }
}