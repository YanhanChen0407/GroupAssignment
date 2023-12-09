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

        public IActionResult Users()
        {
            var users = _dbContext.Users.Select(u => new UserModel { Id = u.Id, Username = u.Username,Email = u.Email, Role = u.Role }).ToList();

            return View(users);
        }

        // 在 AccountController.cs 中添加以下方法
        public IActionResult Edit(Guid userId)
        {
            // 根据用户 ID 从数据库中获取用户信息
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                // 用户不存在，可以添加适当的处理逻辑
                return RedirectToAction("Users");
            }

            // 将用户信息传递到编辑视图
            var userModel = new UserModel
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
                // 添加其他需要编辑的属性
            };

            return View(userModel);
        }

        [HttpPost]
        public IActionResult Edit([Bind("Id,Username,Role")] UserModel model)
        {
            if (ModelState.IsValid)
            {
                // 根据用户 ID 从数据库中获取用户信息
                var user = _dbContext.Users.FirstOrDefault(u => u.Id == model.Id);

                if (user != null)
                {
                    // 更新用户信息
                    user.Username = model.Username;
                    user.Email = model.Email;
                    user.Role = model.Role;
                    // 更新其他属性

                    // 保存更改到数据库
                    _dbContext.SaveChanges();

                    return RedirectToAction("Users");
                }
            }

            // 如果模型验证失败，返回编辑视图
            return View(model);
        }

    }
}
