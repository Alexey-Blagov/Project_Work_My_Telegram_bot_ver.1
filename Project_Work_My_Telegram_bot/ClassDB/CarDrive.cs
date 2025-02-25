using Project_Work_My_Telegram_bot;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Work_My_Telegram_bot.ClassDB
{
    /// <summary>
    /// Класс сущнсотей Автопарка 
    /// CarId: Ключ БД 
    /// CarName: Марка машины
    /// isPersonalCar: true машина личная, false машина конторская или нет машины false 
    /// CarNumber: Номер машины по шаблону H 000 EE 150
    /// GasСonsum: Средний расход бензина на 100 км.пути
    /// TypeFuel Марка бензина для транспорта выбор из: ТД, АИ95, АИ92 из типа Enum 
    /// PersonalId: Данные пользователя по БД порсональный авто 
    /// UserPersonal: Экземпляр класса на кого зарегестрирована авто 
    /// ObjectPaths: Сптисок поездок классов пути
    /// </summary>
    public class CarDrive
        {
            public int CarId { get; set; }
            public string? CarName { get; set; }
            public bool isPersonalCar { get; set; } = false;
            public string? CarNumber { get; set; }
            public double? GasСonsum { get; set; } 
            public int TypeFuel { get; set; } = (int)Fuel.Non;
            public long? PersonalId {  get; set; }
            public User? UserPersonal { get; set; } 
            public List<ObjectPath> ObjectPaths { get; set; } = new();
        }
}
