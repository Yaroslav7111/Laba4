namespace Laba4
{
    public static class TextProcessing
    {
        public static void Run_textprocessing()
        {
            while (true)
            {
                Text.P("Choose the text processing method:");
                Text.P("1. Edit the student's file");
                Text.P("2. Create a new file with the student's data");
                Text.P("3. Exit on the main menu");

                int choice = ReadInt("Enter your choice: ");
                switch (choice)
                {
                    case 1:
                        Text.P("Editing the student's file is not implemented yet.");
                        Text.P("Press any key to return...");
                        Console.ReadKey(true);
                        Console.Clear();
                        break;
                    case 2:
                        Console.Clear();
                        Add_new_file.User_Input();
                        Text.P("Press any key to return...");
                        Console.ReadKey(true);
                        Console.Clear();
                        break;
                    case 3:
                        Console.Clear();
                        return;
                    default:
                        Text.P("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value))
                    return value;

                Text.P("Invalid number. Please try again.");
            }
        }
    }
}
