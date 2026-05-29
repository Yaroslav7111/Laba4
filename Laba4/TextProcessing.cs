namespace Laba4
{ /*
 Но тут будет полный разколбас , потому что я не знаю как это делать , и вообще не понимаю что я делаю 
*/
    public static class TextProcessing
    {
        // Ну короче 
        // так через два часа я напишу что у меня получилось 
        public static void Run_textprocessing()
        {
            Texp.P("Choose the text processing method:");
            Texp.P("1. Edit the student's file ");
            Texp.P("2. Create a new file with the student's data");
            Texp.P("3. Exit on the main menu");// Веном, боже (- -)
            int choice = Texp.GetInt("Enter your choice: ");
            switch (choice)
            {                case 1:
                    Texp.P("Editing the student's file...");
                    // Здесь будет код для редактирования файла студента
                    break;
                case 2:
                    Console.Clear();
                    Add_new_file.User_Input();
                    break;
                case 3:
                    Texp.P("Exiting to the main menu...");
                    // Здесь будет код для выхода в главное меню
                    break;
                default:
                    Texp.P("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}