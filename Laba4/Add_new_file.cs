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
            Text.P("Group: ");
            Student.Group = MenuSelect.ShowHorizontal("", new string[] { "КС-25", "КН-25", "КМ-24" }, 10, 12);
            Console.SetCursorPosition(2, 14);
            Text.P("Subjects:");
            // Здесь мы просто отображаем предметы в зависимости от курса студента ,
            // и пофакту это просто вызов метода который возвращает массив с предметами
            string[] subjects = Student.GetSubjectsByCourse(Student.Course);
            for (int i = 0; i < subjects.Length; i++)
            {
                Console.SetCursorPosition(4, 16 + i);
                Text.P(subjects[i]);
            }
            Console.SetCursorPosition(2, 22);
            Text.P("Average:");
            Console.SetCursorPosition(11, 22);
            Student.AverageGrade = M_op.CalculateAverageGrade(Student.Grades);
            Console.SetCursorPosition(2, 25);
            Text.P("Study:");
            Student.StudyType = MenuSelect.ShowHorizontal("", new string[] { "Budget", "Contract" }, 10, 25);
            if (Student.StudyType == "Budget")
            {
                Student.Scholarship = M_op.CalculateScholarship(Student.AverageGrade, Student.StudyType);
                Console.SetCursorPosition(2, 26);
                Text.P("Scholarship: " + Student.Scholarship + " грн.");
                Console.SetCursorPosition(2, 28);
                Text.P("Can edit: [Yes] [No]");
                Student.CanEdit = MenuSelect.ShowHorizontal("", new string[] { "Yes ", "No" }, 16, 29) ;
                if (Student.CanEdit)
                {
                    Console.SetCursorPosition(2, 30);
                    Text.P("Password: [Without] [With]");
                    Student.Password = MenuSelect.ShowHorizontal("", new string[] { "Without ", "With" }, 16, 31);
                    if (Student.Password == "With")
                    {
                        Console.SetCursorPosition(2, 32);
                        Text.P("Enter password:");
                        Student.Password = Console.ReadLine();
                    }
                }
            } 
            else if (Student.StudyType == "Contract")
            {
                Student.Scholarship = 0;
                Console.SetCursorPosition(2, 26);
                Text.P("Can edit: [Yes] [No]");
                Student.CanEdit = MenuSelect.ShowHorizontal("", new string[] { "Yes ", "No" }, 16, 27) ;
                 if (Student.CanEdit)
                {
                    Console.SetCursorPosition(2, 28);
                    Text.P("Password: [Without] [With]");
                    Student.Password = MenuSelect.ShowHorizontal("", new string[] { "Without ", "With" }, 16, 29);
                    if (Student.Password == "With")
                    {
                        Console.SetCursorPosition(2, 30);
                        Text.P("Enter password:");
                        Student.Password = Console.ReadLine();
                    }
                }
            }
            Text.PLine("You wonna save this student?");
            string save = MenuSelect.ShowHorizontal("", new string[] { "Yes ", "No" }, 16, 34);
            if (save == "Yes")
            {                
                CreateNewFile();
            }

        }
    
    public static void CreateNewFile()
    {
        Student student = new Student
        {
            Name = Student.Name,
            Surname = Student.Surname,
            Age = Student.Age,
            Gender = Student.Gender,
            Course = Student.Course,
            Group = Student.Group,
            AverageGrade = Student.AverageGrade,
            StudyType = Student.StudyType,
            Scholarship = Student.Scholarship,
            CanEdit = Student.CanEdit,
            Password = Student.Password
        };

        string json = JsonSerializer.Serialize(student, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        // Папка для хранения
        string folderPath ;
        if (student.Group == "KC-25")
        {
            if (student.StudyType == "Budget")
            {
                folderPath = @"/home/yarolav/CSharpProjects/Laba4/Laba4/Students/KC-25/Budget_Students";
            }
            else
            {
                folderPath = @"/home/yarolav/CSharpProjects/Laba4/Laba4/Students/KC-25/Contract_Students";
            }
        }
        else if (student.Group == "KN-25")
        {
            if (student.StudyType == "Budget")
            {
                folderPath = @"/home/yarolav/CSharpProjects/Laba4/Laba4/Students/KN-25/Budget_Students";
            }
            else
            {
                folderPath = @"/home/yarolav/CSharpProjects/Laba4/Laba4/Students/KN-25/Contract_Students";
            }
        }
        else if (student.Group == "KM-24")
        {
            if (student.StudyType == "Budget")
            {
            folderPath = @"/home/yarolav/CSharpProjects/Laba4/Laba4/Students/KM-24/Budget_Students";
            }
            else
            {
                folderPath = @"/home/yarolav/CSharpProjects/Laba4/Laba4/Students/KM-24/Contract_Students";
            }
        }
        else
        {
            folderPath = @"/home/yarolav/CSharpProjects/Laba4/Laba4/Students/Other_Students";
        }

        // Полный путь к файлу
        string filePath = Path.Combine(
            folderPath,
            $"{student.Name}_{student.Surname}_{student.Group}.json"
        );

        File.WriteAllText(filePath, json);
    }

}
} 