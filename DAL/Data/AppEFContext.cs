using Microsoft.EntityFrameworkCore;
using SomeeMSSQLConsole.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Data
{
    public class AppEFContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=TaniaDB.mssql.somee.com;Database=TaniaDB;User Id=voyo_SQLLogin_1;Password=mm9a6mhplx;");
        }
    }
}
