using GroupAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public class InventoryController : Controller
{
    public IActionResult Index()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        ViewData["UserId"] = userId;


        return View();
    }

    public IActionResult ProductList()
    {
        //var products = _dbContext.Products.Select(p => new ProductModel { Id = p.Id, ProductName = p.ProductName, Quantities = p.Quantities, Description = p.Description }).ToList();

        return View(/*products*/);
    }


}
