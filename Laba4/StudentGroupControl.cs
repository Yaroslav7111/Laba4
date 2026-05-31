namespace Laba4
{
    public static class StudentGroupControl
    {
        public const int MaxStudentsPerGroup = 30;

        public static int CountStudentsInGroup(string studentsRootPath, string group)
        {
            if (string.IsNullOrWhiteSpace(group))
                return 0;

            string groupPath = Path.Combine(studentsRootPath, group);

            if (!Directory.Exists(groupPath))
                return 0;

            int budgetStudents = CountJsonFiles(Path.Combine(groupPath, "Budget_Students"));
            int contractStudents = CountJsonFiles(Path.Combine(groupPath, "Contract_Students"));

            return budgetStudents + contractStudents;
        }

        public static bool CanAddStudent(string studentsRootPath, string group, string newStudentFilePath)
        {
            if (File.Exists(newStudentFilePath))
                return true;

            return CountStudentsInGroup(studentsRootPath, group) < MaxStudentsPerGroup;
        }

        public static string BuildLimitMessage(string group)
        {
            return $"You cannot add more than {MaxStudentsPerGroup} students to group {group}.";
        }

        private static int CountJsonFiles(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                return 0;

            return Directory.GetFiles(folderPath, "*.json").Length;
        }
    }
}
