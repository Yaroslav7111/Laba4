namespace Laba4
{
    class Add_new_file
    {
        public static void User_Input()
        {
          
        }
        public static void Interface()
        {
            Console.Clear();
            // Здесь мы просто рисуем интерфейс для ввода данных студента , 
               // и пофакту это просто рамка с надписями ,
               //И дааа его написал мне ИИ , и да , а что вы мне сделаете
               // И да , я знаю что это выглядит ужасно , но я не знаю как это сделать по другому 
               //Слава Императору  
            Console.WriteLine("╔══════════════════════════════════════╗");
            Text.P("║         STUDENT INFORMATION          ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.SetCursorPosition(2, 2);
            Text.P("Name:");
            Console.SetCursorPosition(8, 2);
            Student.Name = Console.ReadLine();
            Console.SetCursorPosition(2, 4);
            Text.P("Surname:");
            Console.SetCursorPosition(11, 4);
            Student.Surname = Console.ReadLine();
            Console.SetCursorPosition(2, 6);
            Text.P("Gender:");
            Console.SetCursorPosition(10, 6);
            // Здесь используется горизонтальное меню для выбора пола студента
            // и приммечание , что я создал массив потому-что неизвесно сколько там будет пунктов , 
            // а так же для удобства добавления новых пунктов в будущем
            //и пофакту у нас сначала идет "" разумеется string title , но так как у нас нет заголовка , то мы просто передаем пустую строку ,
            //  а дальше уже массив с пунктами и координаты для отображения меню
            Student.Gender = MenuSelect.ShowHorizontal("", new string[] { "Male ", "Female" }, 10, 6);
            Console.SetCursorPosition(2, 8);
            Text.P("Age:");
            Console.SetCursorPosition(7, 8);
            Student.Age = int.Parse(Console.ReadLine());
            Console.SetCursorPosition(2, 10);
            Text.P("Course: ");
            Console.SetCursorPosition(10, 10);
            Student.Course = MenuSelect.ShowHorizontal("", new string[] { "1", "2", "3", "4" }, 10, 10);
            Console.SetCursorPosition(2, 12);
            Text.P("Group: [КС-25] [КН-25] [КМ-24]");
            Student.Group = MenuSelect.ShowHorizontal("", new string[] { "КС-25", "КН-25", "КМ-24" }, 10, 12);
            Console.SetCursorPosition(2, 14);
            Text.P("Subjects:");

            Console.SetCursorPosition(2, 22);
            Text.P("Average:");

            Console.SetCursorPosition(2, 25);
            Text.P("Study: [Budget] [Contract]");

            Console.SetCursorPosition(2, 27);
            Text.P("Scholarship: ____ грн.");

            Console.SetCursorPosition(2, 29);
            Text.P("Can edit: [Yes] [No]");

            Console.SetCursorPosition(2, 31);
            Text.P("Password: [Without] [With]");
        }
    
        public static void CreateNewFile()
        {
            // Здесь будет код для создания нового файла с данными студента
        }

}
} 