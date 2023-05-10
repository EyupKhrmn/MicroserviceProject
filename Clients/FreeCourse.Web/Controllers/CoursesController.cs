using CourseServiceCatalog.Shares;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Web.Controllers;

[Authorize]
public class CoursesController : Controller
{
    private readonly ICatalogService _catalogService;

    private readonly ISharedIdentityServer _sharedIdentityServer;
    // GET
    public CoursesController(ICatalogService catalogService, ISharedIdentityServer sharedIdentityServer)
    {
        _catalogService = catalogService;
        _sharedIdentityServer = sharedIdentityServer;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _catalogService.GetAllCourseByUserIdAsync(_sharedIdentityServer.GetUserId));
    }
}