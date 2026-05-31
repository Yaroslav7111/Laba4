using System.Text.Json;
namespace Laba4
{
    public static class Sort_data
    {
        public static int[,] Sort_by_edit(int[,] fileDates, string filePath)
        {   
            fileDates = new int[1, 2];         
            string budgetPath = Path.Combine(filePath, "Budget_Students");
            string contractPath = Path.Combine(filePath, "Contract_Students");
            foreach (string file in Directory.GetFiles(budgetPath, "*.json"))
            {
                
                string json = File.ReadAllText(file);
                int temp = 0;
                int temp2 = 0;
                int [] ints = new int[2];
                Student student = JsonSerializer.Deserialize<Student>(json);

                if (student.CanEdit)
                {
                    if (student.Password.Length > 0)
                    {
                        temp2++; 
                    }
                    else
                    {
                        temp++; 
                    }
                }
                                
            }
            return fileDates;
        }
    }
}