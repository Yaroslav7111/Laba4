using System;
using System.Collections.Generic;
using System.Linq;

namespace Laba4
{
    class Student
    {
        /*Свойства в C# — это члены класса, которые выглядят как поля, но на деле работают как методы (аксессоры).*/
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }
        public int Age => CalculateAge(BirthDate);
        public string Course { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;

        public Dictionary<string, int> Grades { get; set; }
        public double AverageGrade { get; set; }

        public string StudyType { get; set; } = string.Empty;
        public double Scholarship { get; set; }

        public bool CanEdit { get; set; }
        public string Password { get; set; } = string.Empty;

            /*
         _   
         ||      Це конструтор , який визиває якись із массивів придметів , для того шоб можно було ставити оцінки , веном 
        \  /   
         \/ 
             */
        public Student()
        {
            Grades = new Dictionary<string, int>();
        }

        public static int CalculateAge(DateTime birthDate)
        {
            if (birthDate == default)
                return 0;

            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;

            if (birthDate.Date > today.AddYears(-age))
                age--;

            return age;
        }

        
        public static string[] GetSubjectsByCourse(string course)
        {
            switch (course)
            {
                case "1":
                    return new string[]
                    {
                        "Math I",
                        "Physics I",
                        "Programming Basics",
                        "English",
                        "History",
                        "Discrete Math"
                    };

                case "2":
                    return new string[]
                    {
                        "Math II",
                        "OOP",
                        "Algorithms",
                        "Database",
                        "Web Dev",
                        "Operating Systems"
                    };

                case "3":
                    return new string[]
                    {
                        "Advanced C#",
                        "ASP.NET",
                        "LINQ",
                        "Software Engineering",
                        "AI Basics",
                        "Cybersecurity"
                    };

                default:
                    return new string[]
                    {
                        "Diploma Project",
                        "Practice",
                        "System Design",
                        "Cloud Basics",
                        "DevOps",
                        "Final Exam Prep"
                    };
            }
        }
    }
}