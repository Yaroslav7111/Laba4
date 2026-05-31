namespace Laba4
{
    public static class Student_edit
    {
        static readonly string path = Path.Combine(AppContext.BaseDirectory, "Students");
        static readonly string[] groups = { "КС-25", "КН-25", "КМ-24" };

        public static void Edit_student_file()
        {
            Console.Clear();
            Text.P("Editable student files:");

            int[,] fileDates = Sort_data.Sort_by_edit(path, groups);

            for (int i = 0; i < groups.Length; i++)
            {
                Text.P($"{groups[i]}: without password - {fileDates[0, i]}, with password - {fileDates[1, i]}");
            }

            Text.P("Press any key to return...");
            Console.ReadKey(true);
        }
    }
}
