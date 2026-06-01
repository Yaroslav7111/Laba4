using System.Globalization;
using System.Text.Json;

namespace Laba4
{
    class Add_new_file
    {
        private const int FormLeft = 2;
        private const int MenuLeft = 24;
        private static readonly string[] GenderOptions = { "Male", "Female" };
        private static readonly string[] CourseOptions = { "1", "2", "3", "4" };
        private static readonly string[] GroupOptions = { "КС-25", "КН-25", "КМ-24" };
        private static readonly string[] StudyOptions = { "Budget", "Contract" };
        private static readonly string[] YesNoOptions = { "Yes", "No" };
        private static readonly string[] PasswordOptions = { "Without", "With" };

        public static void User_Input()
        {
            Student student = new Student();
            Interface(student);
        }

        public static void Interface(Student student)
        {
            Console.Clear();
            DrawHeader();

            student.Name = ReadRequiredText("Name:", "Enter the student's name.");
            student.Surname = ReadRequiredText("Surname:", "Enter the student's surname.");
            student.Gender = SelectOption("Gender:", GenderOptions);
            student.BirthDate = ReadBirthDate();

            WriteInfo($"Age: {student.Age}");

            student.Course = SelectOption("Course:", CourseOptions);
            student.Group = SelectOption("Group:", GroupOptions);
            ReadSubjects(student);

            student.AverageGrade = M_op.CalculateAverageGrade(student.Grades);
            WriteInfo($"Average: {student.AverageGrade:F2}");

            student.StudyType = SelectOption("Study:", StudyOptions);
            student.Scholarship = M_op.CalculateScholarship(student.AverageGrade, student.StudyType);

            if (student.StudyType == "Budget")
            {
                WriteInfo($"Scholarship: {student.Scholarship:F2} грн.");
            }
            else
            {
                WriteInfo("Scholarship: 0 грн.");
            }

            ConfigureEditAccess(student);
            ShowPreview(student);

            bool shouldSave = SelectOption("You wanna save this student?", YesNoOptions) == "Yes";
            if (shouldSave)
            {
                bool saved = CreateNewFile(student);

                if (saved)
                {
                    WriteSuccess("Student file has been saved successfully.");
                }
            }
            else
            {
                WriteWarning("Student file was not saved.");
            }

            WriteFooter("Press any key to return...");
            Console.ReadKey(true);
        }

        private static void DrawHeader()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine(T("║         STUDENT INFORMATION          ║").PadRight(59) + "║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
        }

        private static string ReadRequiredText(string label, string helpText)
        {
            while (true)
            {
                WriteFieldLabel(label);
                Console.Write(T(helpText) + " ");
                string value = (Console.ReadLine() ?? string.Empty).Trim();

                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }

                WriteError("Value cannot be empty. Please try again.");
            }
        }

        private static DateTime ReadBirthDate()
        {
            while (true)
            {
                WriteFieldLabel("Birth date (yyyy-mm-dd):");
                Console.Write(T("Use format yyyy-mm-dd: "));
                string input = (Console.ReadLine() ?? string.Empty).Trim();

                bool parsed = DateTime.TryParseExact(
                    input,
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime birthDate);

                if (parsed && birthDate.Date <= DateTime.Today)
                {
                    return birthDate.Date;
                }

                WriteError("Invalid birth date. Enter a real date that is not in the future.");
            }
        }

        private static string SelectOption(string label, string[] options)
        {
            WriteFieldLabel(label);
            Console.WriteLine(T("Use Left/Right arrows and Enter."));

            int y = Math.Max(0, Console.CursorTop - 1);
            string selected = MenuSelect.ShowHorizontal(string.Empty, options, MenuLeft, y);
            Console.WriteLine();
            Console.WriteLine();

            return selected.Trim();
        }

        private static void ReadSubjects(Student student)
        {
            WriteSection("Subjects:");
            string[] subjects = Student.GetSubjectsByCourse(student.Course);

            foreach (string subject in subjects)
            {
                int grade = ReadGrade(subject);
                student.Grades[subject] = grade;
            }
        }

