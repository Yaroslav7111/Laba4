using System.Globalization;
using System.Text.Json;

namespace Laba4
{
    public static class Student_edit
    {
        private const int MenuLeft = 28;
        private static readonly string StudentsRootPath = Path.Combine(AppContext.BaseDirectory, "Students");
        private static readonly string[] Groups = { "КС-25", "КН-25", "КМ-24" };
        private static readonly string[] GenderOptions = { "Male", "Female" };
        private static readonly string[] CourseOptions = { "1", "2", "3", "4" };
        private static readonly string[] StudyOptions = { "Budget", "Contract" };
        private static readonly string[] YesNoOptions = { "Yes", "No" };
        private static readonly string[] PasswordTypeOptions = { "Without password", "With password" };
        private static readonly string[] PasswordOptions = { "Without", "With" };

        public static void Edit_student_file()
        {
            Console.Clear();
            DrawHeader();

            string selectedGroup = SelectOption("Group:", Groups);
            bool needPassword = SelectOption("Password type:", PasswordTypeOptions) == "With password";
            List<StudentFile> editableStudents = LoadEditableStudents(selectedGroup, needPassword);

            if (editableStudents.Count == 0)
            {
                WriteWarning("No editable students were found for the selected group and password type.");
                WaitReturn();
                return;
            }

            StudentFile? selectedStudent = SelectStudent(editableStudents);
            if (selectedStudent == null)
            {
                WriteWarning("Student editing was cancelled.");
                WaitReturn();
                return;
            }

            if (needPassword && !CheckPassword(selectedStudent.Student))
            {
                WriteError("Wrong password. Editing is not allowed.");
                WaitReturn();
                return;
            }

            EditStudent(selectedStudent);
        }

        private static List<StudentFile> LoadEditableStudents(string group, bool needPassword)
        {
            List<StudentFile> students = new List<StudentFile>();
            string groupPath = Path.Combine(StudentsRootPath, group);

            AddEditableStudentsFromFolder(Path.Combine(groupPath, "Budget_Students"), needPassword, students);
            AddEditableStudentsFromFolder(Path.Combine(groupPath, "Contract_Students"), needPassword, students);

            return students
                .OrderBy(item => item.Student.Surname)
                .ThenBy(item => item.Student.Name)
                .ToList();
        }

        private static void AddEditableStudentsFromFolder(
            string folderPath,
            bool needPassword,
            List<StudentFile> students)
        {
            if (!Directory.Exists(folderPath))
            {
                return;
            }

            foreach (string filePath in Directory.GetFiles(folderPath, "*.json"))
            {
                string json = File.ReadAllText(filePath);
                Student? student = JsonSerializer.Deserialize<Student>(json);

                if (student == null || !student.CanEdit)
                {
                    continue;
                }

                bool hasPassword = !string.IsNullOrWhiteSpace(student.Password);
                if (hasPassword != needPassword)
                {
                    continue;
                }

                students.Add(new StudentFile(student, filePath));
            }
        }

        private static StudentFile? SelectStudent(List<StudentFile> students)
        {
            while (true)
            {
                WriteSection("Editable student files:");

                for (int i = 0; i < students.Count; i++)
                {
                    Student student = students[i].Student;
                    Console.WriteLine(
                        $"{i + 1}. {student.Surname} {student.Name} | {T("Course:")} {student.Course} | " +
                        $"{T("Study:")} {T(student.StudyType)} | {T("Average:")} {student.AverageGrade:F2}");
                }

                Console.Write(T("Choose student number or 0 to cancel: "));
                string input = Console.ReadLine() ?? string.Empty;

                if (input == "0")
                {
                    return null;
                }

                if (int.TryParse(input, out int selectedIndex) &&
                    selectedIndex >= 1 &&
                    selectedIndex <= students.Count)
                {
                    return students[selectedIndex - 1];
                }

                WriteError("Invalid choice. Please try again.");
            }
        }

        private static bool CheckPassword(Student student)
        {
            Console.Write(T("Enter password:") + " ");
            string password = Console.ReadLine() ?? string.Empty;

            return password == student.Password;
        }

