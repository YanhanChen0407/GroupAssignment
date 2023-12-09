using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class InventoryController : Controller
{
    public IActionResult Index()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        ViewData["UserId"] = userId;


        return View();
    }
}
