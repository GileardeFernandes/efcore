using EFCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Data
{
    public class ApplicationContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.UseSqlServer("localhost;Database=CURSO_EF_CORE;User Id=sa;Password=ps@#1346;");
            //optionsBuilder.UseSqlServer("data source=(localhost); initial catalog=CURSO_EF_CORE; user id=sa; password=ps@#1346;");
         optionsBuilder.UseSqlServer("Server=127.0.0.1,1433;Database=CURSO_EF_CORE;User Id=sa;Password=ps@#1346;MultipleActiveResultSets=true");

        
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }
    }
}