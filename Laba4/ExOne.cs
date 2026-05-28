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
    // коротко говаря что тут за колбасня творится  
    /*пофакту метод работает пока runningClock = true, если что runningClock глобальный 
     для вас Юлия Евгеньевна , я сделаю вид что это не написал ии не знаю что это значит*/

    /*Пояснение для Власыча: Этот метод запускается в отдельном потоке и работает в бесконечном цикле(поеа я этого хочу), 
    который продолжается, пока переменная runningClock равна true. 
    Внутри цикла он получает текущее время с учетом смещения offsetSeconds, 
    форматирует его в строку и отображает на экране. 
    Каждую секунду он обновляет строку с текущим временем и днем недел
    и, используя Console.SetCursorPosition(примечание SetCursorPosition это метод блягадаря каторыму мы можем указать в каком месте будет выводится инфа в терменал 
    и я устал уже писать...) для того, чтобы перезаписать предыдущую строку времени. 
    Когда пользователь входит в меню изменения времени, переменная runningClock устанавливается в false, 
    что останавливает обновление времени, и после выхода из меню снова устанавливается в true, 
    чтобы продолжить обновление времени. Таким образом, этот метод обеспечивает 
    динамическое отображение текущего времени и дня недели на экране, 
    учитывая любые изменения времени, внесенные пользователем через меню. 
    Важно отметить, что этот метод работает в отдельном потоке, что позволяет ему обновлять время независимо от основного потока, 
    который обрабатывает пользовательский ввод. Это обеспечивает плавное отображение времени без блокировки интерфейса пользователя.
    Так что не знаю где тут может быть проблема, и балы ты не заработаеш тут(сделал сигма лицо , и провёл своим пальчиком по челюсти оформив сочнейший мюнинг)
    */
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
        // Выводим строку с текущим временем и днем недели, перезаписывая предыдущую строку
        //благодоря тому что мы используем SetCursorPosition, мы можем перезаписывать строку с временем, не создавая новые строки 
        // в консоли, что позволяет нам динамически обновлять отображение времени без засорения консоли.
        //таким образом програма не засирает консоль новыми строками, 
        // а просто обновляет существующую строку с временем, ч
        // то обеспечивает более чистый и удобный интерфейс для пользователя
        // и код становится более оптимезированым и более маштабируймым(Лично для тебя сделал Давид, так что не говори что я не стараюсь)
        //
        Console.Write(timeText.PadRight(50));
        // Ждем одну секунду перед следующим обновлением
        /*Да это мне посаоветовалал сдлеать ИИ признаюсь и каюсь */
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
        Знай я знаю где ты живешь , знаю когда ты дома , знаю когда ты спишь , я бы уже давно дал тебе повестку и заташил бы в чорный бусик, 
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

