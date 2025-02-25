using Microsoft.EntityFrameworkCore;
using Project_Work_My_Telegram_bot.ClassDB;
using Project_Work_My_Telegram_bot.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Work_My_Telegram_bot
{
    /// <summary>
    /// Класс формирования БД EF контект доступ и формирование миграций 
    /// </summary>
    public class ApplicationContext : DbContext
    {
        private PassUser _passUser = new PassUser();
        public DbSet<User> Users { get; set; }
        public DbSet<CarDrive> CarDrives{ get; set; }
        public DbSet<ObjectPath> ObjectPaths { get; set; }
        public DbSet<OtherExpenses> OtherExpenses { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_passUser.BdToken);           
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {       
            modelBuilder.ApplyConfiguration(new UserConfigurations());
            modelBuilder.ApplyConfiguration(new ObjectPathDriveConfigurations());
            modelBuilder.ApplyConfiguration(new CarDriveConfigurations());
            modelBuilder.ApplyConfiguration(new OtherExpensesConfigurations());
           
            base.OnModelCreating(modelBuilder);
        }
    }
}
