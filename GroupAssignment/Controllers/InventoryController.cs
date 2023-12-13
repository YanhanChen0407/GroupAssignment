using GroupAssignment;
using GroupAssignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public class InventoryController : Controller
{
    private readonly AppDbContext _dbContext;

    public InventoryController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    [Authorize]
    public IActionResult Index()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        ViewData["UserId"] = userId;


        return View();
    }
    [Authorize]
    public IActionResult ProductList()
    {
        //var products = _dbContext.Products.Select(p => new ProductModel { Id = p.Id, 
        //                                                                  ProductName = p.ProductName, 
        //                                                                  ProductQuantity = p.ProductQuantity, 
        //                                                                  ProductDescription = p.ProductDescription }).ToList();
        var products = _dbContext.Products.ToList();

        return View(products);
    }


}
