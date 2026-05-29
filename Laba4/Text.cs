using System;
using System.Collections.Generic;
using System.Linq;

namespace Laba4
{
    public static class Text
    {
        
        public static string CurrentLang ; 

      // Этот метод принимает строку, переводит ее на текущий язык и выводит на экран.
        public static void P(string text)
        {
            Console.WriteLine(TranslateText(text));
        }

    // Этот метод переводит заданный текст на текущий язык, используя словари перевода.
    /*
    Ну если текст пустой или null, возвращаем его без изменений. Это нужно для предотвращения ошибок при попытке перевести пустой текст.
     Выбираем словарь перевода в зависимости от текущего языка. Если текущий язык
    */
        public static string TranslateText(string text)
        {
            // Если текст пустой или null, возвращаем его без изменений
            // Это нужно для предотвращения ошибок при попытке перевести пустой текст 
            //Вот эта билиберда  делает IsNullOrEmpty всю эту гразную работу 
            //веном
            if (string.IsNullOrEmpty(text))
                return text;
            // Выбираем словарь перевода в зависимости от текущего языка
            // Если текущий язык - русский, используем словарь ruWords,
            //  если украинский - uaWords (включается гимн Украины и появляется Император человечества Владимир Зеленский )     
            Dictionary<string, string> dict;
            // Если текущий язык не поддерживается, возвращаем текст без изменений
            switch (CurrentLang)
            {
                // В зависимости от текущего языка выбираем соответствующий словарь перевода
                case "ru":
                    dict = ruWords;
                    break;
                    // Если текущий язык - английский, не используем словарь, так как текст уже на английском
                case "ua":
                    dict = uaWords;
                    break;
                    // Если текущий язык - английский, не используем словарь, так как текст уже на английском
                default:
                    return text; // Тільки сігми використовоють English <- фу кто это написал, что эта за клоун
            }
                     // Проходим по всем парам ключ-значение в выбранном словаре и заменяем все вхождения ключа в тексте на соответствующее значение
            foreach (var pair in dict.OrderByDescending(x => x.Key.Length))
            {
                // Заменяем все вхождения ключа на значение, используя String.Replace
                text = text.Replace(pair.Key, pair.Value);
            }

            return text;
        }


