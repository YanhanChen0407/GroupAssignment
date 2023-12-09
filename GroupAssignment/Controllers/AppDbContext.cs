using GroupAssignment.Models;
using Microsoft.EntityFrameworkCore;

//namespace GroupAssignment.Controllers
//{


//}

public class AppDbContext : DbContext
{
    public DbSet<UserModel> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=..\\app.db");

    }


}