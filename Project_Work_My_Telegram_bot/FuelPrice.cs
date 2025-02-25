using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project_Work_My_Telegram_bot
{
    /// <summary>
    /// Класс который получет информацию с URL и парсит ее в свои свойста стоимость бензина, храним в файлк Json поля меняются 
    /// через ТГ Бот по требованию Юзера тип роли Admin 
    /// </summary>
    public class FuelPrice
    {
        private static string? _filePath;
        public decimal Ai92 { get; set; }
        public decimal Ai95 { get; set; }
        public decimal Diesel { get; set; }

        public FuelPrice()
        {
            Task.Run(() => GetDataAsync()).Wait();
        }
        /// <summary>
        /// Метод сохранения данных в файл по стоимости топлива
        /// </summary>
        public void SaveToJson()
        {
            var jsonData = new
            {
                Ai92,
                Ai95,
                Diesel,
            };
            File.WriteAllText(_filePath!, System.Text.Json.JsonSerializer.Serialize(jsonData));
        }
        /// <summary>
        /// Метод выгрузки в поля класса данны из файла 
        /// </summary>
        public void LoadFromJson()
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FuelSetPrice.json");
            if (File.Exists(_filePath))
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("FuelSetPrice.json", optional: false, reloadOnChange: true)
                    .Build();
                Ai92 = ParsePrice(configuration["Ai92"]!);
                Ai95 = ParsePrice(configuration["Ai95"]!);
                Diesel = ParsePrice(configuration["Diesel"]!);
            }
            else
            {
                //Если файйл не создан то мы забираем по умолчанию данные с сайта 
                SaveToJson();
            }
        }
        /// <summary>
        /// Метод выгрузки с сайта данных по МСК региону о топливе записывается в поля класса при создании 
        /// </summary>
        /// <returns></returns>
        private async Task GetDataAsync()
        {
            var url = "https://card-oil.ru/fuel-cost/moskovskaya-oblast/";

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetStringAsync(url);

                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(response);

                    // Поиск строки, содержащей "Москва"
                    var moscowRow = htmlDoc.DocumentNode.SelectSingleNode("//tr[td[contains(text(), 'Москва')]]");

                    if (moscowRow != null)
                    {
                        var cells = moscowRow.SelectNodes("td");
                        if (cells != null && cells.Count >= 6)
                        {
                            Ai92 = ParsePrice(cells[1].InnerText);
                            Ai95 = ParsePrice(cells[3].InnerText);
                            Diesel = ParsePrice(cells[5].InnerText);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении данных: {ex.Message}");
                }
            }
        }
        /// <summary>
        /// Метод парсинга цены с исключением мусорных символов
        /// </summary>
        /// <param name="priceText"></param> входящая цена тип 
        /// <returns></returns> decimal Цена 
        private decimal ParsePrice(string priceText)
        {
            return decimal.TryParse(priceText.Replace(" руб.", "").Replace(",", "."),
                                    System.Globalization.CultureInfo.InvariantCulture,
                                    out var price)
                   ? price
                   : 0m;
        }
    }
}



