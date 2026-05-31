namespace Laba4
{
    public static class Additional
    {
        public static void Run_additional()
        {
          Console.SetCursorPosition(0,0);
          Text.P("Choose task:\n");
          Text.P("1.Gigachad Women.\n");
          Text.P("2.Born on the same day.");
          Text.P("3.Come back on main menu");
          Console.SetCursorPosition(0,5);
          Choose();
            

        }
        static void Choose()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);;
            switch (key.Key)
            {
                case ConsoleKey.D1:
                  Console.Clear();
                  Gigachad_women();
                  break; 
                case ConsoleKey.D2:
                  Console.Clear();
                  Born_on_the_same_day();
                  break;
                case ConsoleKey.D3:
                  Console.Clear();
                  Program.ShowMenu();
                  break;
                default:
                    Console.SetCursorPosition(0,4);
                    Text.P("You select an invalid option. Please click another button!");
                    Choose();
                    break;

            }
        }
        static void Gigachad_women()
        {
            Gigachad_W.Res();
        } 
        static void Born_on_the_same_day()
        {
            
        }
    }
}