namespace Laba4
{
    class M_op
    {
        public static double CalculateAverageGrade(Dictionary<string, int> grades)
        {
            double [] ints = new double[grades.Count];
            for (int i = 0; i < grades.Count; i++)
            {
                if (grades.Values.ElementAt(i) < 60 && grades.Values.ElementAt(i) == null)
                {
                    return 0;  
                }
                else if (grades.Values.ElementAt(i) >= 60)
                {
                    ints[i] += grades.Values.ElementAt(i);
                }
                
            }
            return ints.Sum() / grades.Count;
        }
        static double CalculateScholarship(double averageGrade, string studyType)
        {
            if (studyType == "Budget")
            {
                if (averageGrade >= 95)
                    return 3500;  
                else if (averageGrade >= 85)
                    return 2500;   
                else if (averageGrade >= 75)
                    return 1500;
                else
                    return 0;
            }
            else
            {
                return 0; 
            }
        }
    }
}