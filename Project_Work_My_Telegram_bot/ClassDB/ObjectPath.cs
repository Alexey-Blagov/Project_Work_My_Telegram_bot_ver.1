using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Work_My_Telegram_bot.ClassDB
{
    /// <summary>
    /// IdPath Id ключ пути 
    /// ObjectName Наименование пункта назначения (string)
    /// PathLeng: Дина пути с учетом обратного пути 
    /// DatePath: Дата поездки 2 в формате ДД.ММ.ГГГГ 
    /// CarId: Форинкей Id на связь с CarDrive осуществивший поездку 
    /// CarDrive: Екземпляр класса машины осуществившей поездку
    /// UserId: Форинкей на юзера Юсера осуществивший поездку 
    /// UserPath: Екземпяр класса Юсера осуществивший поездку
    /// </summary>
    public class ObjectPath
    {
        public int IdPath { get; set; }
        public string? ObjectName { get; set; }
        public double? PathLengh { get; set; } = null; 
        public DateTime DatePath { get; set; } = DateTime.MinValue;
        public int? CarId { get; set; } 
        public CarDrive? CarDrive { get; set; }
        public long? UserId { get; set; }
        public User? UserPath { get; set; }
    }
}
