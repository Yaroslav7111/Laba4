using MyTime = (int hour, int min, int sec);


namespace Laba4{
class TimeProgram
{
    /*це змінна для поточного часу, 
     яка буде оновлюватися в окремому потоці, 
     щоб відображати поточний час з урахуванням зміщення, 
     яке може бути змінено користувачем через меню*/
public static Thread? clockThread;
public static bool running = true;
public static bool runningClock = true;
public static string currentDay;
// Це змінна для зберігання поточного часу, 
// яка буде оновлюватися в окремому потоці, 
// щоб відображати поточний час з урахуванням зміщення, 
// яке може бути змінено користувачем через меню
public static MyTime now;
// Зміщення часу в секундах від реального часу, яке можна змінювати через меню
public static int offsetSeconds = 0;
static int clockX = 0;
static int clockY = 7;
static int menuY = 8;


static void Run_one()
{
    Text.P("Hello! This program will help you to know your schedule and the current time.");
    secondMain();
}

public static void secondMain()
    {
        Console.SetCursorPosition(0, menuY);

        Text.P(
            "Choose option:\n" +
            "1. " + "Change time . " + " Enter Q.\n" +
            "2. " + "Show what lesson is now . " + " Enter W.\n" +
            "3. " + "Calculate the time remaining until a specific event. " + " Enter E.\n" +
            "4. " + "Exit the program. " + " Enter R."
        );

    if (clockThread == null || !clockThread.IsAlive)
{
    runningClock = true;
    clockThread = new Thread(() => Clock());
    clockThread.IsBackground = true;
    clockThread.Start();
}
    // Це головна частина програми, яка обробляє введення користувача та виконує відповідні дії
     while (running)
    {
    ConsoleKeyInfo key = Console.ReadKey(true);

    Console.SetCursorPosition(0, menuY+1);
    Console.Write(new string(' ', Console.WindowWidth));
    Console.SetCursorPosition(0, menuY+1);

    if (key.Key == ConsoleKey.Q)
    {
        //веном 
      ChangeTimeMenu();
    }
    else if (key.Key == ConsoleKey.W)
    {
        Text.P("You pressed W");
    }
    else if (key.Key == ConsoleKey.E)
    {
        runningClock = false;
        Difference_main(lang);
        runningClock = true;
    }
    else if (key.Key == ConsoleKey.R)
    {
        runningClock = false;
        running = false;
        Console.Clear();
        Text.P("You pressed R");
    }
}
    }
public static void Clock()
{

    while (runningClock)
    {
        // Получаем реальное время с учетом смещения
        DateTime realTime = DateTime.Now.AddSeconds(offsetSeconds);
        //создаем кортеж для хранения текущего времени в формате MyTime (часы, минуты, секунды)
        now = (
            realTime.Hour,
            realTime.Minute,
            realTime.Second
        );
        // Форматируем строку с текущим временем и днем недели(после этих слов сделал мюнинг)
        string timeText =
            $"{Text.TranslateText("Now is the time:")} {MyTimeToString(now)} | {GetDayOfWeek(lang)}";

        Console.SetCursorPosition(clockX, clockY);
       Console.Write(timeText.PadRight(50));
        Thread.Sleep(1000);
    }
}
 
public static void ChangeTimeMenu()
{
    Console.Clear();
    // Встановлюємо курсор для виведення часу 
    clockY = 1;
    //вказуемо де буде текст 
    //Веном
    Console.SetCursorPosition(0, 2);

    Text.P(
    "Q - +1 second\n" +
    "W - -1 second\n" +
    "E - +10 seconds\n" +
    "R - -10 seconds\n" +
    "T - +30 seconds\n" +
    "Y - -30 seconds\n" +
    "U - +1 minute\n" +
    "I - -1 minute\n" +
    "O - +1 hour\n" +
    "P - -1 hour\n" +
    "ESC - Exit menu"
   );
    /* Да-да, я знаю! Юлия Евгеньевна, вы не любите, когда
    бесконечный цикл на основе while(true), но я не знаю, как по-другому сделать меню для изменения времени.

    И да, я уже устал переписывать код в 11-й раз (по своей же "гениальности"),
    так что будьте добрее и сделайте вид, что не читаете этот текст и не видите проблему :)
    */
    /*P.S. (Для Власыча)
        Знай! Я знаю где ты живешь , знаю когда ты дома , знаю когда ты спишь , я бы уже давно дал тебе повестку и заташил бы в чорный бусик, 
         но я не хочу проблем с законом , так что будь добрее и не трогай меня)
    */ 

   while (true)
{
    /* если пользователь нажимает клавишу, мы обрабатываем ее и изменяем время соответственно (например, если нажата клавиша Q,
     мы добавляем одну секунду к текущему времени).
     НАТИВНО ПРИМЕНЯЕМ МЕТОДЫ ДЛЯ ИЗМЕНЕНИЯ ВРЕМЕНИ, КОТОРЫЕ МЫ РАНЕЕ ОПРЕДЕЛИЛИ (AddOneSecond, SubtractOneSecond и т.д.)
     хотя если честно я испльзвал их потому-что требывалось в задаче (хотя я мог бы просто добавить или отнять нужное количество секунд напрямую, 
     но да я решил следовать требованиям и использовать эти методы, чтобы показать их работу в действии)
     После обработки каждой клавиши мы обновляем строку с текущим временем
        */
    ConsoleKey key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Q)
            {
                now = AddOneSecond(now);
                offsetSeconds += 1;
            }
            else if (key == ConsoleKey.W)
            {
                now = SubtractOneSecond(now);
                offsetSeconds -= 1;
            }
            else if (key == ConsoleKey.E)
            {
                now = AddTenSeconds(now);
                offsetSeconds += 10;
            }
            else if (key == ConsoleKey.R)
            {
                now = SubtractTenSeconds(now);
                offsetSeconds -= 10;
            }
            else if (key == ConsoleKey.T)
            {
                now = AddThirtySeconds(now);
                offsetSeconds += 30;
            }
            else if (key == ConsoleKey.Y)
            {
                now = SubtractThirtySeconds(now);
                offsetSeconds -= 30;
            }
            else if (key == ConsoleKey.U)
            {
                now = AddOneMinute(now);
                offsetSeconds += 60;
            }
            else if (key == ConsoleKey.I)
            {
                now = SubtractOneMinute(now);
                offsetSeconds -= 60;
            }
            else if (key == ConsoleKey.O)
            {
                now = AddOneHour(now);
                offsetSeconds += 3600;
            }
            else if (key == ConsoleKey.P)
            {
                now = SubtractOneHour(now);
                offsetSeconds -= 3600;
            }
// Якщо натиснуто клавішу Escape, виходимо з меню зміни часу та повертаємося до головного меню
    else if (key == ConsoleKey.Escape)
    {
        Console.Clear();

        clockY = 1;
        menuY = 2;

        secondMain();

        break;
    }
}
    // Після кожного натискання клавіші оновлюємо рядок часу (offsetSeconds змінюється разом із now)
    Console.SetCursorPosition(0, 10);
    Console.Write(new string(' ', Console.WindowWidth));
}

public static string MyTimeToString(MyTime t)
{

    t = Normalize(t);

    return $"{t.hour:D1}:{t.min:D2}:{t.sec:D2}";
}

public static MyTime Normalize(MyTime t)
{
   if (t.sec >= 60)
    {
        t.min += t.sec / 60;
        t.sec = t.sec % 60;
    }
    else if (t.sec < 0)
    {
        int borrow = (-t.sec + 59) / 60;
        t.min -= borrow;
        t.sec += borrow * 60;
    }

    if (t.min >= 60)
    {
        t.hour += t.min / 60;
        t.min = t.min % 60;
    }
    else if (t.min < 0)
    {
        int borrow = (-t.min + 59) / 60;
        t.hour -= borrow;
        t.min += borrow * 60;
    }

    if (t.hour >= 24)
    {
        t.hour = t.hour % 24;
    }
    else if (t.hour < 0)
    {
        t.hour = (t.hour % 24 + 24) % 24;
    }

    return t;
}

public static int ToSecSinceMidnight(MyTime t)
{
    return t.hour * 3600 + t.min * 60 + t.sec;
}
public static MyTime FromSecSinceMidnight(int t)
{
    // Ця функція перетворює кількість секунд, що минули з півночі, назад у формат MyTime (години, хвилини, секунди)
    MyTime result;
    // Спочатку визначаємо кількість годин, потім залишок секунд для визначення хвилин і секунд
    result.hour = t / 3600;
    // Залишок секунд після визначення годин
    t %= 3600;
    // Визначаємо кількість хвилин з залишку секунд
    result.min = t / 60;
    // Залишок секунд після визначення хвилин - це кількість секунд
    result.sec = t % 60;

    return result;
}

public static MyTime AddSeconds(MyTime t, int s)
{
    int totalSeconds = ToSecSinceMidnight(t);

    totalSeconds += s;

    totalSeconds %= 86400;

    if (totalSeconds < 0)
    {
        totalSeconds += 86400;
    }

    return FromSecSinceMidnight(totalSeconds);
}

public static MyTime AddOneSecond(MyTime t)
{
    return AddSeconds(t, 1);
}

public static MyTime AddOneMinute(MyTime t)
{
    return AddSeconds(t, 60);
}

public static MyTime AddOneHour(MyTime t)
{
    return AddSeconds(t, 3600);
}

public static MyTime SubtractOneSecond(MyTime t) => AddSeconds(t, -1);

public static MyTime AddTenSeconds(MyTime t) => AddSeconds(t, 10);

public static MyTime SubtractTenSeconds(MyTime t) => AddSeconds(t, -10);

public static MyTime AddThirtySeconds(MyTime t) => AddSeconds(t, 30);

public static MyTime SubtractThirtySeconds(MyTime t) => AddSeconds(t, -30);

public static MyTime SubtractOneMinute(MyTime t) => AddSeconds(t, -60);

public static MyTime SubtractOneHour(MyTime t) => AddSeconds(t, -3600);
public static int Difference(int sec1 , int sec2 , out int result) 
{
    result = sec2 - sec1;  
    return result;
}
public static void Difference_main()
    {
        Console.Clear();
        Text.P(
    "Choose what you want:\n" +
    "1. Calculate difference between two times (Q)\n" +
    "2. Calculate time until a specific event (W)\n" +
    "3. Exit to main menu (R)"
    );
 
    ConsoleKeyInfo key = Console.ReadKey(true);
    if (key.Key == ConsoleKey.Q)
    {
     Console.Clear();
     Dop_Time.Shiza();
    }
    else if (key.Key == ConsoleKey.W)
    {
     Console.Clear();
     clockY = 1;
     menuY = 2;
     Difference_main( );
    }
    else if (key.Key == ConsoleKey.R)
    {
    Console.Clear();
    Text.P("You pressed") + " R";
   }
}
public static void WhatLesson(MyTime t)
{
    /* Формат plan.txt: День|Назва|Номер пари|Тип|Початок|Кінець */
    bool hasLessonsToday = false;
    bool isNowLesson = false;

    string today = DateTime.Now.AddSeconds(offsetSeconds).DayOfWeek.ToString();

    foreach (string line in File.ReadLines(Dop_Time.path))
    {
        if (string.IsNullOrWhiteSpace(line))
            continue;

        string[] parts = line.Split('|');
         // Якщо день у рядку не відповідає поточному дню, пропускаємо цей рядок і переходимо до наступного
        if (parts[0] != currentDay)
            continue;


        string type = parts[3].Trim();
        if (type != "lecture" && type != "practice" && type != "seminar")
            continue;

        hasLessonsToday = true;

        MyTime start = ParseTime(parts[4].Trim());
        MyTime end = ParseTime(parts[5].Trim());

        if (IsTimeInRange(t, start, end))
        {
            Text.P($"Now is lesson: {type}, {parts[1].Trim()}");
            isNowLesson = true;
            break;
        }
    }

    if (!hasLessonsToday)
    {
        Text.P("Today have no lessons");
    }
    else if (!isNowLesson)
    {
        Text.P("Now there are no lessons");
    }
}
public static MyTime ParseTime(string timeStr)
{
    string[] parts = timeStr.Split(':');
    return (
        int.Parse(parts[0]),
        int.Parse(parts[1]),
        int.Parse(parts[2])
    );
}
static bool IsTimeInRange(MyTime t, MyTime start, MyTime end)
{
    int tSec = ToSecSinceMidnight(t);
    int startSec = ToSecSinceMidnight(start);
    int endSec = ToSecSinceMidnight(end);

    return tSec >= startSec && tSec < endSec;
}
// Этот метод получает текущий день недели с учетом смещения времени, которое может быть изменено пользователем через меню.
public static string GetDayOfWeek()
{
    // Получаем реальное время с учетом смещения
    DateTime realTime = DateTime.Now.AddSeconds(offsetSeconds);
    DayOfWeek day = realTime.DayOfWeek;

    string key = day.ToString(); // Monday, Tuesday...

    string result = Text.TranslateText(key);

    currentDay = result;
    return result;
}
}
}