        private static void EditStudent(StudentFile studentFile)
        {
            while (true)
            {
                Console.Clear();
                DrawHeader();
                ShowStudentPreview(studentFile.Student);
                ShowEditMenu();

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        studentFile.Student.Name = ReadRequiredText("Name:", studentFile.Student.Name);
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        studentFile.Student.Surname = ReadRequiredText("Surname:", studentFile.Student.Surname);
                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        studentFile.Student.Gender = SelectOption("Gender:", GenderOptions);
                        break;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        studentFile.Student.BirthDate = ReadBirthDate(studentFile.Student.BirthDate);
                        break;

                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        EditCourseAndGrades(studentFile.Student);
                        break;

                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        studentFile.Student.Group = SelectOption("Group:", Groups);
                        break;

                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                        studentFile.Student.StudyType = SelectOption("Study:", StudyOptions);
                        RecalculateStudent(studentFile.Student);
                        break;

                    case ConsoleKey.D8:
                    case ConsoleKey.NumPad8:
                        ConfigureEditAccess(studentFile.Student);
                        break;

                    case ConsoleKey.D9:
                    case ConsoleKey.NumPad9:
                        if (SaveStudent(studentFile))
                        {
                            WriteSuccess("Student file has been saved successfully.");
                            WaitReturn();
                            return;
                        }
                        break;

                    case ConsoleKey.D0:
                    case ConsoleKey.NumPad0:
                    case ConsoleKey.Escape:
                        WriteWarning("Student editing was cancelled.");
                        WaitReturn();
                        return;
                }
            }
        }

        private static void ShowEditMenu()
        {
            WriteSection("Choose field to edit:");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Surname");
            Console.WriteLine("3. Gender");
            Console.WriteLine("4. Birth date");
            Console.WriteLine("5. Course and grades");
            Console.WriteLine("6. Group");
            Console.WriteLine("7. Study type");
            Console.WriteLine("8. Edit access and password");
            Console.WriteLine("9. Save and overwrite file");
            Console.WriteLine("0. Cancel");
            Console.Write(T("Choose: "));
        }

        private static void EditCourseAndGrades(Student student)
        {
            student.Course = SelectOption("Course:", CourseOptions);
            string[] subjects = Student.GetSubjectsByCourse(student.Course);
            Dictionary<string, int> updatedGrades = new Dictionary<string, int>();

            WriteSection("Subjects:");

            foreach (string subject in subjects)
            {
                student.Grades.TryGetValue(subject, out int currentGrade);
                updatedGrades[subject] = ReadGrade(subject, currentGrade);
            }

            student.Grades = updatedGrades;
            RecalculateStudent(student);
        }

        private static void ConfigureEditAccess(Student student)
        {
            student.CanEdit = SelectOption("Can edit: [Yes] [No]", YesNoOptions) == "Yes";

            if (!student.CanEdit)
            {
                student.Password = string.Empty;
                return;
            }

            string passwordMode = SelectOption("Password: [Without] [With]", PasswordOptions);
            if (passwordMode == "With")
            {
                student.Password = ReadRequiredText("Enter password:", student.Password);
            }
            else
            {
                student.Password = string.Empty;
            }
        }

        private static bool SaveStudent(StudentFile studentFile)
        {
            RecalculateStudent(studentFile.Student);

            string newFilePath = BuildStudentFilePath(studentFile.Student);
            bool isSameFile = string.Equals(
                Path.GetFullPath(studentFile.FilePath),
                Path.GetFullPath(newFilePath),
                StringComparison.OrdinalIgnoreCase);

            bool isSameGroup = string.Equals(
                GetGroupFromFilePath(studentFile.FilePath),
                studentFile.Student.Group,
                StringComparison.OrdinalIgnoreCase);

            if (!isSameGroup && !StudentGroupControl.CanAddStudent(StudentsRootPath, studentFile.Student.Group, newFilePath))
            {
                WriteError(StudentGroupControl.BuildLimitMessage(studentFile.Student.Group));
                WaitReturn();
                return false;
            }

            if (!isSameFile && File.Exists(newFilePath))
            {
                bool overwrite = SelectOption("File already exists. Overwrite?", YesNoOptions) == "Yes";
                if (!overwrite)
                {
                    WriteWarning("Student file was not saved.");
                    WaitReturn();
                    return false;
                }
            }

            Directory.CreateDirectory(Path.GetDirectoryName(newFilePath)!);

            string json = JsonSerializer.Serialize(studentFile.Student, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(newFilePath, json);

            if (!isSameFile && File.Exists(studentFile.FilePath))
            {
                File.Delete(studentFile.FilePath);
                studentFile.FilePath = newFilePath;
            }

            return true;
        }

        private static string BuildStudentFilePath(Student student)
        {
            string typeFolder = student.StudyType == "Budget" ? "Budget_Students" : "Contract_Students";
            return Path.Combine(
                StudentsRootPath,
                student.Group,
                typeFolder,
                BuildSafeFileName(student));
        }

        private static string GetGroupFromFilePath(string filePath)
        {
            DirectoryInfo? typeDirectory = Directory.GetParent(filePath);
            return typeDirectory?.Parent?.Name ?? string.Empty;
        }

        private static string BuildSafeFileName(Student student)
        {
            string fileName = $"{student.Name}_{student.Surname}_{student.Group}";

            foreach (char invalidChar in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(invalidChar, '_');
            }

            return fileName.Replace(' ', '_') + ".json";
        }

        private static void RecalculateStudent(Student student)
        {
            student.AverageGrade = M_op.CalculateAverageGrade(student.Grades);
            student.Scholarship = M_op.CalculateScholarship(student.AverageGrade, student.StudyType);
        }

        private static string ReadRequiredText(string label, string currentValue)
        {
            while (true)
            {
                Console.Write(T(label) + " ");
                if (!string.IsNullOrWhiteSpace(currentValue))
                {
                    Console.Write($"[{currentValue}] ");
                }

                string input = (Console.ReadLine() ?? string.Empty).Trim();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }

                if (!string.IsNullOrWhiteSpace(currentValue))
                {
                    return currentValue;
                }

                WriteError("Value cannot be empty. Please try again.");
            }
        }

