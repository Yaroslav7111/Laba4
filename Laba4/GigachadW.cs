using System;
using System.IO;
using System.Text.Json;

namespace Laba4
{
    public static class Gigachad_W
    {
        public static void Res()
        {
            string studentsPath = Path.Combine(AppContext.BaseDirectory, "Students");

            double sumMen = 0;
            int countMen = 0;

            // СНАЧАЛА собираем всех мужчин
            foreach (string groupFolder in Directory.GetDirectories(studentsPath))
            {
                string budgetPath = Path.Combine(groupFolder, "Budget_Students");
                string contractPath = Path.Combine(groupFolder, "Contract_Students");

                CollectMen(budgetPath, ref sumMen, ref countMen);
                CollectMen(contractPath, ref sumMen, ref countMen);
            }

            // Считаем средний балл мужчин
            double maleAverage = countMen > 0 ? sumMen / countMen : 0;

            // Ищем женщин, которые выше среднего мужчин
            foreach (string groupFolder in Directory.GetDirectories(studentsPath))
            {
                string budgetPath = Path.Combine(groupFolder, "Budget_Students");
                string contractPath = Path.Combine(groupFolder, "Contract_Students");

                CheckWomen(budgetPath, maleAverage);
                CheckWomen(contractPath, maleAverage);
            }

            Console.WriteLine("\nPress any key...");
            Console.ReadKey(true);
        }

        private static void CollectMen(string folderPath, ref double sum, ref int count)
        {
            if (!Directory.Exists(folderPath))
                return;

            foreach (string file in Directory.GetFiles(folderPath, "*.json"))
            {
                string json = File.ReadAllText(file);
                Student? student = JsonSerializer.Deserialize<Student>(json);

                if (student == null)
                    continue;

                if (student.Gender == "Male")
                {
                    sum += student.AverageGrade;
                    count++;
                }
            }
        }

        private static void CheckWomen(string folderPath, double maleAverage)
        {
            if (!Directory.Exists(folderPath))
                return;

            foreach (string file in Directory.GetFiles(folderPath, "*.json"))
            {
                string json = File.ReadAllText(file);
                Student? student = JsonSerializer.Deserialize<Student>(json);

                if (student == null)
                    continue;

                if (student.Gender == "Female" && student.AverageGrade > maleAverage)
                {
                    Console.WriteLine(
                        $"{student.Name} {student.Surname} | " +
                        $"Group: {student.Group} | " +
                        $"Birth: {student.BirthDate:yyyy-MM-dd} | " +
                        $"Age: {student.Age} | " +
                        $"Avg: {student.AverageGrade}"
                    );
                }
            }
        }
    }
}