using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project_Work_My_Telegram_bot.ClassDB;
using Project_Work_My_Telegram_bot;

namespace Project_Work_My_Telegram_bot.Configurations
{
    /// <summary>
    /// Класс настройки миграции сущнсотей User
    /// </summary>
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder.
                HasKey(a => a.IdTg);
            builder.
                HasMany(u => u.ObjectPaths). 
                WithOne(p => p.UserPath).
                HasForeignKey(c => c.UserId);
            builder. 
                HasMany(u => u.OtherExpenses). 
                WithOne(e => e.UserExp).
                HasForeignKey(e => e.UserId);
        }
    }
}