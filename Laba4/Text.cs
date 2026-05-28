using System;
using System.Collections.Generic;
using System.Linq;

namespace Laba4
{
    public static class Text
    {
        
        public static string CurrentLang = "en"; // default = English

      
        public static void P(string text)
        {
            Console.WriteLine(TranslateText(text));
        }

    
        public static string TranslateText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            Dictionary<string, string> dict;

            switch (CurrentLang)
            {
                case "ru":
                    dict = ruWords;
                    break;
                case "ua":
                    dict = uaWords;
                    break;
                default:
                    return text; // Тільки сігми використовоють English
            }

            foreach (var pair in dict.OrderByDescending(x => x.Key.Length))
            {
                text = text.Replace(pair.Key, pair.Value);
            }

            return text;
        }


        public static Dictionary<string, string> ruWords = new Dictionary<string, string>()
        {
            {"Monday", "Понедельник"},
            {"Tuesday", "Вторник"},
            {"Wednesday", "Среда"},
            {"Thursday", "Четверг"},
            {"Friday", "Пятница"},
            {"Saturday", "Суббота"},
            {"Sunday", "Воскресенье"},

            {"sleep", "Сон"},
            {"wake up", "Пробуждение"},
            {"breakfast", "Завтрак"},
            {"lunch", "Обед"},
            {"dinner", "Ужин"},

            {"coding", "Программирование"},
            {"training", "Тренировка"},
            {"free time", "Свободное время"},
            {"rest", "Отдых"},

            {"ride University", "Поездка в университет"},
            {"ride home", "Поездка домой"},
            {"ride", "Поездка"},

            {"Math", "Математика"},
            {"English", "Английский"},
            {"History", "История"},
            {"Physics", "Физика"},

            {"Now is the time:", "Сейчас время:"},
            {"Hello! This program will help you to know your schedule and the current time.",
             "Привет! Эта программа поможет вам узнать ваше расписание и текущее время."},

            {"Choose option:", "Выберите опцию:"},
            {"Enter", "Нажмите"},
            {"You pressed", "Вы нажали"},
            {"Press any key to return...", "Нажмите любую клавишу для возврата..."},
            {"Monday", "Понедельник"},
            {"Tuesday", "Вторник"},
            {"Wednesday", "Среда"},
            {"Thursday", "Четверг"},
            {"Friday", "Пятница"},
            {"Saturday", "Суббота"},
            {"Sunday", "Воскресенье"}

        };

        public static Dictionary<string, string> uaWords = new Dictionary<string, string>()
        {
            {"Monday", "Понеділок"},
            {"Tuesday", "Вівторок"},
            {"Wednesday", "Середа"},
            {"Thursday", "Четвер"},
            {"Friday", "П’ятниця"},
            {"Saturday", "Субота"},
            {"Sunday", "Неділя"},

            {"sleep", "Сон"},
            {"wake up", "Пробудження"},
            {"breakfast", "Сніданок"},
            {"lunch", "Обід"},
            {"dinner", "Вечеря"},

            {"coding", "Програмування"},
            {"training", "Тренування"},
            {"free time", "Вільний час"},
            {"rest", "Відпочинок"},

            {"ride University", "Поїздка до університету"},
            {"ride home", "Поїздка додому"},
            {"ride", "Поїздка"},

            {"Math", "Математика"},
            {"English", "Англійська"},
            {"History", "Історія"},
            {"Physics", "Фізика"},

            {"Now is the time:", "Зараз час:"},
            {"Hello! This program will help you to know your schedule and the current time.",
             "Привіт! Ця програма допоможе вам дізнатися ваш розклад та поточний час."},

            {"Choose option:", "Виберіть опцію:"},
            {"Enter", "Натисніть"},
            {"You pressed", "Ви нажали"},
            {"Press any key to return...", "Натисніть будь-яку клавішу для повернення..."}
        };
    }
}