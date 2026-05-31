namespace Laba4
{
    public static class TextProcessing
    {
        public static void Run_textprocessing()
        {
            while (true)
            {
                Console.Clear();

                Text.P("Choose the text processing method:");
                Text.P("1. Edit the student's file");
                Text.P("2. Create a new file with the student's data");
                Text.P("3. Exit to the main menu");

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Student_edit.Edit_student_file();
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.Clear();
                        Add_new_file.User_Input();
                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        Console.Clear();
                        return;

                    default:
                        Console.Clear();
                        Text.P("Invalid choice. Please try again.");
                        Console.ReadKey(true);
                        break;
                }
            }
        }
    }
}