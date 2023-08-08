using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using TelegramBot;
using System.Threading;

namespace TelegramBotForm
{
    public class TgBot
    {
        public delegate void TgBotHandler(string message);
        public event TgBotHandler? Notify;

        private TelegramBotClient botClient;
        public List<UserChat> users;

        public async void Start()
        {

            botClient = new TelegramBotClient("6178728774:AAHsZblR0F5szqVNpQGtlvzFcO-sFOnJrOY");
            CancellationTokenSource cts = new CancellationTokenSource();
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };
            User me = await botClient.GetMeAsync();
            Notify?.Invoke($"{me.Username} запущен!");
            botClient.StartReceiving(HandleUpdateAsync, HandlePollingErrorAsync, receiverOptions, cts.Token);
            Notify?.Invoke($"Start listening for @{me.Username}");

        }

// Добавить rigeons
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update.Message != null && update.Message.Text != null)
            {
                #region[response processing]
                var message = update.Message.Text;
                var us = users.Where(x => x.ChatId == update.Message.Chat.Id).FirstOrDefault();
                if (us == null)
                {
                    Notify?.Invoke($"Написал неизвестный @{update.Message.Chat.Id}");
                    await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Кто вы такие? Я вас не звал! Идите к Адмистратору!");
                }
                else
                {
                    if (us.Status == null)
                    {
                        await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Укажите варинт ответа нажав кнопку");
                        List<UserChat> stupidusers = new List<UserChat>();
                        stupidusers.Add(us);
                        StartQuestionnaire(stupidusers);
                    }
                    else if (us.Status == "name_waiting")
                    {                        
                        us.Username = message;
                        us.Status  = "callsign_waiting";
                        users[users.IndexOf(us)] = us;
                        Notify?.Invoke($"{message} @{update.Message.Chat.Id}");
                        await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Спасибо. Напишите ваш позывной");
                    }
                    else if (us.Status == "callsign_waiting")
                    {
                        us.Сallsign = message;
                        us.Status = "age_waiting";
                        users[users.IndexOf(us)] = us;
                        Notify?.Invoke($"{message} @{update.Message.Chat.Id}");
                        var text1 = "Спасибо. Сколько вам полных лет?";              
                        await botClient.SendTextMessageAsync(update.Message.Chat.Id, text1);
                    }

                    else if (us.Status == "age_waiting")
                    {
                        bool isNumeric = message.All(char.IsDigit);
                        if (isNumeric)
                        {
                            us.Age = Convert.ToInt16(message);
                            us.Status = "question_1";
                            users[users.IndexOf(us)] = us;
                            Notify?.Invoke($"{message} @{update.Message.Chat.Id}");
                            var text1 = "Как часто вы посещаете страйкбольные игры?";
                            ReplyKeyboardMarkup ikm = new(new[]
                      {
                            new KeyboardButton[] { "Раз в неделю", "Два раза в месяц", "Раз в месяц" },
                            new KeyboardButton[] { "Несколько раз в сезон","Ехжу только на большие игры" },
                        })
                            {
                                ResizeKeyboard = true,
                                OneTimeKeyboard = true
                            };
                            await botClient.SendTextMessageAsync(update.Message.Chat.Id, text1, replyMarkup: ikm);
                        }
                        else 
                        {
                            us.Status = "age_waiting";
                            var text1 = "Введите, пожалуйста, число";
                            await botClient.SendTextMessageAsync(update.Message.Chat.Id, text1);

                        }
                    }


                    else if (us.Status == "question_1")
                    {
                        us.Question_1 = message;
                        us.Status = "question_2";
                        users[users.IndexOf(us)] = us;
                        Notify?.Invoke($"{message} @{update.Message.Chat.Id}");
                        var text1 = "За какую сторону предпочитаете играть ?";                     
                        ReplyKeyboardMarkup ikm = new(new[]
                        {
                            new KeyboardButton[] { "Темная", "Светлая", "И на темной и на светлой" },
                        })
                        {
                            ResizeKeyboard = true,
                            OneTimeKeyboard = true
                        };
                        await botClient.SendTextMessageAsync(update.Message.Chat.Id, text1, replyMarkup: ikm);
                    }


                    else if (us.Status == "question_2")
                    {
                        us.Question_2 = message;
                        us.Status = "question_3";
                        users[users.IndexOf(us)] = us;
                        Notify?.Invoke($"{message} @{update.Message.Chat.Id}");
                        var text1 = "Напишите название вашего основного привода";          
                        await botClient.SendTextMessageAsync(update.Message.Chat.Id, text1);
                    }


                    else if (us.Status == "question_3")
                    {
                        us.Question_3 = message;
                        us.Status = "question_4";
                        users[users.IndexOf(us)] = us;
                        Notify?.Invoke($"{message} @{update.Message.Chat.Id}");
                        var text1 = "Какой камуфляж вы используете?";            
                        await botClient.SendTextMessageAsync(update.Message.Chat.Id, text1);
                    }

                    else if (us.Status == "question_4")
                    {
                        us.Question_4 = message;
                        us.Status = "question_5";
                        users[users.IndexOf(us)] = us;
                        Notify?.Invoke($"{message} @{update.Message.Chat.Id}");
                        var text1 = "Какие шары вы используете на основном приводе?";
                        ReplyKeyboardMarkup ikm = new(new[]
                        {
                            new KeyboardButton[] { "0,2", "0,25", "0,3" },
                            new KeyboardButton[] { "0,36", "0,4", "0,43" },
                        })
                        {
                            ResizeKeyboard = true,
                            OneTimeKeyboard = true

                        };
                        await botClient.SendTextMessageAsync(update.Message.Chat.Id, text1, replyMarkup: ikm);
                    }


                    else if (us.Status == "question_5")
                    {
                        us.Question_5 = message;
                        us.Status = null;
                        users[users.IndexOf(us)] = us;
                        Notify?.Invoke($"{message} @{update.Message.Chat.Id}");
                        var text1 = "Спасибо. Анкетирование закончено";
                        var ikm = new InlineKeyboardMarkup(new[]
                       {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Начать заново", "Restart"),
                                        },
                                    });
                        await botClient.SendTextMessageAsync(update.Message.Chat.Id, text1, replyMarkup: ikm);
                    }

                    
                }
                #endregion
            }
            if (update.Type == UpdateType.CallbackQuery && update.CallbackQuery != null && update.CallbackQuery.Data != null)
            {
                #region[action processing]
                var message = update.CallbackQuery.Data;
                UserChat? us = users.Where(x => x.ChatId == update.CallbackQuery.From.Id).FirstOrDefault();
                if (us == null)
                {
                    await botClient.SendTextMessageAsync(update.CallbackQuery.From.Id, "Кто вы такие? Я вас не звал! Идите к Адмистратору!");
                }
                else
                {
                    if (us.Status == null)
                    {
                        switch (message)
                        {
                            case "Yes":
                                us.Status = "name_waiting";
                                users[users.IndexOf(us)] = us;
                                Notify?.Invoke($"Согласие @{update.CallbackQuery.From.Id}");
                                await botClient.SendTextMessageAsync(update.CallbackQuery.From.Id, "Отлично! Давайте начнем. Напишите Ваше имя.");
                                break;

                            case "No":
                                us.Status = null;
                                users[users.IndexOf(us)] = us;
                                Notify?.Invoke($"Отказ @{update.CallbackQuery.From.Id}");
                                var text1 = "Можете пройти опрос в другое время";
                                var ikm = new InlineKeyboardMarkup(new[]
                               {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Начать заново", "Restart"),
                                        },
                                    });
                                await botClient.SendTextMessageAsync(update.CallbackQuery.From.Id, text1, replyMarkup: ikm);
                                break;

                            case "Later":
                                var text = "Хорошо. Нажмите \"Начать\" когда будете готовы.";

                                var ikm1 = new InlineKeyboardMarkup(new[]
                                {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Начать", "Yes"),
                                        },
                                    });
                                Notify?.Invoke($"Отложено @{update.CallbackQuery.From.Id}");
                                await botClient.SendTextMessageAsync(update.CallbackQuery.From.Id, text, replyMarkup: ikm1);
                                break;
                            case "Restart":
                                us.Status = null;
                                users[users.IndexOf(us)] = us;
                                Notify?.Invoke($"Перезапуск @{update.CallbackQuery.From.Id}");
                                StartQuestionnaire(users);
                                break;

                        }
                    }
                }
                #endregion
            }

        }

   


        static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception,
            CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
        public async void StartQuestionnaire(List<UserChat> userslist)
        {
            users = userslist;
            foreach (UserChat user in users)
            {
                if(user.Status == null)
                {
                    var text = "Здравствуйте! Можете пройти небольшой опрос?";
                    
                    var ikm = new InlineKeyboardMarkup(new[]
                    {
                    new[]
                        {
                             InlineKeyboardButton.WithCallbackData("Да", "Yes"),
                        },
                    new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Позже", "Later"),
                        },
                    new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Отказываюсь", "No"),
                    },
                    });
                    
                    Notify?.Invoke($"Отправлено @{user.ChatId}");
                    await botClient.SendTextMessageAsync(user.ChatId, text, replyMarkup: ikm);
                   
                }                
            }
        }
    }
}
