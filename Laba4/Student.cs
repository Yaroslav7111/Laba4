using System;
using System.Collections.Generic;
using System.Linq;

namespace Laba4
{
    class Student
    {
        /*Свойства в C# — это члены класса, которые выглядят как поля, но на деле работают как методы (аксессоры).*/
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }

        public int Age { get; set; }
        public string Course { get; set; }
        public string Group { get; set; }

        public Dictionary<string, int> Grades { get; set; }
        public double AverageGrade { get; set; }

        public string StudyType { get; set; }
        public double Scholarship { get; set; }

        public bool CanEdit { get; set; }
        public string Password { get; set; }
        // Конструктор по умолчанию, который инициализирует словарь оценок
        // Это нужно для того, чтобы при создании нового студента у него уже был готов словарь для хранения оценок по предметам, 
        // и не нужно было бы его создавать вручную каждый раз
        //веном 
        public Student()
        {
            Grades = new Dictionary<string, int>();
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