        public static Dictionary<string, string> ruWords = new Dictionary<string, string>()
        {
            // Дни недели
            {"Monday", "Понедельник"},
            {"Tuesday", "Вторник"},
            {"Wednesday", "Среда"},
            {"Thursday", "Четверг"},
            {"Friday", "Пятница"},
            {"Saturday", "Суббота"},
            {"Sunday", "Воскресенье"},

            // События из plan.txt
            {"sleep", "Сон"},
            {"wake up", "Пробуждение"},
            {"breakfast", "Завтрак"},
            {"lunch", "Обед"},
            {"dinner", "Ужин"},
            {"doing housework", "Домашние дела"},
            {"wolk", "Прогулка"},
            {"getting ready for bed", "Подготовка ко сну"},
            {"relax ", "Отдых"},
            {"coding", "Программирование"},
            {"training", "Тренировка"},
            {"Training", "Тренировка"},
            {"free time", "Свободное время"},
            {"rest", "Отдых"},
            {"ride University", "Поездка в университет"},
            {"ride home", "Поездка домой"},
            {"ride", "Поездка"},
            {"Math", "Математика"},
            {"PALM", "ПАЛМ"},
            {"English", "Английский"},
            {"History", "История"},
            {"Physics", "Физика"},

            // Типы занятий и активностей
            {"lecture", "Лекция"},
            {"practice", "Практика"},
            {"seminar", "Семинар"},
            {"sport", "Спорт"},
            {"meal", "Приём пищи"},
            {"work", "Работа"},

            // Части дня
            {"Midnight", "Полночь"},
            {"Early morning", "Раннее утро"},
            {"Morning", "Утро"},
            {"Noon", "Полдень"},
            {"Afternoon", "День"},
            {"Evening", "Вечер"},
            {"Night", "Ночь"},
            {"Unknown", "Неизвестно"},

            // Program.cs
            {"Choose language: en - English, ru - Russian, ua - Ukrainian.", "Выберите язык: en - английский, ru - русский, ua - украинский."},
            {"Invalid language. Defaulting to English.", "Неверный язык. Используется английский."},
            {"Choose what you want:", "Выберите, что вы хотите:"},
            {"1 - Task 1", "1 - Задание 1"},
            {"2 - Task 2", "2 - Задание 2"},
            {"3 - Additional", "3 - Дополнительное"},
            {"4 - Text processing", "4 - Обработка текста"},
            {"Q - Quit", "Q - Выход"},

            // ExOne.cs
            {"Hello! This program will help you to know your schedule and the current time.",
             "Привет! Эта программа поможет вам узнать ваше расписание и текущее время."},
            {"Now is the time:", "Сейчас время:"},
            {"Choose option:", "Выберите опцию:"},
            {"Change time . ", "Изменить время. "},
            {"Show what lesson is now . ", "Показать текущую пару. "},
            {"Calculate the time remaining until a specific event. ", "Рассчитать время до события. "},
            {"Exit the program. ", "Выйти из программы. "},
            {" Enter Q.", " Нажмите Q."},
            {" Enter W.", " Нажмите W."},
            {" Enter E.", " Нажмите E."},
            {" Enter R.", " Нажмите R."},
            {"You pressed W", "Вы нажали W"},
            {"You pressed R", "Вы нажали R"},
            {"You pressed", "Вы нажали"},
            {"Q - +1 second", "Q - +1 секунда"},
            {"W - -1 second", "W - -1 секунда"},
            {"E - +10 seconds", "E - +10 секунд"},
            {"R - -10 seconds", "R - -10 секунд"},
            {"T - +30 seconds", "T - +30 секунд"},
            {"Y - -30 seconds", "Y - -30 секунд"},
            {"U - +1 minute", "U - +1 минута"},
            {"I - -1 minute", "I - -1 минута"},
            {"O - +1 hour", "O - +1 час"},
            {"P - -1 hour", "P - -1 час"},
            {"ESC - Exit menu", "ESC - Выход из меню"},
            {"Calculate difference between two times (Q)", "Разница между двумя моментами времени (Q)"},
            {"Calculate time until a specific event (W)", "Время до события (W)"},
            {"Exit to main menu (R)", "В главное меню (R)"},
            {"Now is lesson: ", "Сейчас идёт пара: "},
            {"Today have no lessons", "Сегодня пар нет"},
            {"Now there are no lessons", "Сейчас пар нет"},

            // Dop_Time.cs
            {"Press Q If you want to calculate the time between classes during the day.\n", "Нажмите Q, чтобы рассчитать время между парами в течение дня.\n"},
            {"Press W so that to calculate the time between various events.\n", "Нажмите W, чтобы рассчитать время между разными событиями.\n"},
            {"Press R so that come to back.\n", "Нажмите R, чтобы вернуться назад.\n"},
            {"Today not lesson\n", "Сегодня нет пар\n"},
            {"Because it's a day off ", "Потому что это выходной "},
            {"Today only one lesson\n", "Сегодня только одна пара\n"},
            {"You cann`t calculate the time between classes during the day. ", "Нельзя рассчитать время между парами в течение дня. "},
            {"Today lesson", "Пары сегодня"},
            {"Lesson", "Пара"},
            {"subject", "предмет"},
            {"lesson type", "тип пары"},
            {"Choose", "Выберите"},
            {"First lesson", "Первая пара"},
            {"Second lesson", "Вторая пара"},
            {"You have chosen the same lesson\n", "Вы выбрали одну и ту же пару\n"},
            {"Please, choose another lesson", "Пожалуйста, выберите другую пару"},
            {"Time between classes", "Время между парами"},
            {"Press any key to back", "Нажмите любую клавишу, чтобы вернуться"},
            {"Choose the event", "Выберите событие"},
            {" hours", " часов"},
            {" minutes", " минут"},
            {" minutes:", " минут:"},
            {" seconds", " секунд"},

            // ExTwo.cs, Additional.cs, TextProcessing.cs
            {"This is the second task. Here you can calculate the time between classes during the day and between various events.",
             "Это второе задание. Здесь можно рассчитать время между парами в течение дня и между разными событиями."},
            {"This is the additional task. Here you can perform various additional operations.",
             "Это дополнительное задание. Здесь можно выполнять различные дополнительные операции."},
            {"This is the text processing task. Here you can perform various text processing operations.",
             "Это задание по обработке текста. Здесь можно выполнять различные операции с текстом."},

            {"Enter", "Нажмите"},
            {"Press any key to return...", "Нажмите любую клавишу для возврата..."}
        };