        private static int ReadGrade(string subject)
        {
            while (true)
            {
                WriteFieldLabel($"{subject}:");
                Console.Write(T("Grade (0-100): "));
                string input = (Console.ReadLine() ?? string.Empty).Trim();

                if (int.TryParse(input, out int grade) && grade >= 0 && grade <= 100)
                {
                    return grade;
                }

                WriteError("Invalid grade. Enter 0-100.");
            }
        }

        private static void ConfigureEditAccess(Student student)
        {
            student.CanEdit = SelectOption("Can edit: [Yes] [No]", YesNoOptions) == "Yes";
            student.Password = string.Empty;

            if (!student.CanEdit)
            {
                return;
            }

            string passwordMode = SelectOption("Password: [Without] [With]", PasswordOptions);
            if (passwordMode != "With")
            {
                return;
            }

            while (true)
            {
                WriteFieldLabel("Enter password:");
                string password = Console.ReadLine() ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(password))
                {
                    student.Password = password;
                    return;
                }

                WriteError("Password cannot be empty when password mode is enabled.");
            }
        }

        private static void ShowPreview(Student student)
        {
            WriteSection("Preview");
            Console.WriteLine($"  {T("Name:")} {student.Name} {student.Surname}");
            Console.WriteLine($"  {T("Gender:")} {T(student.Gender)}");
            Console.WriteLine($"  {T("Birth date (yyyy-mm-dd):")} {student.BirthDate:yyyy-MM-dd}");
            Console.WriteLine($"  {T("Age:")} {student.Age}");
            Console.WriteLine($"  {T("Course:")} {student.Course}");
            Console.WriteLine($"  {T("Group:")} {student.Group}");
            Console.WriteLine($"  {T("Study:")} {T(student.StudyType)}");
            Console.WriteLine($"  {T("Average:")} {student.AverageGrade:F2}");
            Console.WriteLine($"  {T("Scholarship:")} {student.Scholarship:F2} грн.");
            Console.WriteLine($"  {T("Can edit: [Yes] [No]")} {T(student.CanEdit ? "Yes" : "No")}");
            Console.WriteLine();
        }

        public static bool CreateNewFile(Student student)
        {
            string studentsRootPath = Path.Combine(AppContext.BaseDirectory, "Students");
            string typeFolder = student.StudyType == "Budget" ? "Budget_Students" : "Contract_Students";
            string folderPath = Path.Combine(studentsRootPath, student.Group, typeFolder);
            string fileName = BuildSafeFileName(student);
            string filePath = Path.Combine(folderPath, fileName);

            if (!StudentGroupControl.CanAddStudent(studentsRootPath, student.Group, filePath))
            {
                WriteError(StudentGroupControl.BuildLimitMessage(student.Group));
                return false;
            }

            Directory.CreateDirectory(folderPath);

            string json = JsonSerializer.Serialize(student, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(filePath, json);
            return true;
        }

        private static string BuildSafeFileName(Student student)
        {
            string baseName = $"{student.Name}_{student.Surname}_{student.Group}";

            foreach (char invalidChar in Path.GetInvalidFileNameChars())
            {
                baseName = baseName.Replace(invalidChar, '_');
            }

            return baseName.Replace(' ', '_') + ".json";
        }

        private static void WriteSection(string title)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.WriteLine($"── {T(title)} ─────────────────────────────────────────────");
            Console.ResetColor();
        }

        private static void WriteFieldLabel(string label)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(new string(' ', FormLeft));
            Console.Write(T(label).PadRight(22));
            Console.ResetColor();
        }

        private static void WriteInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(T(message));
            Console.ResetColor();
        }

        private static void WriteSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(T(message));
            Console.ResetColor();
        }

        private static void WriteWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(T(message));
            Console.ResetColor();
        }

        private static void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(T(message));
            Console.ResetColor();
        }

        private static void WriteFooter(string message)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(T(message));
            Console.ResetColor();
        }

        private static string T(string text)
        {
            return Text.TranslateText(text);
        }
    }
}
