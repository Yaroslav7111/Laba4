using System.Text.Json;

namespace Laba4
{
    public static class ExTwo
    {
        public static void Run_two()
        {
            string studentsPath = Path.Combine(
                AppContext.BaseDirectory,
                "Students"
            );

            Text.P("Студенты первого курса с оценкой 5 по физике:");

            if (!Directory.Exists(studentsPath))
            {
                Text.P("Students folder was not found.");
                Console.ReadKey(true);
                return;
            }

            foreach (string groupFolder in Directory.GetDirectories(studentsPath))
            {
                string budgetPath = Path.Combine(groupFolder, "Budget_Students");
                string contractPath = Path.Combine(groupFolder, "Contract_Students");

                CheckFolder(budgetPath);
                CheckFolder(contractPath);
            }

            Text.P("\nНажмите любую клавишу...");
            Console.ReadKey(true);
        }

        private static void CheckFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                return;

            foreach (string file in Directory.GetFiles(folderPath, "*.json"))
            {
                string json = File.ReadAllText(file);

                Student? student =
                    JsonSerializer.Deserialize<Student>(json);

                if (student == null)
                    continue;

                // Только первокурсники
                if (student.Course != "1")
                    continue;

                // Проверяем оценку по физике
                if (student.Grades.TryGetValue("Physics I", out int physics))
                {
                    // 5 = от 90 до 100 баллов
                    if (physics >= 90 && physics <= 100)
                    {
                        Text.P(
                            $"{student.Name} {student.Surname} | " +
                            $"Группа: {student.Group} | " +
                            $"Дата рождения: {student.BirthDate:yyyy-MM-dd} | " +
                            $"Возраст: {student.Age} | " +
                            $"Физика: {physics}"
                        );
                    }
                }
            }
        }
    }
}