        private static DateTime ReadBirthDate(DateTime currentValue)
        {
            while (true)
            {
                Console.Write(T("Birth date (yyyy-mm-dd):") + $" [{currentValue:yyyy-MM-dd}] ");
                string input = (Console.ReadLine() ?? string.Empty).Trim();

                if (string.IsNullOrWhiteSpace(input) && currentValue != default)
                {
                    return currentValue.Date;
                }

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

        private static int ReadGrade(string subject, int currentGrade)
        {
            while (true)
            {
                Console.Write($"{subject} [{currentGrade}]: ");
                string input = (Console.ReadLine() ?? string.Empty).Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    return currentGrade;
                }

                if (int.TryParse(input, out int grade) && grade >= 0 && grade <= 100)
                {
                    return grade;
                }

                WriteError("Invalid grade. Enter 0-100.");
            }
        }

        private static string SelectOption(string label, string[] options)
        {
            Console.Write(T(label).PadRight(26));
            Console.WriteLine(T("Use Left/Right arrows and Enter."));

            int y = Math.Max(0, Console.CursorTop - 1);
            string selected = MenuSelect.ShowHorizontal(string.Empty, options, MenuLeft, y);
            Console.WriteLine();
            Console.WriteLine();

            return selected.Trim();
        }

        private static void ShowStudentPreview(Student student)
        {
            Console.WriteLine($"{T("Name:")} {student.Name} {student.Surname}");
            Console.WriteLine($"{T("Gender:")} {T(student.Gender)}");
            Console.WriteLine($"{T("Birth date (yyyy-mm-dd):")} {student.BirthDate:yyyy-MM-dd}");
            Console.WriteLine($"{T("Age:")} {student.Age}");
            Console.WriteLine($"{T("Course:")} {student.Course}");
            Console.WriteLine($"{T("Group:")} {student.Group}");
            Console.WriteLine($"{T("Study:")} {T(student.StudyType)}");
            Console.WriteLine($"{T("Average:")} {student.AverageGrade:F2}");
            Console.WriteLine($"{T("Scholarship:")} {student.Scholarship:F2} грн.");
            Console.WriteLine($"{T("Can edit: [Yes] [No]")} {T(student.CanEdit ? "Yes" : "No")}");
            Console.WriteLine();
            WriteSection("Subjects:");

            foreach (KeyValuePair<string, int> grade in student.Grades)
            {
                Console.WriteLine($"- {grade.Key}: {grade.Value}");
            }
        }

        private static void DrawHeader()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine(T("║       EDIT STUDENT INFORMATION       ║").PadRight(59) + "║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
        }

        private static void WriteSection(string title)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.WriteLine($"── {T(title)} ─────────────────────────────────────────────");
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

        private static void WaitReturn()
        {
            Console.WriteLine();
            Console.WriteLine(T("Press any key to return..."));
            Console.ReadKey(true);
        }

        private static string T(string text)
        {
            return Text.TranslateText(text);
        }

        private sealed class StudentFile
        {
            public StudentFile(Student student, string filePath)
            {
                Student = student;
                FilePath = filePath;
            }

            public Student Student { get; }
            public string FilePath { get; set; }
        }
    }
}
