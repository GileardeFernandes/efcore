using System;
using System.Linq;
using EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFCore.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Cliente> Clientes {get; set;}
        public DbSet<Produto> Produtos {get; set;}
        public DbSet<Pedido> pedidos {get; set;}

        private static readonly ILoggerFactory _logger = LoggerFactory.Create(p=> p.AddConsole());
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

			//optionsBuilder.UseSqlServer("Server=127.0.0.1,1433;Database=CURSO_EF_CORE;User Id=sa;Password=ps@#1346;MultipleActiveResultSets=true");
			optionsBuilder.UseLoggerFactory(_logger)
						  .EnableSensitiveDataLogging()
						  .UseNpgsql("Host=localhost;Username=postgres;Password=local123;Database=efcore",
                          //por padrão o EnableRetryOnFailure tenta reconectar em caso de falhas por até 6x
                          p => p.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(5), errorCodesToAdd:null)
                          .MigrationsHistoryTable("tabela_migracoes"));//Alterando nome da tabela de histórioc de mirgrações padrão
                          
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
            MepearPropriedadesEsquecidas(modelBuilder);
        }
        protected void MepearPropriedadesEsquecidas(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                 var properties = entity.GetProperties().Where(p => p.ClrType == typeof(string));

                 foreach (var property in properties)
                 {
                     if(string.IsNullOrEmpty(property.GetColumnType()) || !property.GetMaxLength().HasValue){

                         //property.SetMaxLength(100);
                         property.SetColumnType("VARCHAR(100)");
                     }
                 }
            }
        }
    }
}