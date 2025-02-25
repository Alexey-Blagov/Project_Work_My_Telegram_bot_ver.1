using Microsoft.EntityFrameworkCore;
using Polly;
using Project_Work_My_Telegram_bot.ClassDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Work_My_Telegram_bot
{
  /// <summary>
  /// Репозиторий данных для формирования отчетов 
  /// </summary>
    public class RepositoryReportMaker
    {
        private readonly ApplicationContext _reportDb;

        public RepositoryReportMaker(ApplicationContext reportDb)
        {
            _reportDb = reportDb;
        }
        /// <summary>
        /// Метод формирования анонимного типа данных с необходимыми полями 
        /// </summary>
        /// <param name="tgId"></param> long tgId
        /// <param name="startDate"></param> Дата начала отчета
        /// <param name="endDate"></param> Дата окончания отчета 
        /// <returns></returns> UserName, ObjectName, PathLengh, CarName, CarNumber, GasConsum, TypeFuel 
        public async Task<List<object>> GetUserObjectPathsByTgId(long tgId, DateTime startDate, DateTime endDate)
        {
            var result = await _reportDb.Users
                .AsNoTracking()
                .Where(u => u.IdTg == tgId)
                .Select(u => new
                {
                     UserName = u.UserName,
                     ObjectPaths = u.ObjectPaths
                    .Where(op => op.DatePath >= startDate && op.DatePath <= endDate)
                    .OrderBy(op => op.DatePath)
                    // Фильтрация по дате
                    .Select(op => new
                    {
                        ObjectName = op.ObjectName,
                        PathLengh = op.PathLengh,
                        DatePath = op.DatePath,
                        CarName = op.CarDrive != null ? op.CarDrive.CarName : null,
                        CarNumber = op.CarDrive != null ? op.CarDrive.CarNumber : null,
                        GasConsum = op.CarDrive != null ? op.CarDrive.GasСonsum : null,
                        TypeFuel = op.CarDrive != null ? op.CarDrive.TypeFuel : 2

                    })
                    .ToList()
                })
        .ToListAsync();
            return result.Cast<object>().ToList();
        }
        /// <summary>
        /// Метод формирования затрат выводит анониммный тип 
        /// </summary>
        /// <param name="tgId"></param> long Id 
        /// <param name="startDate"></param> Дата стартового отчета 
        /// <param name="endDate"></param> Дата конечного отчета 
        /// <returns></returns> NameExpense, Coast, DateTimeExp
        public async Task<List<object>> GetUserExpensesByTgId(long tgId, DateTime startDate, DateTime endDate)
        {
            var result = await _reportDb.Users
                 .AsNoTracking()
                 .Where(u => u.IdTg == tgId)
                 .Select(u => new
                 {
                     UserName = u.UserName,
                     OtherExpenses = u.OtherExpenses
                     .Where(oe => oe.DateTimeExp >= startDate && oe.DateTimeExp <= endDate)
                     .OrderBy(oe => oe.DateTimeExp)
                     // Фильтрация по дате
                     .Select(oe => new
                     {
                         NameExpense = oe.NameExpense,
                         Coast = oe.Coast,
                         DateTimeExp = oe.DateTimeExp
                     })
                     .ToList()
                 }).ToListAsync();
            return result.Cast<object>().ToList();
        }
        /// <summary>
        /// Метод вывода анонимного Типа данных с полями всех пользователей
        /// </summary>
        /// <returns></returns> UserId long,  UserName string
        public async Task<List <dynamic>> GetListUsersByTgIdAsync()
        {
            var result = await _reportDb.Users
                .AsNoTracking()
                .Where(u => u.UserName != null)
                .OrderBy(u => u.UserName)
                .Select(u => new
                {
                    UserId = u.IdTg,
                    UserName = u.UserName

                }).ToListAsync(); 
            return result.Cast<dynamic>().ToList(); ;
    
        }
    }
}