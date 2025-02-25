using Microsoft.AspNetCore.Http.HttpResults;
using Project_Work_My_Telegram_bot.ClassDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;


namespace Project_Work_My_Telegram_bot
{
    /// <summary>
    /// Класс который хранит данные по клавиатурам для ТГБота виды InlineKeyboard и KeyboardMarkup
    /// </summary>
    public static class KeyBoardSetting
    {
        // Клапвиатура Start  
        public static ReplyKeyboardMarkup startkeyboard = new ReplyKeyboardMarkup(new[] { new KeyboardButton("Пользователь"), new KeyboardButton("Администратор") })
        {
            ResizeKeyboard = true
        };
        //Клавиатура произвольного типа сформированная по 2 кнопки аавтомобилей 
        public static KeyboardButton[][] GenerateKeyboard(List<CarDrive> buttonCarsData)
        {
            List<KeyboardButton[]> keyboard = new List<KeyboardButton[]>();

            for (int i = 0; i < buttonCarsData.Count; i += 2)
            {
                if (i + 1 < buttonCarsData.Count)
                {
                    keyboard.Add(new KeyboardButton[] { new KeyboardButton(buttonCarsData[i].CarName + " " + buttonCarsData[i].CarNumber),
                                                        new KeyboardButton(buttonCarsData[i + 1].CarName + " " + buttonCarsData[i + 1].CarNumber) });
                }
                else
                {
                    keyboard.Add(new KeyboardButton[] { new KeyboardButton(buttonCarsData[i].CarName + " " + buttonCarsData[i].CarNumber) });
                }
            }
            return keyboard.ToArray();
        }
        public static ReplyKeyboardMarkup GetReplyMarkup(List<CarDrive> buttonCarDrivesData)
        {
            var keyboard = GenerateKeyboard(buttonCarDrivesData);
            return new ReplyKeyboardMarkup(keyboard) { ResizeKeyboard = true };
        }

        //Метод формирования клавиатуры по списку строк 
        public static KeyboardButton[][] GenerateKeyboardTypeString(List<string> button)
        {
            List<KeyboardButton[]> keyboard = new List<KeyboardButton[]>();

            for (int i = 0; i < button.Count; i += 2)
            {
                if (i + 1 < button.Count)
                {
                    keyboard.Add(new KeyboardButton[] { new KeyboardButton(button[i] + " "), new KeyboardButton(button[i + 1]) });
                }
                else
                {
                    keyboard.Add(new KeyboardButton[] { new KeyboardButton(button[i]) });
                }
            }
            return keyboard.ToArray();
        }
        public static ReplyKeyboardMarkup GetReplyMarkupTypeString(List<string> button)
        {
            var keyboard = GenerateKeyboardTypeString(button);
            return new ReplyKeyboardMarkup(keyboard) { ResizeKeyboard = true };
        }

        // Клавиатура Main тип Юзер 
        public static KeyboardButton[][] keyboardUser =
        [
            ["👤 Профиль", "📚 Вывести отчет"],
            ["📝 Регистрация поездки", "💰 Регистрация трат"],
            ["Смена статуcа Admin/User"]
        ];
        public static ReplyKeyboardMarkup keyboardMainUser = new(keyboard: keyboardUser)
        {
            ResizeKeyboard = true,
        };
        // Клавиатура Main тип Администатор 
        public static KeyboardButton[][] keyboardAdmin =
        [
            ["👤 Установка пароля User", "💰 Стоимость бензина"],
            ["📝 Регистрация автопарка компании", "📚 Вывести отчет по User"],
            ["Смена статуcа Admin/User"]
        ];
        public static ReplyKeyboardMarkup keyboardMainAdmin = new(keyboard: keyboardAdmin)
        {
            ResizeKeyboard = true,
        };
        //Клавиатура выбора типа топлива 
        public static KeyboardButton[][] keyboardGasType =
        [
            ["🪫 ДТ", "🔋 AИ-95", "🔋 AИ-92"]
        ];
        public static ReplyKeyboardMarkup keyboardMainGasType = new(keyboard: keyboardGasType)
        {
            ResizeKeyboard = true,
        };
        //Клавиатура вывода отчета по пользователю тип Юзер 
        public static KeyboardButton[][] ReportUser =
        [
            ["📚 Отчет за текущий месяц", "💼 Отчет за выбранный месяц"],
            ["⬅️ Возврат в основное меню"]
        ];
        public static ReplyKeyboardMarkup keyboardReportUser = new(keyboard: ReportUser)
        {
            ResizeKeyboard = true,
        };

