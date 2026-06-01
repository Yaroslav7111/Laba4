using System.Text.Json;

namespace Laba4
{
    class Born_in_same_day
    {
        public static void Rush()
        {
            string studentsPath = Path.Combine(AppContext.BaseDirectory, "Students");

            Dictionary<string, List<Student>> sameFullDate = new();
            Dictionary<string, List<Student>> sameDayMonth = new();

            foreach (string groupFolder in Directory.GetDirectories(studentsPath))
            {
                foreach (string file in Directory.GetFiles(groupFolder, "*.json", SearchOption.AllDirectories))
                {
                    string json = File.ReadAllText(file);
                    Student? student = JsonSerializer.Deserialize<Student>(json);

                    if (student == null) continue;

                    string fullKey = student.BirthDate.ToString("yyyy-MM-dd");
                    string dayMonthKey = student.BirthDate.ToString("dd-MM");

                    if (!sameFullDate.ContainsKey(fullKey))
                        sameFullDate[fullKey] = new List<Student>();
                    sameFullDate[fullKey].Add(student);

                    if (!sameDayMonth.ContainsKey(dayMonthKey))
                        sameDayMonth[dayMonthKey] = new List<Student>();
                    sameDayMonth[dayMonthKey].Add(student);
                }
            }

            Text.P("Same full date :");
            foreach (var group in sameFullDate)
            {
                if (group.Value.Count > 1)
                {
                    foreach (var s in group.Value)
                        Text.P($"Group : {group.Key} | {s.Name} {s.Surname}");
                }
            }

            Text.P("\nSame day:month :");
            foreach (var group in sameDayMonth)
            {
                if (group.Value.Count > 1)
                {
                    foreach (var s in group.Value)
                        Text.P($"Group : {group.Key} | {s.Name} {s.Surname}");
                }
            }

            Text.P("\nPress any key...");
            Console.ReadKey(true);
        }
    }
}