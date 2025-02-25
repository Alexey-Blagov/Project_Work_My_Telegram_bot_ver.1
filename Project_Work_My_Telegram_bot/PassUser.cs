using Microsoft.Extensions.Configuration;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Work_My_Telegram_bot
{
    /// <summary>
    /// Класс чтения из файла данных по поролям запись в User  
    /// </summary>
    internal class PassUser
    {
        private string? _passwordUser;
        private string? _passwordAdmin;
        private readonly string _filePath;
        private string? _token;
        private string? _bdToken;
        public string PasswordUser
        {
            get
            {
                return _passwordUser!;
            }
            private set
            {
                _passwordUser = value;
            }
        }
        public string PasswordAdmin
        {
            get
            {
                return _passwordAdmin!;
            }
            private set
            {
                _passwordAdmin = value;
            }
        }

        public string Token
        {
            get
            {
                return _token!;
            }
        }
        public string BdToken
        {
            get
            {
                return _bdToken!;
            }
        }

        public PassUser()
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "userPass.json");
            try
            {
                LoadFromJson();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private void LoadFromJson()
        {
            if (File.Exists(_filePath))
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("userPass.json", optional: false, reloadOnChange: true)
                    .Build();
                _passwordUser = configuration["PasswordUser"] ?? throw new InvalidDataException("Поле PasswordUser отсутствует в файле.");
                _passwordAdmin = configuration["PasswordAdmin"] ?? throw new InvalidDataException("Поле PasswordAdmin отсутствует в файле.");
                _token = configuration["Token"] ?? throw new InvalidDataException("Поле Token отсутствует в файле.");
                _bdToken = configuration["BdToken"] ?? throw new InvalidDataException("Поле BdToken отсутствует в файле.");
            }
            else
            {
                SaveToJson();
            }
        }
        private void SaveToJson()
        {
            var jsonData = new
            {
                PasswordUser,
                PasswordAdmin,
                Token,
                BdToken
            };
            File.WriteAllText(_filePath, System.Text.Json.JsonSerializer.Serialize(jsonData));
        }
        public void UpdatePasswordsAdmin(string passAdmin)
        {
            _passwordAdmin = passAdmin;
            Console.WriteLine($"Введен и сохранен новый пароль для администратора: {passAdmin}");
            SaveToJson();
            Console.WriteLine("Пароли успешно обновлены и сохранены в файл. ");
        }
        public void UpdatePasswordsUser(string passUser)
        {
            _passwordUser = passUser;
            Console.WriteLine($"Введен и сохранен новый пароль для администратора: {passUser}");
            SaveToJson();
            Console.WriteLine("Пароли успешно обновлены и сохранены в файл. ");
        }
    }
}
