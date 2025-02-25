using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project_Work_My_Telegram_bot
{
    /// <summary>
    /// Класс Логирования информации в log.json
    /// </summary>
    public class LogerTgBot
    {
        private readonly string _logFilePath;

        public LogerTgBot()
        {
            _logFilePath =  Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log.json");
        }
        public LogerTgBot(string logFilePath)
        {
            _logFilePath = logFilePath;
        }
        /// <summary>
        /// Мтод логирования сообщений в файл с серилизацией 
        /// </summary>
        /// <param name="message"></param>
        public void LogMessage(object message)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };
                string jsonMessage = JsonSerializer.Serialize(message, options);

                // Логирование сообщения в файл
                File.AppendAllText(_logFilePath, $"{DateTime.Now}: {jsonMessage}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }
        /// <summary>
        /// Метод логирования исключений 
        /// </summary>
        /// <param name="ex"></param>
        public void LogException(Exception ex)
        {
            try
            {
                // Сериализация исключения в JSON
                var exceptionData = new
                {
                    ExceptionType = ex.GetType().ToString(),
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    InnerException = ex.InnerException?.ToString()
                };

                string jsonException = JsonSerializer.Serialize(exceptionData, new JsonSerializerOptions { WriteIndented = true });

                // Логирование исключения в консоль приложения
                File.AppendAllText(_logFilePath, $"{DateTime.Now}: {jsonException}{Environment.NewLine}");
            }
            catch (Exception logEx)
            {
                Console.WriteLine($"Ошибка Log: {logEx.Message}");
            }
        }
    }
}
