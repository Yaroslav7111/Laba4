using System;
using System.Collections.Generic;
using System.Linq;

namespace Laba4
{
    public static class Text
    {
        
        public static string CurrentLang = "en";

      // Этот метод принимает строку, переводит ее на текущий язык и выводит на экран.
        public static void P(string text)
        {
            Console.WriteLine(TranslateText(text));
        }
        public static void PLine(string text)
        {
            Console.Write(TranslateText(text));
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
            {"Press any key to return...", "Нажмите любую клавишу для возврата..."},
                        // Program.cs
            {"Choose what you want:\n", "Выберите, что вы хотите:\n"},
            {"Choose: ", "Выберите: "},

            // ExOne.cs
            {"Choose option:\n", "Выберите опцию:\n"},
            {"Q - +1 second\n", "Q - +1 секунда\n"},
            {"\nPress any key to return...", "\nНажмите любую клавишу для возврата..."},

            // ExTwo.cs
            {"First-year students with a grade of 5 in physics:", "Первокурсники с оценкой 5 по физике:"},
            {"Students folder was not found.", "Папка со студентами не найдена."},
            {"\nНажмите любую клавишу...", "\nНажмите любую клавишу..."},
            {"Group: ", "Группа: "},
            {"Date of birth: ", "Дата рождения: "},
            {"Age: ", "Возраст: "},
            {"Physics: ", "Физика: "},
            {"Scholarship: ", "Стипендия: "},
            {" UAH.", " грн."},

            // Additional.cs и Born_s_d.cs
            {"Choose task:\n", "Выберите задание:\n"},
            {"1.Gigachad Women.\n", "1.Гигачад женщины.\n"},
            {"2.Born on the same day.", "2.Родились в один день."},
            {"3.Come back on main menu", "3.Вернуться в главное меню"},
            {"You select an invalid option. Please click another button!", "Вы выбрали неверный вариант. Нажмите другую кнопку!"},
            {"Same full date :", "Одинаковая полная дата:"},
            {"\nSame day:month :", "\nОдинаковые день и месяц:"},
            {"\nPress any key...", "\nНажмите любую клавишу..."},
            {"Group : ", "Группа: "},

            // Add_new_file.cs и Student_edit.cs
            {"║         STUDENT INFORMATION          ║", "║        ИНФОРМАЦИЯ О СТУДЕНТЕ        ║"},
            {"Name:", "Имя:"},
            {"Surname:", "Фамилия:"},
            {"Gender:", "Пол:"},
            {"Birth date (yyyy-mm-dd):", "Дата рождения (гггг-мм-дд):"},
            {"Course:", "Курс:"},
            {"Group:", "Группа:"},
            {"Subjects:", "Предметы:"},
            {"Average:", "Средний балл:"},
            {"Study:", "Форма обучения:"},
            {"Can edit: [Yes] [No]", "Можно редактировать: [Да] [Нет]"},
            {"Password: [Without] [With]", "Пароль: [Без] [С]"},
            {"Enter password:", "Введите пароль:"},
            {"You wanna save this student?", "Сохранить этого студента?"},
            {"Editable student files:", "Файлы студентов, доступные для редактирования:"},
            {"Male", "Мужской"},
            {"Female", "Женский"},
            {"Budget", "Бюджет"},
            {"Contract", "Контракт"},
            {"Yes", "Да"},
            {"No", "Нет"},
            {"Without", "Без"},
            {"With", "С"},

            // Dop_Time.cs
            {"No events today", "Сегодня нет событий"},
            {"Today events:", "События сегодня:"},
            {"\nChoose FIRST event:", "\nВыберите ПЕРВОЕ событие:"},
            {"Choose SECOND event:", "Выберите ВТОРОЕ событие:"},
            {"Same event selected", "Выбрано одно и то же событие"},
            {"Invalid choice. Try again.", "Неверный выбор. Попробуйте снова."},
            {"Invalid choice. Please try again.", "Неверный выбор. Пожалуйста, попробуйте снова."},
            {"Invalid choice.", "Неверный выбор."},
            {"hours", "часов"},
            {"minutes", "минут"},
            {"seconds", "секунд"},

            // GigachadW.cs и TextProcessing.cs
            {"\nPlease tap ather bottom...", "\nПожалуйста, нажмите другую кнопку..."},
            {"Choose the text processing method:", "Выберите метод обработки текста:"},
            {"1. Edit the student's file", "1. Редактировать файл студента"},
            {"2. Create a new file with the student's data", "2. Создать новый файл с данными студента"},
            {"3. Exit to the main menu", "3. Выйти в главное меню"},

            {"Math I", "Математика I"},
            {"Physics I", "Физика I"},
            {"Programming Basics", "Основы программирования"},
            {"Discrete Math", "Дискретная математика"},
            {"Math II", "Математика II"},
            {"OOP", "ООП"},
            {"Algorithms", "Алгоритмы"},
            {"Database", "Базы данных"},
            {"Web Dev", "Веб-разработка"},
            {"Operating Systems", "Операционные системы"},
            {"Advanced C#", "Продвинутый C#"},
            {"ASP.NET", "ASP.NET"},
            {"LINQ", "LINQ"},
            {"Software Engineering", "Программная инженерия"},
            {"AI Basics", "Основы ИИ"},
            {"Cybersecurity", "Кибербезопасность"},
            {"Diploma Project", "Дипломный проект"},
            {"Practice", "Практика"},
            {"System Design", "Проектирование систем"},
            {"Cloud Basics", "Основы облачных технологий"},
            {"DevOps", "DevOps"},
            {"Final Exam Prep", "Подготовка к выпускному экзамену"}
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
                        // Program.cs
            {"Choose what you want:\n", "Оберіть, що ви хочете:\n"},
            {"Choose: ", "Оберіть: "},

            // ExOne.cs
            {"Choose option:\n", "Виберіть опцію:\n"},
            {"Q - +1 second\n", "Q - +1 секунда\n"},
            {"\nPress any key to return...", "\nНатисніть будь-яку клавішу для повернення..."},

            // ExTwo.cs
            {"First-year students with a grade of 5 in physics:", "Першокурсники з оцінкою 5 з фізики:"},
            {"Students folder was not found.", "Папку зі студентами не знайдено."},
            {"\nНажмите любую клавишу...", "\nНатисніть будь-яку клавішу..."},
            {"Group: ", "Група: "},
            {"Date of birth: ", "Дата народження: "},
            {"Age: ", "Вік: "},
            {"Physics: ", "Фізика: "},
            {"Scholarship: ", "Стипендія: "},
            {" UAH.", " грн."},

            // Additional.cs і Born_s_d.cs
            {"Choose task:\n", "Оберіть завдання:\n"},
            {"1.Gigachad Women.\n", "1.Гігачад жінки.\n"},
            {"2.Born on the same day.", "2.Народилися в один день."},
            {"3.Come back on main menu", "3.Повернутися до головного меню"},
            {"You select an invalid option. Please click another button!", "Ви обрали неправильний варіант. Натисніть іншу кнопку!"},
            {"Same full date :", "Однакова повна дата:"},
            {"\nSame day:month :", "\nОднакові день і місяць:"},
            {"\nPress any key...", "\nНатисніть будь-яку клавішу..."},
            {"Group : ", "Група: "},

            // Add_new_file.cs і Student_edit.cs
            {"║         STUDENT INFORMATION          ║", "║        ІНФОРМАЦІЯ ПРО СТУДЕНТА      ║"},
            {"Name:", "Ім'я:"},
            {"Surname:", "Прізвище:"},
            {"Gender:", "Стать:"},
            {"Birth date (yyyy-mm-dd):", "Дата народження (рррр-мм-дд):"},
            {"Course:", "Курс:"},
            {"Group:", "Група:"},
            {"Subjects:", "Предмети:"},
            {"Average:", "Середній бал:"},
            {"Study:", "Форма навчання:"},
            {"Can edit: [Yes] [No]", "Можна редагувати: [Так] [Ні]"},
            {"Password: [Without] [With]", "Пароль: [Без] [З]"},
            {"Enter password:", "Введіть пароль:"},
            {"You wanna save this student?", "Зберегти цього студента?"},
            {"Editable student files:", "Файли студентів, доступні для редагування:"},
            {"Male", "Чоловіча"},
            {"Female", "Жіноча"},
            {"Budget", "Бюджет"},
            {"Contract", "Контракт"},
            {"Yes", "Так"},
            {"No", "Ні"},
            {"Without", "Без"},
            {"With", "З"},

            // Dop_Time.cs
            {"No events today", "Сьогодні немає подій"},
            {"Today events:", "Події сьогодні:"},
            {"\nChoose FIRST event:", "\nОберіть ПЕРШУ подію:"},
            {"Choose SECOND event:", "Оберіть ДРУГУ подію:"},
            {"Same event selected", "Обрано одну й ту саму подію"},
            {"Invalid choice. Try again.", "Неправильний вибір. Спробуйте ще раз."},
            {"Invalid choice. Please try again.", "Неправильний вибір. Будь ласка, спробуйте ще раз."},
            {"Invalid choice.", "Неправильний вибір."},
            {"hours", "годин"},
            {"minutes", "хвилин"},
            {"seconds", "секунд"},

            // GigachadW.cs і TextProcessing.cs
            {"\nPlease tap ather bottom...", "\nБудь ласка, натисніть іншу кнопку..."},
            {"Choose the text processing method:", "Оберіть метод обробки тексту:"},
            {"1. Edit the student's file", "1. Редагувати файл студента"},
            {"2. Create a new file with the student's data", "2. Створити новий файл із даними студента"},
            {"3. Exit to the main menu", "3. Вийти до головного меню"},

            // Предмети
            {"Math I", "Математика I"},
            {"Physics I", "Фізика I"},
            {"Programming Basics", "Основи програмування"},
            {"Discrete Math", "Дискретна математика"},
            {"Math II", "Математика II"},
            {"OOP", "ООП"},
            {"Algorithms", "Алгоритми"},
            {"Database", "Бази даних"},
            {"Web Dev", "Веб-розробка"},
            {"Operating Systems", "Операційні системи"},
            {"Advanced C#", "Поглиблений C#"},
            {"ASP.NET", "ASP.NET"},
            {"LINQ", "LINQ"},
            {"Software Engineering", "Програмна інженерія"},
            {"AI Basics", "Основи ШІ"},
            {"Cybersecurity", "Кібербезпека"},
            {"Diploma Project", "Дипломний проєкт"},
            {"Practice", "Практика"},
            {"System Design", "Проєктування систем"},
            {"Cloud Basics", "Основи хмарних технологій"},
            {"DevOps", "DevOps"},
            {"Final Exam Prep", "Підготовка до випускного іспиту"},

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