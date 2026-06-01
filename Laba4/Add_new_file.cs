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

            Text.P("║         STUDENT INFORMATION          ║");

            // NAME
            Console.SetCursorPosition(2, 2);
            Text.P("Name:");
            Console.SetCursorPosition(8, 2);
            student.Name = Console.ReadLine() ?? string.Empty;

            // SURNAME
            Console.SetCursorPosition(2, 4);
            Text.P("Surname:");
            Console.SetCursorPosition(11, 4);
            student.Surname = Console.ReadLine() ?? string.Empty;

            // GENDER
            Console.SetCursorPosition(2, 6);
            Text.P("Gender:");
            student.Gender = MenuSelect.ShowHorizontal("", new string[] { "Male", "Female" }, 10, 6);

            // BIRTH DATE (FIXED WITHOUT FUNCTION)
            Console.SetCursorPosition(2, 8);
            Text.P("Birth date (yyyy-mm-dd):");
            Console.SetCursorPosition(28, 8);

            string birthInput = Console.ReadLine() ?? string.Empty;

            student.BirthDate =
                DateTime.TryParse(birthInput, out DateTime bd) && bd.Date <= DateTime.Today
                    ? bd.Date
                    : default;

            // AGE
            Console.SetCursorPosition(2, 10);
            Text.P($"Age: {student.Age}");

            // COURSE
            Console.SetCursorPosition(2, 12);
            Text.P("Course:");
            student.Course = MenuSelect.ShowHorizontal("", new string[] { "1", "2", "3", "4" }, 10, 12);

            // GROUP
            Console.SetCursorPosition(2, 14);
            Text.P("Group:");
            student.Group = MenuSelect.ShowHorizontal("", new string[] { "КС-25", "КН-25", "КМ-24" }, 10, 14);

            // SUBJECTS
            Console.SetCursorPosition(2, 16);
            Text.P("Subjects:");

            string[] subjects = Student.GetSubjectsByCourse(student.Course);

            for (int i = 0; i < subjects.Length; i++)
            {
                Console.SetCursorPosition(4, 18 + i);
                Text.P(subjects[i]);
            }

            // AVERAGE
            Console.SetCursorPosition(2, 24);
            Text.P("Average:");

            student.AverageGrade = M_op.CalculateAverageGrade(student.Grades);

            Console.SetCursorPosition(11, 24);
            Text.P(student.AverageGrade.ToString());

            // STUDY TYPE
            Console.SetCursorPosition(2, 27);
            Text.P("Study:");

            student.StudyType = MenuSelect.ShowHorizontal("", new string[] { "Budget", "Contract" }, 10, 27);

            // BUDGET
            if (student.StudyType == "Budget")
            {
                student.Scholarship = M_op.CalculateScholarship(student.AverageGrade, student.StudyType);

                Console.SetCursorPosition(2, 28);
                Text.P("Scholarship: " + student.Scholarship + " грн.");

                Console.SetCursorPosition(2, 30);
                Text.P("Can edit: [Yes] [No]");

                student.CanEdit =
                    MenuSelect.ShowHorizontal("", new string[] { "Yes", "No" }, 16, 31).Trim() == "Yes";

                if (student.CanEdit)
                {
                    Console.SetCursorPosition(2, 33);
                    Text.P("Password: [Without] [With]");

                    string passwordMode =
                        MenuSelect.ShowHorizontal("", new string[] { "Without", "With" }, 16, 34).Trim();

                    student.Password = string.Empty;

                    if (passwordMode == "With")
                    {
                        Console.SetCursorPosition(2, 36);
                        Text.P("Enter password:");
                        student.Password = Console.ReadLine() ?? string.Empty;
                    }
                }
            }
            else // CONTRACT
            {
                student.Scholarship = 0;

                Console.SetCursorPosition(2, 28);
                Text.P("Can edit: [Yes] [No]");

                student.CanEdit =
                    MenuSelect.ShowHorizontal("", new string[] { "Yes", "No" }, 16, 29).Trim() == "Yes";

                if (student.CanEdit)
                {
                    Console.SetCursorPosition(2, 31);
                    Text.P("Password: [Without] [With]");

                    string passwordMode =
                        MenuSelect.ShowHorizontal("", new string[] { "Without", "With" }, 16, 32).Trim();

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
            Text.PLine("You wanna save this student?");

            bool shouldSave =
                MenuSelect.ShowHorizontal("", new string[] { "Yes", "No" }, 16, 39).Trim() == "Yes";

            if (shouldSave)
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

            string studentsRootPath = Path.Combine(AppContext.BaseDirectory, "Students");

            string typeFolder =
                student.StudyType == "Budget" ? "Budget_Students" : "Contract_Students";

            string folderPath = Path.Combine(
                studentsRootPath,
                student.Group,
                typeFolder
            );

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