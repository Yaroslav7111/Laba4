using System.Text.Json;
namespace Laba4
{
    class Add_new_file
    {
        public static void User_Input()
        {
            Student student = new Student();
            Interface(student);
        }
        public static void Interface(Student student)
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
            student.Name = Console.ReadLine();
            Console.SetCursorPosition(2, 4);
            Text.P("Surname:");
            Console.SetCursorPosition(11, 4);
            student.Surname = Console.ReadLine();
            Console.SetCursorPosition(2, 6);
            Text.P("Gender:");
            Console.SetCursorPosition(10, 6);
            // Здесь используется горизонтальное меню для выбора пола студента
            // и приммечание , что я создал массив потому-что неизвесно сколько там будет пунктов , 
            // а так же для удобства добавления новых пунктов в будущем
            //и пофакту у нас сначала идет "" разумеется string title , но так как у нас нет заголовка , то мы просто передаем пустую строку ,
            //  а дальше уже массив с пунктами и координаты для отображения меню
            student.Gender = MenuSelect.ShowHorizontal("", new string[] { "Male ", "Female" }, 10, 6);
            Console.SetCursorPosition(2, 8);
            Text.P("Age:");
            Console.SetCursorPosition(7, 8);
            int age = int.Parse(Console.ReadLine());

            if (age > 0)
            {
                student.Age = age;
            }
            else
            {
                Console.SetCursorPosition(2, 8);
                Text.P("You entered not correct age, please try again: ");
                student.Age = int.Parse(Console.ReadLine());
            }
            Console.SetCursorPosition(2, 10);
            Text.P("Course: ");
            Console.SetCursorPosition(10, 10);
            student.Course = MenuSelect.ShowHorizontal("", new string[] { "1", "2", "3", "4" }, 10, 10);
            Console.SetCursorPosition(2, 12);
            Text.P("Group: ");
            student.Group = MenuSelect.ShowHorizontal("", new string[] { "КС-25", "КН-25", "КМ-24" }, 10, 12);
            Console.SetCursorPosition(2, 14);
            Text.P("Subjects:");
            // Здесь мы просто отображаем предметы в зависимости от курса студента ,
            // и пофакту это просто вызов метода который возвращает массив с предметами
            string[] subjects = Student.GetSubjectsByCourse(student.Course);
            for (int i = 0; i < subjects.Length; i++)
            {
                Console.SetCursorPosition(4, 16 + i);
                Text.P(subjects[i]);
            }
            Console.SetCursorPosition(2, 22);
            Text.P("Average:");
            Console.SetCursorPosition(11, 22);
            student.AverageGrade = M_op.CalculateAverageGrade(student.Grades);
            Console.SetCursorPosition(2, 25);
            Text.P("Study:");
            student.StudyType = MenuSelect.ShowHorizontal("", new string[] { "Budget", "Contract" }, 10, 25);
            if (student.StudyType == "Budget")
            {
                student.Scholarship = M_op.CalculateScholarship(student.AverageGrade, student.StudyType);
                Console.SetCursorPosition(2, 26);
                Text.P("Scholarship: " + student.Scholarship + " грн.");
                Console.SetCursorPosition(2, 28);
                Text.P("Can edit: [Yes] [No]");
                student.CanEdit = MenuSelect.ShowHorizontal("", new string[] { "Yes ", "No" }, 16, 29).Trim() == "Yes";
                if (student.CanEdit)
                {
                    Console.SetCursorPosition(2, 30);
                    Text.P("Password: [Without] [With]");
                    student.Password = MenuSelect.ShowHorizontal("", new string[] { "Without ", "With" }, 16, 31);
                    if (student.Password == "With")
                    {
                        Console.SetCursorPosition(2, 32);
                        Text.P("Enter password:");
                        student.Password = Console.ReadLine();
                    }
                }
            } 
            else if (student.StudyType == "Contract")
            {
                student.Scholarship = 0;
                Console.SetCursorPosition(2, 26);
                Text.P("Can edit: [Yes] [No]");
                student.CanEdit = MenuSelect.ShowHorizontal("", new string[] { "Yes ", "No" }, 16, 27).Trim() == "Yes";
                 if (student.CanEdit)
                {
                    Console.SetCursorPosition(2, 28);
                    Text.P("Password: [Without] [With]");
                    student.Password = MenuSelect.ShowHorizontal("", new string[] { "Without ", "With" }, 16, 29);
                    if (student.Password == "With")
                    {
                        Console.SetCursorPosition(2, 30);
                        Text.P("Enter password:");
                        student .Password = Console.ReadLine();
                    }
                }
            }
            Text.PLine("You wonna save this student?");
            student.CanEdit = MenuSelect.ShowHorizontal("", new string[] { "Yes", "No" }, 16, 29) == "Yes";
            if (student.CanEdit)
            {                
                CreateNewFile(student);
            }

        }
    
    public static void CreateNewFile(Student student)
    {
       

        string json = JsonSerializer.Serialize(student, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        // Папка для хранения
        string folderPath ;
        // Выбираем папку в зависимости от типа обучения студента
        string typeFolder =
            student.StudyType == "Budget" ? "Budget_Students" : "Contract_Students";
        // Полный путь к папке для хранения файла
        folderPath = Path.Combine(
            "/home/yaroslav/CSharpProjects/Laba4/Laba4/Students",
            student.Group,
            typeFolder
        );
        // Полный путь к файлу
        string filePath = Path.Combine(
            folderPath,
            $"{student.Name}_{student.Surname}_{student.Group}.json"
        );
        Directory.CreateDirectory(folderPath);

        File.WriteAllText(filePath, json);
    }

}
} 