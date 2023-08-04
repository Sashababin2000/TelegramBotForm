using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;

namespace TelegramBot
{
    public class UserChat
    {
        public string? Username { get; set; }
        public string? Сallsign { get; set; }
        public string? Status { get; set; }
        public int? Age { get; set; }
        public string? Question_1 { get; set; }
        public string? Question_2 { get; set; }
        public string? Question_3 { get; set; }
        public string? Question_4 { get; set; }
        public string? Question_5 { get; set; }
        public long ChatId { get; set; }
    }
}
