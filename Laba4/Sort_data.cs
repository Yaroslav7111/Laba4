using System.Text.Json;

namespace Laba4
{
    public static class Sort_data
    {
        public static int[,] Sort_by_edit(string studentsPath, string[] groups)
        {
            int[,] result = new int[2, groups.Length];

            for (int groupIndex = 0; groupIndex < groups.Length; groupIndex++)
            {
                string groupPath = Path.Combine(studentsPath, groups[groupIndex]);
                CountEditableStudentsInGroup(groupPath, result, groupIndex);
            }

            return result;
        }

        public static int[,] Sort_by_edit(string filePath)
        {
            int[,] fileDates = new int[2, 1];

            CountEditableStudentsInGroup(filePath, fileDates, 0);

            return fileDates;
        }

        private static void CountEditableStudentsInGroup(string groupPath, int[,] result, int groupIndex)
        {
            CountEditableStudentsInFolder(
                Path.Combine(groupPath, "Budget_Students"),
                result,
                groupIndex);

            CountEditableStudentsInFolder(
                Path.Combine(groupPath, "Contract_Students"),
                result,
                groupIndex);
        }

        private static void CountEditableStudentsInFolder(string folderPath, int[,] result, int groupIndex)
        {
            if (!Directory.Exists(folderPath))
                return;

            foreach (string file in Directory.GetFiles(folderPath, "*.json"))
            {
                string json = File.ReadAllText(file);

                Student? student =
                    JsonSerializer.Deserialize<Student>(json);

                if (student == null || !student.CanEdit)
                    continue;

                if (string.IsNullOrWhiteSpace(student.Password))
                {
                    result[0, groupIndex]++;
                }
                else
                {
                    result[1, groupIndex]++;
                }
            }
        }
    }
}