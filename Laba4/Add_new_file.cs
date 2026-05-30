using System.Text.Json;

namespace Laba4
{
    class Add_new_file
    {
        public static void User_Input()
        {
            Interface();
        }

        public static void Interface()
        {
            Console.Clear();

            var student = new Student();

            Console.WriteLine("╔══════════════════════════════════════╗");
            Text.P("║         STUDENT INFORMATION          ║");
            Console.WriteLine("╚══════════════════════════════════════╝");

            Console.Write("Name: ");
            student.Name = ReadRequiredString();

            Console.Write("Surname: ");
            student.Surname = ReadRequiredString();

            Text.PLine("Gender: ");
            student.Gender = MenuSelect.ShowHorizontal("", new[] { "Male", "Female" }, Console.CursorLeft, Console.CursorTop).Trim();
            Console.WriteLine();

            student.Age = ReadInt("Age: ", 1, 120);

            Text.PLine("Course: ");
            student.Course = MenuSelect.ShowHorizontal("", new[] { "1", "2", "3", "4" }, Console.CursorLeft, Console.CursorTop).Trim();
            Console.WriteLine();

            Text.PLine("Group: ");
            student.Group = MenuSelect.ShowHorizontal("", new[] { "КС-25", "КН-25", "КМ-24" }, Console.CursorLeft, Console.CursorTop).Trim();
            Console.WriteLine();

            Text.P("Subjects and grades:");
            string[] subjects = Student.GetSubjectsByCourse(student.Course);
            foreach (string subject in subjects)
            {
                int grade = ReadInt($"{subject}: ", 0, 100);
                student.Grades[subject] = grade;
            }

            student.AverageGrade = M_op.CalculateAverageGrade(student.Grades);
            Text.P($"Average: {student.AverageGrade:F2}");

            Text.PLine("Study: ");
            student.StudyType = MenuSelect.ShowHorizontal("", new[] { "Budget", "Contract" }, Console.CursorLeft, Console.CursorTop).Trim();
            Console.WriteLine();

            student.Scholarship = M_op.CalculateScholarship(student.AverageGrade, student.StudyType);
            Text.P($"Scholarship: {student.Scholarship} грн.");

            Text.PLine("Can edit: ");
            string canEdit = MenuSelect.ShowHorizontal("", new[] { "Yes", "No" }, Console.CursorLeft, Console.CursorTop).Trim();
            student.CanEdit = canEdit == "Yes";
            Console.WriteLine();

            if (student.CanEdit)
            {
                Text.PLine("Password: ");
                string passwordMode = MenuSelect.ShowHorizontal("", new[] { "Without", "With" }, Console.CursorLeft, Console.CursorTop).Trim();
                Console.WriteLine();

                if (passwordMode == "With")
                {
                    Console.Write("Enter password: ");
                    student.Password = Console.ReadLine() ?? string.Empty;
                }
                else
                {
                    student.Password = string.Empty;
                }
            }
            else
            {
                student.Password = string.Empty;
            }

            Text.PLine("You want to save this student? ");
            string save = MenuSelect.ShowHorizontal("", new[] { "Yes", "No" }, Console.CursorLeft, Console.CursorTop).Trim();
            Console.WriteLine();

            if (save == "Yes")
            {
                string filePath = CreateNewFile(student);
                Text.P($"Saved to: {filePath}");
            }
        }

        public static string CreateNewFile(Student student)
        {
            string json = JsonSerializer.Serialize(student, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            string studyFolder = student.StudyType == "Budget" ? "Budget_Students" : "Contract_Students";
            string groupFolder = string.IsNullOrWhiteSpace(student.Group) ? "Other_Students" : student.Group;
            string folderPath = Path.Combine(AppContext.BaseDirectory, "Students", groupFolder, studyFolder);
            Directory.CreateDirectory(folderPath);

            string safeName = MakeSafeFileName($"{student.Name}_{student.Surname}_{student.Group}.json");
            string filePath = Path.Combine(folderPath, safeName);

            File.WriteAllText(filePath, json);
            return filePath;
        }

        static string ReadRequiredString()
        {
            while (true)
            {
                string? value = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(value))
                    return value.Trim();

                Text.PLine("Value cannot be empty. Try again: ");
            }
        }

        static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
                    return value;

                Text.P($"Enter a number from {min} to {max}.");
            }
        }

        static string MakeSafeFileName(string fileName)
        {
            foreach (char invalidChar in Path.GetInvalidFileNameChars())
                fileName = fileName.Replace(invalidChar, '_');

            return fileName;
        }
    }
}
