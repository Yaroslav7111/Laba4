using System.Data;

namespace Laba4
{
    public static class Gigachad_W
    {
        public static void Res()
        {
            Otzenka_man();

        }
        static  void Otzenka_man(){
             //Веном 
             string studentsPath = Path.Combine(
                AppContext.BaseDirectory,
                "Students"
            );
            double mid_otz1;
            double mid_otz2;
            double res = 0 ;
            int temp1 =0 ;
            int temp2 =0 ; 
            foreach (string groupFolder in Directory.GetDirectories(studentsPath))
            {
                string budgetPath = Path.Combine(groupFolder, "Budget_Students");
                string contractPath = Path.Combine(groupFolder, "Contract_Students");

                CheckFolder_M(budgetPath,out mid_otz1,out temp1);
                CheckFolder_M(contractPath,out mid_otz2, out temp2);
                res = Sum(mid_otz1,mid_otz2,temp1,temp2);
                CheckFolder_W(budgetPath,res);
                CheckFolder_W(contractPath,res);

            }
            Text.P("\nPlease tap ather bottom...");
            Console.ReadKey(true);   
        }
         private static void CheckFolder_M(string folderPath, out double mid, out int temp)
        {
             mid = 0;
             temp = 0; 
             //це на на випадлк, якщо папка так не знайдется , хоча такого не буде  
            if (!Directory.Exists(folderPath))
             return;
             //дивимось кожен файл 
             foreach (string file in Directory.GetFiles(folderPath, "*.json"))
            {
                //читаемо що находится в файле 
                string json = File.ReadAllText(file);
                Student? student = JsonSerializer.Deserialize<Student>(json);
                  // ну якщо якес значення студента нуль то мі продовжуємо 
                 if (student == null)
                    continue;
                 if (student.Gender == "Male" )
                {
                   temp++; 
                   mid += student.AverageGrade;
                }   
            } 

        }
        private static void CheckFolder_W(string folderPath, double men)
        {
            if (!Directory.Exists(folderPath))
             return;
             foreach (string file in Directory.GetFiles(folderPath, "*.json"))
            {

                string json = File.ReadAllText(file);
                Student? student = JsonSerializer.Deserialize<Student>(json);
                 if (student == null)
                    continue;
                 if (student.Gender == "Female" && student.AverageGrade > men)
                {
                     Text.P(
                            $"{student.Name} {student.Surname} | " +
                            $"Group: {student.Group} | " +
                            $"Date of Birth: {student.BirthDate:yyyy-MM-dd} | " +
                            $"Age: {student.Age} | " +
                            $"Average Grade: {student.AverageGrade}"
                        );
                }   
            } 
        }
        private static double Sum(double one , double two , int temp , int temp2)
        {
            double total = temp + temp2;
            return total > 0 ? (one + two) / total : 0;
        }
    }

}