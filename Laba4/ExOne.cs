using MyTime = (int hour, int min, int sec);


namespace Laba4{
class TimeProgram
{
public static Thread? clockThread;
public static bool running = true;
public static bool runningClock = true;
public static string currentDay;
public static MyTime now;
// Зміщення часу в секундах від реального часу, яке можна змінювати через меню
public static int offsetSeconds = 0;
static int clockX = 0;
static int clockY = 7;
static int menuY = 8;


static void ExOne()
{
    Console.Clear();
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
    clockThread = new Thread(() => Clock(lang));
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
        DateTime realTime = DateTime.Now.AddSeconds(offsetSeconds);

        now = (
            realTime.Hour,
            realTime.Minute,
            realTime.Second
        );

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

    clockY = 1;

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

   while (true)
{
    // Читаємо клавішу від користувача без відображення її в консолі
    ConsoleKey key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Q) offsetSeconds += 1;
            else if (key == ConsoleKey.W) offsetSeconds -= 1;
            else if (key == ConsoleKey.E) offsetSeconds += 10;
            else if (key == ConsoleKey.R) offsetSeconds -= 10;
            else if (key == ConsoleKey.T) offsetSeconds += 30;
            else if (key == ConsoleKey.Y) offsetSeconds -= 30;
            else if (key == ConsoleKey.U) offsetSeconds += 60;
            else if (key == ConsoleKey.I) offsetSeconds -= 60;
            else if (key == ConsoleKey.O) offsetSeconds += 3600;
            else if (key == ConsoleKey.P) offsetSeconds -= 3600;
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
    // Після кожного натискання клавіші, оновлюємо вивід часу, щоб відобразити зміни, які були внесені через зміщення часу (offsetSeconds)
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
public static int Difference(int sec1 , int sec2 , out int result) 
{
    result = sec2 - sec1;  
    return result;
}
public static void Difference_main(string lang )
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
    Console.WriteLine(Translate("You pressed", lang) + " R");
   }
}

public static string WhatLesson(MyTime t )
{
    return "";
}
public static string GetDayOfWeek()
{
    DateTime realTime = DateTime.Now.AddSeconds(offsetSeconds);
    DayOfWeek day = realTime.DayOfWeek;

    string key = day.ToString(); // Monday, Tuesday...

    string result = Text.TranslateText(key);

    currentDay = result;
    return result;
}
}
}

