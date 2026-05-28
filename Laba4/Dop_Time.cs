using Time = (int hour, int min, int sec);
namespace Laba4
{
    public static class Dop_Time
    {
        
        static string day = TimeProgram.GetDayOfWeek();
        static string[] parts = line.Split('|');
        public static string path = "/home/yarolav/CSharpProjects/Laba4/Laba4/plan.txt";
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
            string [] lesson;
            string [] type_lesson;
            Lessons_count(out count, out lesson, out type_lesson);
            if (count == 0)
            {
                    Text.P("Today not lesson\n");
                    Text.P("Because it's a day off ");

            } 
            if (count == 1)
            {
                Text.P(
                    Text.TranslateText("Today only one lesson\n")+
                    Text.TranslateText("You cann`t calculate the time between classes during the day. ")
                    );
            }
            if (count > 2)
            {
                Text.P(Text.TranslateText("Today lesson")+":");
                for (int i = 1; i < count ; i++)
                {
                   Text.P($"{i}" + Text.TranslateText("Lesson") +", "+ Text.TranslateText("subject")+": "+Text.TranslateText(lesson[i-1])+
                   Text.TranslateText("lesson type") + ": " + Text.TranslateText(type_lesson[i-1])
                   ); 
                } 
                Text.P(Text.TranslateText("Choose")+ ":");
                string first_less= "";
                string second_less= "";
                Text.P("First lesson" + ":");
                Choose_less(out first_less, count);
                Text.P("Second lesson" + ":");
                Choose_less(out second_less, count);
                /*Якщо людина вибрала першу пару останню , а не навпаки */
                if (first_less[0] < second_less[0])
                {
                    string temp = "";
                    temp = first_less;
                    second_less = first_less;
                    first_less = temp;
                }
                else if (first_less[0] == second_less[0])
                {
                    Text.P(Text.TranslateText("You have chosen the same lesson\n")+
                    Text.TranslateText("Please, choose another lesson")
                    );
                    Choose_less(out first_less, count);
                    Choose_less(out second_less, count);
                }
                int one_day = "";
                int two_day = "";
                Need_time(first_less, out one_day);
                Need_time(second_less, out two_day);
                int res= 0 ; 
                TimeProgram.Difference(one_day,two_day,out res);
                string str_res = "";
                Time(res,out str_res);
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
           string [] events; 
           string [] time_events;
           string [] time_part;
           int count = 0;
           Count_the_day(day,out count);
           Big_Boss(day,out events,out time_events,out time_part);
        }
        static ConsoleKey [] key = new ConsoleKey[] { ConsoleKey.Q, ConsoleKey.W, ConsoleKey.E, ConsoleKey.R, ConsoleKey.T, ConsoleKey.Y, ConsoleKey.U, ConsoleKey.I };
        //Так Кутузов ця функція для щоб знайти дії , які відбувается за день  
        static void Big_Boss(out string[] events, out string[] time_events, out string[] time_part)
        {
            events = new string[count];
            time_events = new string[count];
            time_part = new string[count];
            int temp = 0;
            foreach (string line in File.ReadLines(path))
            {
                if(parts[0]== day);
            }
            }
        static void Count_the_day(out int count)
        {
            foreach (string line in File.ReadLines(path))
            {
                
                while(parts[0]== day)   
                {
                    count++;
                    
                }   
            }
        }
        static void Time(Time time, int res, out string str_res)
        {
            time.hour = res / 3600;
            if (time.hour > 0)
            {
                res = res - time.hour * 3600;
            }
            int min = res % 3600 ;
            min = min / 60;
            int sec = res % 60;

            if (time.hour == 0)
            {
                str_res = $"{min:D1}"+ " minutes:" + $"{sec:D2}"+ " seconds";
            }
            else if (time.min == 0)
            {
                str_res = $"{sec:D1}"+ " seconds";
            } 
            else
            str_res = $"{time.hour:D1}"+ Text.TranslateText(" hours") + $"{time.min:D2}"+ 
            Text.TranslateText(" minutes") + $"{time.sec:D2}"+ Text.TranslateText(" seconds");
        }
        static void Lessons_count(
        out int count,
        out string [] lesson, 
        out string [] type_lesson )
        {

            count = 0;
            int temp = -1;
            foreach (string line in File.ReadLines(path))
            {
                if (Text.TranslateText(parts[0]) == day && Text.TranslateText(parts[2]) == Text.TranslateText(practice) ||
                   Text.TranslateText(parts[2]) == Text.TranslateText(lecture) ||Text.TranslateText(parts[2]) == Text.TranslateText(seminar)
                )
                {
                    temp++;
                    count++;
                    lesson[temp] = parts[1];
                    type_lesson[temp] = parts[3]; 
                }
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
       
        static void Choose_less(out string less , int count,key[] keys)
        {
             
            for (int i = 0; i < count; i++)
                {
                    lessons[keys[i]] = Text.TranslateText("Lesson")+ $"{i + 1}";
                    Text.P($"{keys[i]} -"+ "Lesson"+ " {i + 1}");
                }

                ConsoleKeyInfo key = Console.ReadKey(true);
                
                if (lessons.ContainsKey(key.Key))
                {
                    less = lessons[key.Key];
                }
        }
        static void Need_time( string less,string str_time, out int time)
        {
            foreach (string line in File.ReadLines(path))
            {
                if(parts[0]==day && parts[2]== less)
                {
                    str_time = parts[4];
                } 
            }
            ConvertInSec(new Time(), str_time, out time);

        }
         /*Ця функція для того , щоб можно було зрозоміти в який час відбуватеся подія, точніш в який пробіжутку дня... 
         А хотя хз , я возможно удалю цю функцію */
        static string GetDayPart(string lang, int start, int end)
        {
            foreach (var part in dayParts)
                {
                    if (hour >= part.start && hour < part.end)
                    {
                        return Text.TranslateText(part.name);
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
    }
}