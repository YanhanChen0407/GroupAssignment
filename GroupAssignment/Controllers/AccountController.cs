namespace GroupAssignment.Controllers
{
    using GroupAssignment.Models;
    using GroupAssignment.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using GroupAssignment;

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

                return Content($"Register success! Username：{model.Username}, Email：{model.Email}");
            }

            return View(model);
        }
    }

}
