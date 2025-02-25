using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Work_My_Telegram_bot.ClassDB
{
    public class OtherExpenses
    {
        /// <summary>
        /// ExpId Ключ БД 
        /// NameExpense Наименование затрат
        /// Coast: Стоимость затрат decimal 
        /// dateTimeExp Дата трат 
        /// UserId Форинкей на юзера Юсера осуществивший траты
        /// UserExp: Екземплыр класса Юсера осуществивший траты
        /// </summary>
        public int ExpId { get; set; }
        public string? NameExpense { get; set; }
        public decimal? Coast { get; set; }
        public DateTime DateTimeExp { get; set; } = DateTime.MinValue; 
        public long? UserId { get; set; }
        public User? UserExp { get; set; }
    }
}
