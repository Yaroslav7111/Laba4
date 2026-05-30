namespace Laba4
{
    class M_op
    {
        public static double CalculateAverageGrade(Dictionary<string, int> grades)
        {
            if (grades == null || grades.Count == 0)
                return 0;
             // Проверяем, есть ли оценки ниже 60. Если есть, то средний балл считается 0, так как студент не сдал предмет.
            if (grades.Values.Any(grade => grade < 60))
                return 0;
             // Если все оценки 60 и выше, то вычисляем средний балл как обычно, используя метод Average() для коллекции оценок.
            return grades.Values.Average();
        }

        public static double CalculateScholarship(double averageGrade, string studyType)
        {
            if (studyType != "Budget")
                return 0;

            if (averageGrade >= 95)
                return 3500;
            if (averageGrade >= 85)
                return 2500;
            if (averageGrade >= 75)
                return 1500;

            return 0;
        }
    }
}
