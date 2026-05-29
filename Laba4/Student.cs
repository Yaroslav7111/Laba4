using System;
using System.Collections.Generic;
using System.Linq;

namespace Laba4
{
    class Student
    {
        // =========================
        // DATA
        // =========================

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }

        public int Age { get; set; }
        public int Course { get; set; }
        public string Group { get; set; }

        public Dictionary<string, int> Grades { get; set; }
        public double AverageGrade { get; set; }

        public string StudyType { get; set; }
        public double Scholarship { get; set; }

        public bool CanEdit { get; set; }
        public string Password { get; set; }

        public Student()
        {
            Grades = new Dictionary<string, int>();
        }

        // =========================
        // TEMPLATE (UI)
        // =========================

        public static void DrawTemplate()
        {
            Console.Clear();

            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║         STUDENT INFORMATION          ║");
            Console.WriteLine("╚══════════════════════════════════════╝");

            Console.SetCursorPosition(2, 2);
            Console.Write("Name:");

            Console.SetCursorPosition(2, 4);
            Console.Write("Surname:");

            Console.SetCursorPosition(2, 6);
            Console.Write("Gender:");

            Console.SetCursorPosition(2, 8);
            Console.Write("Age:");

            Console.SetCursorPosition(2, 10);
            Console.Write("Course: [1] [2] [3] [4]");

            Console.SetCursorPosition(2, 12);
            Console.Write("Group: [КС-25] [КН-25] [КМ-24]");

            Console.SetCursorPosition(2, 14);
            Console.Write("Subjects:");

            Console.SetCursorPosition(2, 22);
            Console.Write("Average:");

            Console.SetCursorPosition(2, 25);
            Console.Write("Study: [Budget] [Contract]");

            Console.SetCursorPosition(2, 27);
            Console.Write("Scholarship: ____ грн.");

            Console.SetCursorPosition(2, 29);
            Console.Write("Can edit: [Yes] [No]");

            Console.SetCursorPosition(2, 31);
            Console.Write("Password: [Without] [With]");
        }

        // =========================
        // SUBJECTS BY COURSE (6+)
        // =========================

        public static string[] GetSubjectsByCourse(int course)
        {
            switch (course)
            {
                case 1:
                    return new string[]
                    {
                        "Math I",
                        "Physics I",
                        "Programming Basics",
                        "English",
                        "History",
                        "Discrete Math"
                    };

                case 2:
                    return new string[]
                    {
                        "Math II",
                        "OOP",
                        "Algorithms",
                        "Database",
                        "Web Dev",
                        "Operating Systems"
                    };

                case 3:
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

        // =========================
        // AVERAGE
        // =========================

        public void CalculateAverage()
        {
            AverageGrade = Grades.Count == 0
                ? 0
                : Grades.Values.Average();
        }
    }
}