namespace GroupAssignment.Controllers
{
    using GroupAssignment.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using GroupAssignment;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
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

                return RedirectToAction("Login");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserModel model)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);

                return RedirectToAction("Index", "Inventory");
            }

            ModelState.AddModelError(string.Empty, "Incorrect username or password");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult Users()
        {
            var users = _dbContext.Users.Select(u => new UserModel { Id = u.Id, Username = u.Username,Email = u.Email, Role = u.Role }).ToList();

            return View(users);
        }

        public IActionResult Edit(Guid userId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return RedirectToAction("Users");
            }

            var userModel = new UserModel
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };

            return View(userModel);
        }

        [HttpPost]
        public IActionResult Edit([Bind("Id,Username,Email,Role")] UserModel model)
        {
            if (ModelState.IsValid)
            {

            }
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == model.Id);

            if (user != null)
            {
                user.Username = model.Username;
                user.Email = model.Email;
                user.Role = model.Role;

                _dbContext.SaveChanges();

                return RedirectToAction("Users");
            }
            return View(model);
        }

    }
}