        public static Dictionary<string, string> uaWords = new Dictionary<string, string>()
        {
            // Дни недели
            {"Monday", "Понеділок"},
            {"Tuesday", "Вівторок"},
            {"Wednesday", "Середа"},
            {"Thursday", "Четвер"},
            {"Friday", "П’ятниця"},
            {"Saturday", "Субота"},
            {"Sunday", "Неділя"},

            // События из plan.txt
            {"sleep", "Сон"},
            {"wake up", "Пробудження"},
            {"breakfast", "Сніданок"},
            {"lunch", "Обід"},
            {"dinner", "Вечеря"},
            {"doing housework", "Домашні справи"},
            {"wolk", "Прогулянка"},
            {"getting ready for bed", "Підготовка до сну"},
            {"relax ", "Відпочинок"},
            {"coding", "Програмування"},
            {"training", "Тренування"},
            {"Training", "Тренування"},
            {"free time", "Вільний час"},
            {"rest", "Відпочинок"},
            {"ride University", "Поїздка до університету"},
            {"ride home", "Поїздка додому"},
            {"ride", "Поїздка"},
            {"Math", "Математика"},
            {"PALM", "ПАЛМ"},
            {"English", "Англійська"},
            {"History", "Історія"},
            {"Physics", "Фізика"},

            // Типы занятий и активностей
            {"lecture", "Лекція"},
            {"practice", "Практика"},
            {"seminar", "Семінар"},
            {"sport", "Спорт"},
            {"meal", "Прийом їжі"},
            {"work", "Робота"},

            // Части дня
            {"Midnight", "Північ"},
            {"Early morning", "Ранній ранок"},
            {"Morning", "Ранок"},
            {"Noon", "Полудень"},
            {"Afternoon", "День"},
            {"Evening", "Вечір"},
            {"Night", "Ніч"},
            {"Unknown", "Невідомо"},

            // Program.cs
            {"Choose language: en - English, ru - Russian, ua - Ukrainian.", "Оберіть мову: en - англійська, ru - російська, ua - українська."},
            {"Invalid language. Defaulting to English.", "Невірна мова. Використовується англійська."},
            {"Choose what you want:", "Оберіть, що ви хочете:"},
            {"1 - Task 1", "1 - Завдання 1"},
            {"2 - Task 2", "2 - Завдання 2"},
            {"3 - Additional", "3 - Додаткове"},
            {"4 - Text processing", "4 - Обробка тексту"},
            {"Q - Quit", "Q - Вихід"},

            // ExOne.cs
            {"Hello! This program will help you to know your schedule and the current time.",
             "Привіт! Ця програма допоможе вам дізнатися ваш розклад та поточний час."},
            {"Now is the time:", "Зараз час:"},
            {"Choose option:", "Виберіть опцію:"},
            {"Change time . ", "Змінити час. "},
            {"Show what lesson is now . ", "Показати поточну пару. "},
            {"Calculate the time remaining until a specific event. ", "Розрахувати час до події. "},
            {"Exit the program. ", "Вийти з програми. "},
            {" Enter Q.", " Натисніть Q."},
            {" Enter W.", " Натисніть W."},
            {" Enter E.", " Натисніть E."},
            {" Enter R.", " Натисніть R."},
            {"You pressed W", "Ви натиснули W"},
            {"You pressed R", "Ви натиснули R"},
            {"You pressed", "Ви натиснули"},
            {"Q - +1 second", "Q - +1 секунда"},
            {"W - -1 second", "W - -1 секунда"},
            {"E - +10 seconds", "E - +10 секунд"},
            {"R - -10 seconds", "R - -10 секунд"},
            {"T - +30 seconds", "T - +30 секунд"},
            {"Y - -30 seconds", "Y - -30 секунд"},
            {"U - +1 minute", "U - +1 хвилина"},
            {"I - -1 minute", "I - -1 хвилина"},
            {"O - +1 hour", "O - +1 година"},
            {"P - -1 hour", "P - -1 година"},
            {"ESC - Exit menu", "ESC - Вихід з меню"},
            {"Calculate difference between two times (Q)", "Різниця між двома моментами часу (Q)"},
            {"Calculate time until a specific event (W)", "Час до події (W)"},
            {"Exit to main menu (R)", "У головне меню (R)"},
            {"Now is lesson: ", "Зараз іде пара: "},
            {"Today have no lessons", "Сьогодні пар немає"},
            {"Now there are no lessons", "Зараз пар немає"},

            // Dop_Time.cs
            {"Press Q If you want to calculate the time between classes during the day.\n", "Натисніть Q, щоб розрахувати час між парами протягом дня.\n"},
            {"Press W so that to calculate the time between various events.\n", "Натисніть W, щоб розрахувати час між різними подіями.\n"},
            {"Press R so that come to back.\n", "Натисніть R, щоб повернутися назад.\n"},
            {"Today not lesson\n", "Сьогодні пар немає\n"},
            {"Because it's a day off ", "Бо це вихідний "},
            {"Today only one lesson\n", "Сьогодні лише одна пара\n"},
            {"You cann`t calculate the time between classes during the day. ", "Неможливо розрахувати час між парами протягом дня. "},
            {"Today lesson", "Пари сьогодні"},
            {"Lesson", "Пара"},
            {"subject", "предмет"},
            {"lesson type", "тип пари"},
            {"Choose", "Оберіть"},
            {"First lesson", "Перша пара"},
            {"Second lesson", "Друга пара"},
            {"You have chosen the same lesson\n", "Ви обрали одну й ту саму пару\n"},
            {"Please, choose another lesson", "Будь ласка, оберіть іншу пару"},
            {"Time between classes", "Час між парами"},
            {"Press any key to back", "Натисніть будь-яку клавішу, щоб повернутися"},
            {"Choose the event", "Оберіть подію"},
            {" hours", " годин"},
            {" minutes", " хвилин"},
            {" minutes:", " хвилин:"},
            {" seconds", " секунд"},

            // ExTwo.cs, Additional.cs, TextProcessing.cs
            {"This is the second task. Here you can calculate the time between classes during the day and between various events.",
             "Це друге завдання. Тут можна розрахувати час між парами протягом дня та між різними подіями."},
            {"This is the additional task. Here you can perform various additional operations.",
             "Це додаткове завдання. Тут можна виконувати різні додаткові операції."},
            {"This is the text processing task. Here you can perform various text processing operations.",
             "Це завдання з обробки тексту. Тут можна виконувати різні операції з текстом."},

            {"Enter", "Натисніть"},
            {"Press any key to return...", "Натисніть будь-яку клавішу для повернення..."}
        };
    }
}