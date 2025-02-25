using Microsoft.EntityFrameworkCore;
using Project_Work_My_Telegram_bot;
using Project_Work_My_Telegram_bot.ClassDB;
using Telegram.Bot.Types;

namespace Project_Work_My_Telegram_bot.Configurations
{
    /// <summary>
    /// Класс настройки миграции CarDrive
    /// </summary>
    public class CarDriveConfigurations : IEntityTypeConfiguration<CarDrive>

    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CarDrive> builder)
        {
            builder.
                HasKey(a => a.CarId);
            builder.
                HasAlternateKey(u => u.CarNumber);
            builder.
                HasOne(u => u.UserPersonal).
                WithOne(c => c.PersonalCar).
                HasForeignKey<CarDrive>(c => c.PersonalId);
        }
    }
}

