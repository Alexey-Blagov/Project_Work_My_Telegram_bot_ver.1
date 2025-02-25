using Telegram.Bot.Polling;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace Project_Work_My_Telegram_bot
{
    public enum UserType {Non = 0, User = 1, Admin = 2 };
    public enum Fuel { Non = 0, dizel = 1, ai95 = 2, ai92 = 3 };
    internal class Program
    {
        private static string? _token;
        private static TelegramBotClient? _myBot;
        private static LogerTgBot? _loger; 
        private static CancellationTokenSource? _cts;
        private static MessageProcessing? _messageProcessing;
       
        //Точка входа в программу 
        static async Task Main(string[] args)
        {
            _loger = new LogerTgBot();
            
            PassUser passUser = new PassUser();
            _token = passUser.Token;
            
            _cts = new CancellationTokenSource();
            _myBot = new TelegramBotClient(_token!, cancellationToken: _cts.Token);

            _messageProcessing = new MessageProcessing(_myBot);
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<Telegram.Bot.Types.Enums.UpdateType>()
            };

            var me = await _myBot.GetMe();

            _loger!.LogMessage(new
            {
                information = $"Бот запущен {me.Username!.ToString()}, в {DateTime.Now.ToShortTimeString()}"
            });
            Console.WriteLine($" Бот запущен: {me.Username}");

            //Подписка на обработку методов TG
            _myBot.OnError += OnError;
            _myBot.OnMessage += OnMessage;
            _myBot.OnUpdate += OnUpdate;

            Console.WriteLine($"@{me.Username} ... нажмите Escape для остановки"); 

            while (Console.ReadKey(true).Key != ConsoleKey.Escape) ;
            _cts.Cancel(); // Остановка 
            _loger!.LogMessage(new
            {
                information = $"Бот остановлен {me.Username!}, в {DateTime.Now.ToShortTimeString()}"
            });
        }
        /// <summary>
        /// Метод обработки ошибок ТГБота 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        private static async Task OnError(Exception exception, HandleErrorSource source)
        {
            _loger!.LogException(exception); 
            Console.WriteLine(exception);
        }
        /// <summary>
        /// Мтеод работы с типом UPdate из ТГБота
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        private static async Task OnUpdate(Update update)
        {
            try
            {
                _loger!.LogMessage(new
                {
                  chatId = update!.CallbackQuery!.Id.ToString(),
                    data = update!.CallbackQuery.Data
                }); 
                //обработка Update 
                switch (update)
                {
                    case { CallbackQuery: { } callbackQuery }:

                        await _messageProcessing!.BotClientOnCallbackQueryAsync(callbackQuery);
                        break;
                    default:
                        Console.WriteLine($"Получена необработанный Update {update.Type}");
                        break;
                };
            }
            catch (Exception ex)
            {
                await OnError(ex, HandleErrorSource.PollingError);
            }
        }
        /// <summary>
        /// Метод обработки сообщений Message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static async Task OnMessage(Message message, UpdateType type)
        {
            var chatId = message.Chat.Id;
            var messageText = message.Text;
            var me = await _myBot!.GetMe();
            if (messageText == null) return;
            Console.WriteLine($"Received text '{message.Text}' in {message.Chat}");
            //Блок обработки сообщений 
            try
            {
                _loger!.LogMessage(new
                {
                    chatId = message.Chat.Id.ToString(),
                    text = messageText,
                    data = message.ToString()
                }); 
                if (messageText is not { } text)
                    Console.WriteLine($"Получено сообщение {message.Type}");
                else if (text.StartsWith('/'))
                {
                    var space = text.IndexOf(' ');
                    if (space < 0) space = text.Length;
                    var command = text[..space].ToLower();
                    if (command.LastIndexOf('@') is > 0 and int at) // it's a targeted command
                        if (command[(at + 1)..].Equals(me.Username, StringComparison.OrdinalIgnoreCase))
                            command = command[..at];
                        else
                            return; // command was not targeted at me

                    //обработчик комманд
                    await _messageProcessing.OnCommandAsync(command, text[space..].TrimStart(), message);
                }
                else
                {
                    //Обработчик сообщений 
                    await _messageProcessing!.OnTextMessageAsync(message);
                }
            }
            catch (Exception ex)
            {
                await OnError(ex, HandleErrorSource.PollingError);
            }
        }
    }
}
