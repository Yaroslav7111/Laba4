namespace Laba4
{
    public static class Student_edit
    {
        static string path = @"/home/yarolav/CSharpProjects/Laba4/Laba4/bin/Debug/net10.0/Students/";
        static int[,] fileDates = new int[1, 2];
        public static void Edit_student_file()
        {
            Console.Clear();
            Console.SetCursorPosition(2, 41);
            Text.P("KC-25"); 
            Console.SetCursorPosition(2, 49);
            Text.P("KH-25");
            Console.SetCursorPosition(2, 57);
            Text.P("KM-24");
            Console.SetCursorPosition(8, 7);
            Text.P("Editable Student File:");
            Console.SetCursorPosition(12, 2);
            Text.P("Editable Student File (with password):");
            string[] options = { "KC-25", "KH-25", "KM-24" };
            for (int i = 0; i < options.Length; i++)
            {
                string needpath = path + "/" + options[i] + "/";
                Serch_need_files_date(needpath);
            }
        }
        public static void Serch_need_files_date(string filePath)
        {
            Sort_data.Sort_by_edit(fileDates, filePath);
        }
    }
}