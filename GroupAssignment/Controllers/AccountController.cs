namespace GroupAssignment.Controllers
{
    using GroupAssignment.Models;
    using GroupAssignment.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using GroupAssignment;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authentication;
    using System.Security.Claims;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AccountController : Controller
    {
        private readonly AppDbContext _dbContext;

        public AccountController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserModel model)
        {
            if (ModelState.IsValid)
            {
                model.Id = Guid.NewGuid();
                _dbContext.Users.Add(model);
                _dbContext.SaveChanges();

                return Content($"Register success! Username：{model.Username}, Email：{model.Email}, Role: {model.Role}");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserModel model)
        {

            var user = _dbContext.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

            if (user != null)
            {
                TempData["UserId"] = user.Id;
                TempData["Username"] = user.Username;
                TempData["UserRole"] = user.Role;
                Console.WriteLine($"Login successful for username: {model.Username}, UserId: {user.Id}, Role: {user.Role}");


                return RedirectToAction("Index", "Inventory");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Incorrect username or password");
                Console.WriteLine($"Login failed for username: {model.Username}, password: {model.Password}");

            }
            return View(model);
        }
    }
}
