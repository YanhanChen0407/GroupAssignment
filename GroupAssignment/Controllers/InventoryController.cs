//Yanhan Chen
//991647343
//Group Assignment 
//Controller for Inventory
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
        return View();
    }
    [Authorize]
    public IActionResult ProductList()
    {
        var products = _dbContext.Products.ToList();

        return View(products);
    }


}
