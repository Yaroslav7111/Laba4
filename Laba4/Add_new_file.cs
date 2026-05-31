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
            student.Name = Console.ReadLine() ?? string.Empty;
            Console.SetCursorPosition(2, 4);
            Text.P("Surname:");
            Console.SetCursorPosition(11, 4);
            student.Surname = Console.ReadLine() ?? string.Empty;
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
            Text.P("Birth date (yyyy-mm-dd):");
            Console.SetCursorPosition(28, 8);
            student.BirthDate = ReadBirthDate();
            Console.SetCursorPosition(2, 10);
            Text.P($"Age: {student.Age}");
            Console.SetCursorPosition(2, 12);
            Text.P("Course: ");
            Console.SetCursorPosition(10, 12);
            student.Course = MenuSelect.ShowHorizontal("", new string[] { "1", "2", "3", "4" }, 10, 12);
            Console.SetCursorPosition(2, 14);
            Text.P("Group: ");
            student.Group = MenuSelect.ShowHorizontal("", new string[] { "КС-25", "КН-25", "КМ-24" }, 10, 14);
            Console.SetCursorPosition(2, 16);
            Text.P("Subjects:");
            // Здесь мы просто отображаем предметы в зависимости от курса студента ,
            // и пофакту это просто вызов метода который возвращает массив с предметами
            string[] subjects = Student.GetSubjectsByCourse(student.Course);
            for (int i = 0; i < subjects.Length; i++)
            {
                Console.SetCursorPosition(4, 18 + i);
                Text.P(subjects[i]);
            }
            Console.SetCursorPosition(2, 24);
            Text.P("Average:");
            Console.SetCursorPosition(11, 24);
            student.AverageGrade = M_op.CalculateAverageGrade(student.Grades);
            Console.SetCursorPosition(2, 27);
            Text.P("Study:");
            student.StudyType = MenuSelect.ShowHorizontal("", new string[] { "Budget", "Contract" }, 10, 27);
            if (student.StudyType == "Budget")
            {
                student.Scholarship = M_op.CalculateScholarship(student.AverageGrade, student.StudyType);
                Console.SetCursorPosition(2, 28);
                Text.P("Scholarship: " + student.Scholarship + " грн.");
                Console.SetCursorPosition(2, 30);
                Text.P("Can edit: [Yes] [No]");
                student.CanEdit = MenuSelect.ShowHorizontal("", new string[] { "Yes ", "No" }, 16, 31).Trim() == "Yes";
                if (student.CanEdit)
                {
                    Console.SetCursorPosition(2, 33);
                    Text.P("Password: [Without] [With]");
                    string passwordMode = MenuSelect.ShowHorizontal("", new string[] { "Without ", "With" }, 16, 34).Trim();
                    student.Password = string.Empty;
                    if (passwordMode == "With")
                    {
                        Console.SetCursorPosition(2, 36);
                        Text.P("Enter password:");
                        student.Password = Console.ReadLine() ?? string.Empty;
                    }
                }
            } 
            else if (student.StudyType == "Contract")
            {
                student.Scholarship = 0;
                Console.SetCursorPosition(2, 28);
                Text.P("Can edit: [Yes] [No]");
                student.CanEdit = MenuSelect.ShowHorizontal("", new string[] { "Yes ", "No" }, 16, 29).Trim() == "Yes";
                 if (student.CanEdit)
                {
                    Console.SetCursorPosition(2, 31);
                    Text.P("Password: [Without] [With]");
                    string passwordMode = MenuSelect.ShowHorizontal("", new string[] { "Without ", "With" }, 16, 32).Trim();
                    student.Password = string.Empty;
                    if (passwordMode == "With")
                    {
                        Console.SetCursorPosition(2, 34);
                        Text.P("Enter password:");
                        student.Password = Console.ReadLine() ?? string.Empty;
                    }
                }
            }
            Console.SetCursorPosition(2, 38);
            Text.PLine("You wonna save this student?");
            bool shouldSave = MenuSelect.ShowHorizontal("", new string[] { "Yes", "No" }, 16, 39).Trim() == "Yes";
            if (shouldSave)
            {                
                CreateNewFile(student);
            }

        }
    

        private static DateTime ReadBirthDate()
        {
            while (true)
            {
                string input = Console.ReadLine() ?? string.Empty;

                if (DateTime.TryParse(input, out DateTime birthDate) &&
                    birthDate.Date <= DateTime.Today)
                {
                    return birthDate.Date;
                }

                Console.SetCursorPosition(2, 8);
                Text.P("Wrong birth date, use yyyy-mm-dd: ");
                Console.SetCursorPosition(37, 8);
            }
        }

    public static void CreateNewFile(Student student)
    {
       

        string json = JsonSerializer.Serialize(student, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        string studentsRootPath = Path.Combine(AppContext.BaseDirectory, "Students");
        // Папка для хранения
        string folderPath ;
        // Выбираем папку в зависимости от типа обучения студента
        string typeFolder =
            student.StudyType == "Budget" ? "Budget_Students" : "Contract_Students";
        // Полный путь к папке для хранения файла
        folderPath = Path.Combine(
            studentsRootPath,
            student.Group,
            typeFolder
        );
        // Полный путь к файлу
        string filePath = Path.Combine(
            folderPath,
            $"{student.Name}_{student.Surname}_{student.Group}.json"
        );

        if (!StudentGroupControl.CanAddStudent(studentsRootPath, student.Group, filePath))
        {
            Text.P(StudentGroupControl.BuildLimitMessage(student.Group));
            Console.ReadKey(true);
            return;
        }

        Directory.CreateDirectory(folderPath);

        File.WriteAllText(filePath, json);
    }

}
} 