using OfficeOpenXml;
using Project_Work_My_Telegram_bot.ClassDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Work_My_Telegram_bot
{
    /// <summary>
    /// Класс формирования файлов для записи отчетов в ТГБот 
    /// </summary>
    public class FileExcelHandler
    {
        private string? _filePath;
        private string? _outputPath;
       

        private int typeFuel;
        private decimal priceOfFuel;
        //При обращении в обработчика класса читаем цены на топливо 
        private FuelPrice? _fuelPrice = new FuelPrice(); 

        public FileExcelHandler()
        {
            //Хранение формирующего файла в базовой дериктории 
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Template.xlsx");
        }
        public string ExportUsersToExcel(List<dynamic> dataPath, List<dynamic> dataExpenses, DateTime monthReport)
        {
            // Инициализация EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Открываем шаблон Excel
            using (var package = new ExcelPackage(new FileInfo(_filePath!)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                decimal sumCoastPath = 0m;
                decimal sumCoastExpenses = 0m;
                int row = 2;

                foreach (var datp in dataPath)
                {
                    decimal coastGasOnPath;
                    var nameUser = (string)datp.UserName ?? "Нет данных пользователя";
                    DateTime getdate = DateTime.Now.Date; 

                    //Первая сторка в файле 
                    worksheet.Cells[row, 1].Value = nameUser + $"Отчет, поездки за  {monthReport.ToString("MMMM yyyy")} г." + "\n"; 

                    row++;
                    //Создаем имя выходного файла  
                    _outputPath = (_outputPath is null) ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nameUser + monthReport.ToString("MMMM yyyy") + ".xlsx") : _outputPath;

                    foreach (var path in datp.ObjectPaths)
                    {
                        getdate = path.GetType().GetProperty("DatePath")?.GetValue(path);
                        string? objectName = path.GetType().GetProperty("ObjectName")?.GetValue(path).ToString();
                        double pathLengh = path.GetType().GetProperty("PathLengh")?.GetValue(path) ?? 0;   
                        string strData = getdate.ToShortDateString();
                        string? carName = path.GetType().GetProperty("CarName")?.GetValue(path).ToString();
                        string? carNumber = path.GetType().GetProperty("CarNumber")?.GetValue(path).ToString();
                        Fuel _fuel = (Fuel)(path.GetType().GetProperty("TypeFuel")?.GetValue(path) ?? 2);
                        double gasConsume = path.GetType().GetProperty("GasConsum")?.GetValue(path) ?? 0.0;
                        priceOfFuel = GetPriceFuel(_fuel);
                        coastGasOnPath = priceOfFuel * (decimal)gasConsume * (decimal)pathLengh / 100m;
                        sumCoastPath += coastGasOnPath;
                        worksheet.Cells[row, 1].Value = objectName ?? "Нет данных";
                        worksheet.Cells[row, 2].Value = strData ?? "Нет данных";
                        worksheet.Cells[row, 3].Value = carName ?? "Нет данных";
                        worksheet.Cells[row, 4].Value = carNumber ?? "Нет данных";
                        worksheet.Cells[row, 5].Value = pathLengh.ToString("F1") ?? "Нет данных";
                        worksheet.Cells[row, 6].Value = coastGasOnPath.ToString("F2") + "руб.";
                        row++;
                    }
                    worksheet.Cells[row, 1].Value = "Итого топливо: ";                                
                    worksheet.Cells[row, 6].Value = (sumCoastPath == 0m) ? "нет данных по тратам" : sumCoastPath.ToString("F2") + "руб.";
                    row++;
                }
                // Выводим данные по затратам 
                foreach (var expens in dataExpenses[0].OtherExpenses)
                {
                    DateTime getdate = expens.GetType().GetProperty("DateTimeExp")?.GetValue(expens);
                    string? nameExpense = expens.GetType().GetProperty("NameExpense")?.GetValue(expens).ToString();
                    decimal coast = expens.GetType().GetProperty("Coast")?.GetValue(expens) ?? 0m;

                    worksheet.Cells[row, 1].Value = nameExpense ?? "Нет данных";
                    worksheet.Cells[row, 2].Value = getdate.ToShortDateString() ?? "Нет данных";
                    worksheet.Cells[row, 6].Value = coast.ToString("F2") + "руб." ?? "Нет данных";
                    sumCoastExpenses += coast;
                    row++;
                }
                worksheet.Cells[row, 1].Value = "Итого затраты сумма: ";
                worksheet.Cells[row, 6].Value = (sumCoastPath == 0m) ? "нет данных по тратам" : sumCoastExpenses.ToString("F2") + "руб.";
                // Сохраняем изменения в новый файл
                package.SaveAs(new FileInfo(_outputPath!));
                return _outputPath!;
            }
        } 
        private decimal GetPriceFuel(Fuel fuel)
        {
             //Читаем данные из файла метод чтения из файла 
            _fuelPrice!.LoadFromJson();
            switch (fuel)
            {
                case Fuel.ai92:
                    return _fuelPrice.Ai92;

                case Fuel.ai95:
                    return _fuelPrice.Ai95;

                case Fuel.dizel:
                    return _fuelPrice.Diesel;
            };
            return 0m;
        }
    }
}
