using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FH.DAL.DataContext
{
    public class EfDbContextFactory : IDesignTimeDbContextFactory<EfDbContext>
    {
        public EfDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EfDbContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-D6DC2ID\\SQLEXPRESS;Database=FHDBv2;Trusted_Connection=True;");

            return new EfDbContext(optionsBuilder.Options);
        }
    }
}