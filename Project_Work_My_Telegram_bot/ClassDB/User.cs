using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Telegram.Bot.Types;
using System.ComponentModel.DataAnnotations;

namespace Project_Work_My_Telegram_bot.ClassDB
{
    /// <summary>
    /// IdTg Ключ назначается телеграм ботом 
    /// TgUsername: Имя при регистрации в боте (может быть не указано от телеграм бота)
    /// UserRol: Идентификатор User права доступа 
    /// UserName: Имя ФИО пользователя 
    /// JobTitle: Должность в компании 
    /// Рersonalcar: Экземпляр персональной авто на данном User  
    /// ObjectPath Список объектов все пути связанные с юзером из БД  
    /// OtherExpenses: Свисок объектов все траты связанные с юзером из БД 
    /// </summary>
    public class User
    {
        public long IdTg { get; set; } 
        public string TgUserName { get; set; } = string.Empty; 
        public int UserRol { get; set; } = (int)UserType.Non;
        public string? UserName { get; set; }
        public string? JobTitlel { get; set; }
        public CarDrive? PersonalCar {  get; set; }
        public List<ObjectPath> ObjectPaths { get; set; } = new ();
        public List<OtherExpenses> OtherExpenses { get; set; } = new(); 
    }
}