        // Инлайнер клапвиатура регистрации пользователя         
        public static InlineKeyboardMarkup profile = new(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "👤 Ф.И.О", callbackData: "username"),
                InlineKeyboardButton.WithCallbackData(text: "⛑ Должность", callbackData: " jobtitle"),
            },
             new []
             {
                InlineKeyboardButton.WithCallbackData(text: "🚗 Марка машины", callbackData: "carname"),
                InlineKeyboardButton.WithCallbackData(text: "🇷🇺 Госномер", callbackData: "carnumber"),
             },
              new []
              {
                InlineKeyboardButton.WithCallbackData(text: "Используемое топливо", callbackData: "typefuel"),
                InlineKeyboardButton.WithCallbackData(text: "Средний расход л.на 100 км. ", callbackData: "gasconsum"),
              },
              new []
              {
                  InlineKeyboardButton.WithCallbackData(text: "🕹 Закончить и сохраниеть", callbackData: "closed"),
                  InlineKeyboardButton.WithCallbackData(text: "⬅️", callbackData: "⬅️")
              }
        });
        public static InlineKeyboardMarkup GenerateInlineKeyboardByString(List<string> buttons)
        {
            buttons.Add("⬅️");
            List<List<InlineKeyboardButton>> keyboard = new List<List<InlineKeyboardButton>>();

            for (int i = 0; i < buttons.Count; i += 2)
            {
                if (i + 1 < buttons.Count)
                {
                    keyboard.Add(new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData(buttons[i], callbackData: buttons[i]),
                    InlineKeyboardButton.WithCallbackData(buttons[i + 1], callbackData: buttons[i + 1])
                });
                }
                else
                {
                    keyboard.Add(new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData(buttons[i], callbackData: buttons[i])
                });
                }
            }
            return new InlineKeyboardMarkup(keyboard);
        }
        // Инлайн клапвиатура регистрации пути следования User регистрация Пути следования
        public static InlineKeyboardMarkup regPath = new(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "🏢 Точка назначения", callbackData: "objectname")
            },
             new []
            {
                InlineKeyboardButton.WithCallbackData(text: "🏃‍♀️ Полный путь в, км", callbackData: "pathlengh")
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "🚗 Собственный трансопорт?", callbackData: "acceptisCar"),
                InlineKeyboardButton.WithCallbackData(text: "📆 Дата поездки", callbackData: "datepath"),
            },
            new []
             {
                InlineKeyboardButton.WithCallbackData(text: "🕹 Закончить и сохранить", callbackData: "closedpath"),
                 InlineKeyboardButton.WithCallbackData(text: "⬅️", callbackData: "⬅️")
             },
        });
     
        //Инлайн клапвиатура регистрации доп. завтрат 
        public static InlineKeyboardMarkup regCoast = new(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "Наименование затрат", callbackData: "namecost")
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "Сумма 00,00 руб", callbackData: "sumexpenses"),
                InlineKeyboardButton.WithCallbackData(text: "📆 Дата зтраты", callbackData: "dateexpenses"),
            },
             new []
             {
                InlineKeyboardButton.WithCallbackData(text: "🕹 Закончить и сохранить", callbackData: "ClosedExpenses"),
                InlineKeyboardButton.WithCallbackData(text: "⬅️", callbackData: "⬅️")
             }
        });
        
        public static InlineKeyboardMarkup regDriveCar = new(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "🚗 Марка машины", callbackData: "carname"),
                InlineKeyboardButton.WithCallbackData(text: "🇷🇺 Госномер", callbackData: "carnumber"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text:  "Расход на 100 км.", callbackData: "gasconsum"),
                InlineKeyboardButton.WithCallbackData(text:  "Используемое топливо", callbackData: "typefuel"),
            },
             new []
             {
                InlineKeyboardButton.WithCallbackData(text: "🕹 Закончить и сохранить", callbackData: "closedDrive"),
                InlineKeyboardButton.WithCallbackData(text: "⬅️", callbackData: "⬅️")
             }
        });
      
        public static ReplyKeyboardMarkup actionAccept = new ReplyKeyboardMarkup(new[] { new KeyboardButton("ДА"), new KeyboardButton("НЕТ") })
        {
            ResizeKeyboard = true
        };
        //Клавиатуры комманд подтверждения Обновить/Выйти 
        public static ReplyKeyboardMarkup updateAccept = new ReplyKeyboardMarkup(new[] { new KeyboardButton("Обновить"), new KeyboardButton("Выйти") })
        {
            ResizeKeyboard = true
        };

        public static InlineKeyboardMarkup regCoastFuel = new(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "💰 Стоимость 🔋 AИ-92", callbackData: "coastAi92")
            },
             new []
            {
                InlineKeyboardButton.WithCallbackData(text: "💰 Стоимость 🔋 AИ-95", callbackData: "coastAi95")
             },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "💰 Стоимость 🪫 ДТ ", callbackData: "coastDizel"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "⬅️", callbackData:"⬅️")
            }
        });
        public static InlineKeyboardMarkup RemoveButtonByCallbackData(InlineKeyboardMarkup keyboardMarkup, string callbackData)
        {
            var newKeyboard = keyboardMarkup.InlineKeyboard
                .Select(row => row
                    .Where(button => button.CallbackData != callbackData)
                    .ToList())
                .Where(row => row.Any())
                .ToList();

            return new InlineKeyboardMarkup(newKeyboard);
        }
    }
}
