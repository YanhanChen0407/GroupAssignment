﻿using GroupAssignment.Models;
using Microsoft.EntityFrameworkCore;
//Yanhan Chen
//991647343
//Group Assignment 
//Database
namespace GroupAssignment
{

    public class AppDbContext : DbContext
    {


        public DbSet<UserModel> Users { get; set; }
        public DbSet<ProductEntity> Products { get; set; }

        public DbSet<OrderEntity> Orders { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().HasKey(u => u.Id);
            modelBuilder.Entity<ProductEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<OrderEntity>().HasKey(o => o.Id);


            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=..\\app.db");

        }


    }
}

