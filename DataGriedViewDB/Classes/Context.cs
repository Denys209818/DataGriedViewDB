using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataGriedViewDB.Classes
{
    public class Context : DbContext
    {
        public DbSet<MedizinDepartment> departments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=95.214.10.36;Port=5432;Database=denysdb;Username=denys;Password=qwerty1*;");
        }
    }
}
