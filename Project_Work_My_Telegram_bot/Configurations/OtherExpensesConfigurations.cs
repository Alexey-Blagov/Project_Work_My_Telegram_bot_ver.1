using Microsoft.EntityFrameworkCore;
using Project_Work_My_Telegram_bot.ClassDB;

namespace Project_Work_My_Telegram_bot.Configurations
{
    /// <summary>
    /// Класс настрйоки миграций сущностей OtherExpenses
    /// </summary>
    public class OtherExpensesConfigurations : IEntityTypeConfiguration<OtherExpenses>

    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<OtherExpenses> builder)
        {
            builder.
                HasKey(a => a.ExpId);
            builder
              .Property(op => op.DateTimeExp)
              .HasColumnType("timestamp without time zone");
        }
    }
}