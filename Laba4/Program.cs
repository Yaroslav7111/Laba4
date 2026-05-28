namespace Laba4
{
    class Program
    {
        static void Main()
        {
            Text.CurrentLang = "en";

            Text.P("Choose language: en - English, ru - Russian, ua - Ukrainian.");
            Console.Write("Choose: ");

            string lang = Console.ReadLine();

            if (lang != "en" && lang != "ru" && lang != "ua")
            {
                Text.P("Invalid language. Defaulting to English.");
                lang = "en";
            }

            Text.CurrentLang = lang;

            while (true)
            {
                ShowMenu();
                ChooseTask();
            }
        }

        static void ShowMenu()
        {
            Text.P(
                "Choose what you want:\n" +
                "1 - Task 1\n" +
                "2 - Task 2\n" +
                "3 - Additional\n" +
                "4 - Text processing\n" +
                "Q - Quit"
            );
        }

        static void ChooseTask()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    ExOne.Run_one();
                    break;

                case ConsoleKey.D2:
                    Console.Clear();
                    ExTwo.Run_two();
                    break;

                case ConsoleKey.D3:
                    Console.Clear();
                    Additional.Run_additional();
                    break;

                case ConsoleKey.D4:
                    Console.Clear();
                    TextProcessing.Run_textprocessing();
                    break;

                case ConsoleKey.Q:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}