using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_Work_My_Telegram_bot;
using Project_Work_My_Telegram_bot.ClassDB;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Requests;


namespace Project_Work_My_Telegram_bot
{
    /// <summary>
    /// Клас работы с контекстом EF сущностей и запись в БД PGSQL 
    /// </summary>
    public class DataBaseHandler
    {
        // Методы получения данных их БД 
        /// <summary>
        /// Метод получения данных о пользователе 
        /// </summary>
        /// <param name="IdTg"></param> long IdTg 
        /// <returns></returns> класс User 
        /// <summary>
        public static async Task<User> GetUserAsync(long IdTg)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                User? user = await db.Users.FirstOrDefaultAsync(x => x.IdTg == IdTg);

                return user!;
            }
        }
        /// <summary>
        /// Метод получения данных для загестрированных пользователей если нет то Null 
        /// </summary>
        /// <param name="IdTg"></param>
        /// <returns></returns> CarDrive Класс 
        public static async Task<CarDrive?> GetPerconalCarDriveByUserAsync(long IdTg)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return await db.CarDrives.FirstOrDefaultAsync(c => c.PersonalId == IdTg && c.isPersonalCar);
            }
            
        }
        /// <summary>
        /// Метод получения списка автомашин автопарка гаража 
        /// </summary>
        /// <returns></returns> List <CarDrive> 
        public static async Task<List<CarDrive>> GetCarsDataListAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return await db.CarDrives
                    .AsNoTracking()
                    .Where(c => c.isPersonalCar == false)
                    .ToListAsync();
            }
        }
        /// <summary>
        /// Метод получения данных по Id Car из БД выбираем машину в поездку
        /// </summary>
        /// <param name="carId"></param> int ключ из БД 
        /// <returns></returns> Класс CarDrive
        public static async Task<CarDrive> GetCarDataForPathAsync(int? carId)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                CarDrive? car = await db.CarDrives.FirstOrDefaultAsync(x => x.CarId == carId);

                return car!;
            }
        }
        /// <summary>
        /// Метод получени информции по его роли из БД 
        /// </summary>
        /// <param name="IdTg"></param>
        /// <returns></returns> Возрат тип пользователя int 
        public static async Task<int> GetUserRoleAsync(long IdTg)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var user = await db.Users.FirstOrDefaultAsync(x => x.IdTg == IdTg);
                // случай если нет юзера в БД возврат Non 
                if (user == null) return 0;

                return user.UserRol;
            }
        }
        /// <summary>
        /// Метод получить автомашину из БД для пользователя по IdTg 
        /// </summary>
        /// <param name="IdTg"></param> long 
        /// <returns></returns> Класc CarDrive 
        
        // Методы записи данных в БД 
        /// <summary>
        /// Метод сохранения в БД информации о пути следования 
        /// </summary>
        /// <param name="newObjPath"></param> Сформированный класс 
        /// <returns></returns>
        public static async Task SetNewObjectPathAsync(ObjectPath newObjPath)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                try
                {
                    if (newObjPath is not null)
                    {
                        await db.AddAsync(newObjPath);
                        await db.SaveChangesAsync();
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }
        /// <summary>
        /// Метод внесения в БД машины сформированой и указаной в поле Профиль тип личный транспорт 
        /// </summary>
        /// <param name="newCarDrive"></param> Сформированный класс в БД 
        /// <returns></returns> true если данные успешно внесены
        public static async Task<bool> SetNewPersonalCarDriveAsync(CarDrive newCarDrive)
        {
            bool isset = false;
            using (ApplicationContext db = new ApplicationContext())
            {
                CarDrive? cardrive = await db.CarDrives.FirstOrDefaultAsync(c => c.PersonalId == newCarDrive.PersonalId);

                if (cardrive is null)
                {
                    await db.AddAsync(newCarDrive);
                    await db.SaveChangesAsync();
                    isset = true;
                }
                else isset = false;
            }
            return isset;
        }
        /// <summary>
        /// Метод записи данных по машинам в гараже комппании 
        /// </summary>
        /// <param name="newCarDrive"></param> Класс данных для сохранения 
        /// <returns></returns> true если данные внесены в БД 
        public static async Task<bool> SetNewCommercialCarDriveAsync(CarDrive newCarDrive)
        {
            bool isset = false;
            using (ApplicationContext db = new ApplicationContext())
            {

                //Поиск в БД по номеру если такая сущ выдаем false 
                CarDrive? cardrive = await db.CarDrives.FirstOrDefaultAsync(c => c.CarNumber == newCarDrive.CarNumber);

                if (cardrive is null)
                {
                    await db.AddAsync(newCarDrive);
                    await db.SaveChangesAsync();
                    isset = true;
                }
                else isset = false;
            }
            return isset;
        }
        /// <summary>
        /// Метод установки роли пользователя по tgId
        /// </summary>
        /// <param name="IdTg"></param> long IdTg
        /// <param name="role"></param> Type role 
        /// <returns></returns>
        public static async Task SetUserRoleAsync(long IdTg, int role)
        {

            using (ApplicationContext db = new ApplicationContext())
            {
                try
                {
                    var user = await db.Users.FirstOrDefaultAsync(x => x.IdTg == IdTg);
                    if (user is null)
                    {
                        User newuser = new User();
                        newuser!.UserRol = role;
                        newuser!.IdTg = IdTg;
                        await db.AddAsync(newuser);
                    }
                    else
                    {
                        user!.UserRol = role;

                    }
                    await db.SaveChangesAsync();
                }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            }
        }
        /// <summary>
        /// Метод обновления информции по личному транспорту в случае если в БД есть такая запись на Id 
        /// </summary>
        /// <param name="newCarDrive"></param>
        /// <returns></returns>
        /// 
        // Методы вобновления данных в БД 
        public static async Task UpdatePersonarCarDriveAsync(CarDrive newCarDrive)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                await db.CarDrives
                         .Where(c => c.PersonalId == newCarDrive.PersonalId)
                         .ExecuteUpdateAsync(s => s
                          .SetProperty(c => c.CarNumber, newCarDrive.CarNumber)
                          .SetProperty(c => c.CarName, newCarDrive.CarName)
                          .SetProperty(c => c.GasСonsum, newCarDrive.GasСonsum)
                          .SetProperty(c => c.TypeFuel, newCarDrive.TypeFuel));
            }
        }
        /// <summary>
        /// Метод обновления инфомрации по транспорту ГАРАЖ компании 
        /// </summary>
        /// <param name="newCarDrive"></param> Сформировный класс 
        /// <returns></returns>
        public static async Task UpdateNewCarDriveAsync(CarDrive newCarDrive)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                await db.CarDrives
                         .Where(c => c.CarNumber == newCarDrive.CarNumber)
                         .ExecuteUpdateAsync(s => s
                          .SetProperty(c => c.PersonalId, newCarDrive.PersonalId)
                          .SetProperty(c => c.isPersonalCar, newCarDrive.isPersonalCar)
                          .SetProperty(c => c.CarName, newCarDrive.CarName)
                          .SetProperty(c => c.GasСonsum, newCarDrive.GasСonsum)
                          .SetProperty(c => c.TypeFuel, newCarDrive.TypeFuel));
            }
        }
        /// <summary>
        /// Метод сохранения или обнволения информации User 
        /// </summary>
        /// <param name="newUser"></param> User класс 
        /// <returns></returns> 
        public static async Task SetOrUpdateUserAsync(User newUser)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var user = await db.Users.FirstOrDefaultAsync(x => x.IdTg == newUser.IdTg);

                if (user is null)
                {
                    db.Users.Add(newUser);
                    await db.SaveChangesAsync();
                }
                else
                {
                    await db.Users
                        .Where(c => c.IdTg == newUser.IdTg)
                        .ExecuteUpdateAsync(s => s
                         .SetProperty(c => c.UserName, newUser.UserName)
                         .SetProperty(c => c.TgUserName, newUser.TgUserName)
                         .SetProperty(c => c.JobTitlel, newUser.JobTitlel)
                         .SetProperty(c => c.UserRol, newUser.UserRol));
                }
            }
        }
        /// <summary>
        /// Метод записи данных в БД по затратам 
        /// </summary>
        /// <param name="expenses"></param> Сформированный класс OtherExpenses
        /// <returns></returns> 
        public static async Task SetNewExpensesAsync(OtherExpenses expenses)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.OtherExpenses.Add(expenses);
                await db.SaveChangesAsync();
            }
        }
    }
}