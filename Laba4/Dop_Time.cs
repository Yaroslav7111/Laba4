using Time = (int hour, int min, int sec);
namespace Laba4
{
    public static class Dop_Time
    {
        static string[] parts = Array.Empty<string>();
        public static string path = Path.Combine(AppContext.BaseDirectory, "plan.txt");
        static ConsoleKey [] key = new ConsoleKey[] 
        { ConsoleKey.Q, 
        ConsoleKey.W, 
        ConsoleKey.E, 
        ConsoleKey.R, 
        ConsoleKey.T, 
        ConsoleKey.Y, 
        ConsoleKey.U, 
        ConsoleKey.I };
        // Эту фукцию так назвал 
        // И бо я уже за******ся придумывать названия для функций
        // И переписывать код в 14 РАЗ 
        public static void Shiza()
        {
             
            Text.P("Press Q If you want to calculate the time between classes during the day.\n");
            Text.P("Press W so that to calculate the time between various events.\n");
            Text.P("Press R so that come to back.\n");
            
            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Q)
            {
                Console.Clear();
                Calcu_bet_less();
            }
            else if (key.Key == ConsoleKey.W)
            {
                Console.Clear();
                Calcu_bet_events();
            }
            else if (key.Key == ConsoleKey.R)
            {
                Console.Clear();
                TimeProgram.Difference_main();
            }
        }
        static void Calcu_bet_less()
        {
            Console.Clear();
            int count = 0;
            // а тут инетресно , мне Давид придевил что код не будет оптимезирован , 
            // потому-что я что бы указать размер массива написал одельную функцию 
            // и это действиьельно не оптимально , но я так сделал для того , чтобы не создавать кучу переменных и массивов ,
            List<string> lesson = new List<string>();
            List<string> type_lesson = new List<string>();
            Lessons_count(out count, out lesson, out type_lesson);
            if (count == 0)
            {
                    Text.P("Today not lesson\n");
                    Text.P("Because it's a day off ");

            } 
            // Блин чай хлебнул а он оказался гарячим , и я сильно обжегся ,
            //  и я не могу нормально писать код, веном 
            else if (count == 1)
            {
                Text.P(
                    Text.TranslateText("Today only one lesson\n")+
                    Text.TranslateText("You cann`t calculate the time between classes during the day. ")
                    );
            }
            else if (count >= 2)
            {
                Text.P(Text.TranslateText("Today lesson") + ":");

                    for (int i = 0; i < count; i++)
                    {
                        Text.P(
                            $"{i + 1} " +
                            Text.TranslateText("Lesson") + ", " +
                            Text.TranslateText("subject") + ": " +
                            Text.TranslateText(lesson[i]) + ", " +
                            Text.TranslateText("lesson type") + ": " +
                            Text.TranslateText(type_lesson[i])
                        );
                    }
                Text.P(Text.TranslateText("Choose")+ ":");
                string first_less= "";
                string second_less= "";
                Text.P("First lesson" + ":");
                Choose_less(out first_less, count);
                Text.P("Second lesson" + ":");
                Choose_less(out second_less, count);
                /*Якщо людина вибрала першу пару останню , а не навпаки  */
                if (first_less[0] > second_less[0])
                {
                    string temp = first_less;
                    first_less = second_less;
                    second_less = temp;
                }
                else if (first_less[0] == second_less[0])
                {
                    Text.P(Text.TranslateText("You have chosen the same lesson\n")+
                    Text.TranslateText("Please, choose another lesson")
                    );
                    Choose_less(out first_less, count);
                    Choose_less(out second_less, count);
                }
                int one_day = 0;
                int two_day = 0;
                Need_time(first_less, out one_day);
                Need_time(second_less, out two_day);
                int res= 0 ; 
                TimeProgram.Difference(one_day,two_day,out res);
                string str_res = "";
                Time(default, res, out str_res);
                Text.P(Text.TranslateText("Time between classes")+": "+ str_res);
                Text.P(Text.TranslateText("Press any key to back"));
                Console.ReadKey();
                Console.Clear();
                Shiza();
            }
            
        }
        static void Calcu_bet_events()
        {
           Console.Clear();
           Text.P(Text.TranslateText("Choose the event"));
           string today = GetToday();
           Big_Boss(today, out List<string> events, out List<string> time_events, out _);
           for (int i = 0; i < events.Count; i++)
           {
               Text.P($"{i + 1}. {Text.TranslateText(events[i])} ({time_events[i]})");
           }
           Text.P(Text.TranslateText("Press any key to back"));
           Console.ReadKey();
           Console.Clear();
           Shiza();
        }
        //Так Кутузов ця функція для щоб знайти дії , які відбувается за день  
            static void Big_Boss(
            string today,
            out List<string> events,
            out List<string> startTimes,
            out List<string> endTimes)
        {
            events = new List<string>();
            startTimes = new List<string>();
            endTimes = new List<string>();

            foreach (string line in File.ReadLines(path))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                parts = line.Split('|');

                if (parts.Length < 6 || parts[0].Trim() != today)
                    continue;

                events.Add(parts[1].Trim());
                startTimes.Add(parts[4].Trim());
                endTimes.Add(parts[5].Trim());
            }
        }
   
        static void Time(Time time, int res, out string str_res)
            {
                time.hour = res / 3600;
                res %= 3600;

                time.min = res / 60;
                time.sec = res % 60;

                if (time.hour > 0 &&
                    time.min == 0 &&
                    time.sec == 0)
                {
                    str_res =
                        $"{time.hour} " +
                        Text.TranslateText("hours");
                }
                else if (time.hour == 0 &&
                        time.min > 0)
                {
                    str_res =
                        $"{time.min} " +
                        Text.TranslateText("minutes");

                    if (time.sec > 0)
                    {
                        str_res +=
                            $" {time.sec} " +
                            Text.TranslateText("seconds");
                    }
                }
                else if (time.hour > 0)
                {
                    str_res =
                        $"{time.hour} " +
                        Text.TranslateText("hours");

                    if (time.min > 0)
                    {
                        str_res +=
                            $" {time.min} " +
                            Text.TranslateText("minutes");
                    }

                    if (time.sec > 0)
                    {
                        str_res +=
                            $" {time.sec} " +
                            Text.TranslateText("seconds");
                    }
                }
                else
                {
                    str_res =
                        $"{time.sec} " +
                        Text.TranslateText("seconds");
                }
            }
        static void Lessons_count(
        out int count,
        out List<string> lesson, 
        out List<string> type_lesson )
        {
            lesson = new List<string>();
            type_lesson = new List<string>();
            count = 0;
            string today = GetToday();
            foreach (string line in File.ReadLines(path))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                parts = line.Split('|');
                if (parts.Length < 6 || parts[0].Trim() != today)
                    continue;

                /* хотя зачем тут Trim я не знаю , но пусть будет , может пригодится в будущем
                Ну на сдучей если какой-то Власыч-колбасыч придерётся, шо код не оптимальный , 
                то я ему скажу , что это для того , чтобы не было проблем с пробелами в файле , 
                 и вообще я так хочу , а ты кто по жизни , 
                 чтобы мне указывать как писать код? */
                string type = parts[3].Trim();
                if (type != "lecture" && type != "practice" && type != "seminar")
                    continue;

                count++;
                lesson.Add(parts[1].Trim());
                type_lesson.Add(type);
            }  
        }
    
      static void ConvertInSec(Time time, string timeStr, out int sum)
        {
            string[] parts = timeStr.Split(':');

            time.hour = int.Parse(parts[0]);
            time.min = int.Parse(parts[1]);
            time.sec = int.Parse(parts[2]);

            sum = time.hour * 3600 + time.min * 60 + time.sec;
        }
       
        static void Choose_less(out string less , int count)
        {
            less = "";
            for (int i = 0; i < count && i < key.Length; i++)
            {
                Text.P($"{key[i]} - {Text.TranslateText("Lesson")} {i + 1}");
            }

            while (string.IsNullOrEmpty(less))
            {
                ConsoleKeyInfo pressed = Console.ReadKey(true);
                int index = Array.IndexOf(key, pressed.Key);
                if (index >= 0 && index < count)
                    less = $"{index + 1} lesson";
                else
                    Text.P("Invalid choice. Please try again.");
            }
        }
        static void Need_time(string less, out int time)
        {
            string str_time = "";
            string today = GetToday();
            int selectedLesson = int.Parse(less.Split(' ')[0]);
            int currentLesson = 0;

            foreach (string line in File.ReadLines(path))
            {
                parts = line.Split('|');
                if (parts.Length < 6 || parts[0].Trim() != today)
                    continue;

                string type = parts[3].Trim();
                if (type != "lecture" && type != "practice" && type != "seminar")
                    continue;

                currentLesson++;
                if (currentLesson == selectedLesson)
                {
                    str_time = parts[4].Trim();
                    break;
                }
            }

            if (string.IsNullOrWhiteSpace(str_time))
            {
                time = 0;
                return;
            }

            ConvertInSec(default, str_time, out time);
        }
         //Ця функція для того , щоб можно було зрозоміти в який час відбуватеся подія, точніш в який пробіжутку дня...
         //Наприклад , якщо подія відбувається в 5 годин ранку , то це буде "Early morning" , якщо в 14 годин , то це буде "Afternoon" і так далі
         //хз для чого це я пішу , але мені здається це буде цікаво і корисно для когось , хто буде читати цей код
         // Слава Імператору  
        static void ChooseEvent(out int index, int count)
            {
            index = -1;

            for (int i = 0; i < count && i < key.Length; i++)
            {
                Text.P($"{key[i]} - Event {i + 1}");
            }

            while (index == -1)
            {
                ConsoleKeyInfo pressed = Console.ReadKey(true);

                int temp = Array.IndexOf(key, pressed.Key);

                if (temp >= 0 && temp < count)
                {
                    index = temp;
                }
                else
                {
                    Text.P("Invalid choice.");
                }
            }
            }
         static Dictionary<ConsoleKey, string> lessons = new Dictionary<ConsoleKey, string >()
        {
            { ConsoleKey.Q, "1 lesson" },
            { ConsoleKey.W, "2 lesson" },
            { ConsoleKey.E, "3 lesson" },
            { ConsoleKey.R, "4 lesson" },
            { ConsoleKey.T, "5 lesson" },
            { ConsoleKey.Y, "6 lesson" },
            { ConsoleKey.U, "7 lesson" },
            { ConsoleKey.I, "8 lesson" }
        };
        /*Коротко говоря это массив в котором хранятся временные промежутки дня , 
        и используются кортеж начало и конец 
        я это сделал для того если событее происходит в разных промижутках дня , 
        то можно было понять в какой части дня происходит событие ,
         а не просто знать время , например 5 часов утра , а так будет понятно что это 
         "Early morning". 
         А кортеж я использовал для того , чтобы было удобно хранить эти промежутки и не создавать кучу переменных...Ауф!
        */
       static (int start, int end, string name)[] dayParts =
    {
        (0, 4, "Midnight"),
        (4, 6, "Early morning"),
        (6, 12, "Morning"),
        (12, 13, "Noon"),
        (13, 17, "Afternoon"),
        (17, 21, "Evening"),
        (21, 24, "Night") 
    };
    static string GetToday()
    {
        return DateTime.Now.AddSeconds(TimeProgram.offsetSeconds).DayOfWeek.ToString();
    }

    //Жестко ,с характером ,как мужчина,это функция парсирует
    static int ParseHour(string time)
    {
        var parts = time.Split(':');
        return int.Parse(parts[0]);
    }
    }
}