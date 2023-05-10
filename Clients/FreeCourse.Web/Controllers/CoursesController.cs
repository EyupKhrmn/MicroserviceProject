using CourseServiceCatalog.Shares;
using FreeCourse.Web.Models.Catalog;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

    public async Task<IActionResult> Create()
    {
        var categories = await _catalogService.GetAllCategoryAsync();

        ViewBag.categoryList = new SelectList(categories, "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CourseCreateInput courseCreateInput)
    {
        var categories = await _catalogService.GetAllCategoryAsync();
        ViewBag.categoryList = new SelectList(categories, "Id", "Name");
        if (!ModelState.IsValid)
        {
            return View();
        }

        courseCreateInput.UserId = _sharedIdentityServer.GetUserId;

        await _catalogService.CreateCourseAsync(courseCreateInput);

        return RedirectToAction(nameof(Index));
    }
}