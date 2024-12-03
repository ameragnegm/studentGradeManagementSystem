using System.Collections.Generic;
using System.ComponentModel.Design;

namespace studentGradeManagementSystem
{
    struct studentRecord { 
 
        public int Id;
        public string Name;
        public int  Englishscore;
        public int  Arabicscore;
        public int  Mathscore;
        public double avgScore;
        public Grade grade;
    
    }

    enum Grade {
  
        A,
        B,
        C,
        D,
        F

    }
    internal class Program
    {
        public static int GetValidInt(string outputvalue, int min, int max)
        {
            int result;
            while (true)
            {
                Console.WriteLine(outputvalue);
                if (int.TryParse(Console.ReadLine(), out  result) && result >= min && result <= max)
                {
                    break;
                }
                Console.WriteLine($"Please enter a valid number between {min} and {max}.");
            }
            return result;
        }
        public static studentRecord AddStudent(List<studentRecord> students)
        {
            studentRecord addedstudent = new studentRecord();
            Console.WriteLine("Enter your Data:");

            while (true)
            {
                addedstudent.Id = GetValidInt("ID:", 1, int.MaxValue);
                if (!students.Any(s => s.Id == addedstudent.Id))
                {
                    break;
                }
                Console.WriteLine(" Error: ID already exists.");
            }

            do
            {
                Console.WriteLine("Name:");
                addedstudent.Name = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(addedstudent.Name));

            addedstudent.Englishscore = GetValidInt("Enter English Score (0-100):", 0, 100);
            addedstudent.Mathscore = GetValidInt("Enter Math Score (0-100):", 0, 100);
            addedstudent.Arabicscore = GetValidInt("Enter Arabic Score (0-100):", 0, 100);

            addedstudent.avgScore = CalculateAverage(addedstudent);
            addedstudent.grade = CalculateGrade(addedstudent.avgScore);

            return addedstudent;
        }
        public static void UpdateStudent(List<studentRecord> students)
        {
            int ID = GetValidInt("Enter ID for updating:", 1, int.MaxValue);

            for (int i = 0; i < students.Count; i++)
            {
                if (ID == students[i].Id)
                {
                    Console.WriteLine("Enter number to update score \n(1- English, 2- Arabic, 3- Math):");
                    int num = GetValidInt("Choose an option:", 1, 3);

                    studentRecord currentstudent = students[i];
                    if (num == 1)
                    {
                        currentstudent.Englishscore = GetValidInt("New English score:", 0, 100);
                    }
                    else if (num == 2)
                    {
                        currentstudent.Arabicscore = GetValidInt("New Arabic score:", 0, 100);
                    }
                    else if (num == 3)
                    {
                        currentstudent.Mathscore = GetValidInt("New Math score:", 0, 100);
                    }

                    // Update avgScore and grade
                    currentstudent.avgScore = CalculateAverage(currentstudent);
                    currentstudent.grade = CalculateGrade(currentstudent.avgScore);

                    students[i] = currentstudent;
                    Console.WriteLine("Student updated successfully.");
                    return;
                }
            }
            Console.WriteLine(" Given ID not found.");
        }
        public static void DisplayAll(List<studentRecord> students)
        {
            if (!students.Any())
            {
                Console.WriteLine("No students available.");
                return;
            }

            foreach (studentRecord student in students)
            {
                Console.WriteLine($"    ID : {student.Id}");
                Console.WriteLine($"    Name : {student.Name}");
                Console.WriteLine($"    English score : {student.Englishscore}");
                Console.WriteLine($"    Arabic Score : {student.Arabicscore}");
                Console.WriteLine($"    Math Score : {student.Mathscore}\n");
                Console.WriteLine($"    Grade : {student.grade}");
                Console.WriteLine($"    AvgScore : {Math.Round(student.avgScore,4)}\n");

            }
        }
        public static void DisplayRecord(studentRecord student)
        {
                Console.WriteLine($"    ID : {student.Id}");
                Console.WriteLine($"    Name : {student.Name}");
                Console.WriteLine($"    English score : {student.Englishscore}");
                Console.WriteLine($"    Arabic Score : {student.Arabicscore}");
                Console.WriteLine($"    Math Score : {student.Mathscore}\n");
                Console.WriteLine($"    Grade : {student.grade}");
                Console.WriteLine($"    AvgScore : {Math.Round(student.avgScore, 4)}\n");

        }
        public static void highestavg(List<studentRecord> students)
        {   
            studentRecord s = new studentRecord();
            double highest = 0;

            if (!students.Any())
            {
                Console.WriteLine("No students available.");
                return;
            }

            for (int i = 0; i < students.Count; i++)
            {
                if (students[i].avgScore > highest)
                {
                    highest = Math.Round(students[i].avgScore,4);
                    s = students[i];
                }
            }
            DisplayRecord(s);

        }
        public static double CalculateAverage(studentRecord student)
        {
            return (student.Mathscore + student.Arabicscore + student.Englishscore) / 3.0;
        }
        public static double Total_avg (List<studentRecord> students)
        {
            double total = 0;
            foreach ( studentRecord s in students){
                total += s.avgScore;   
            }
            return total/students.Count;
        }
        public static Grade CalculateGrade(double avgValue)
        {
            if (avgValue >= 90) return Grade.A;
            if (avgValue >= 80) return Grade.B;
            if (avgValue >= 70) return Grade.C;
            if (avgValue >= 60) return Grade.D;
            return Grade.F;
        }
        public static void passfailStudents(List<studentRecord> students)
        {
            List<studentRecord> passedstudents = new List<studentRecord>();
            List<studentRecord> failedstudents = new List<studentRecord>();

            foreach (studentRecord s in students)
            {
                if (s.grade == Grade.F)
                {
                    failedstudents.Add(s);
                }
                else
                {
                    passedstudents.Add(s);
                }
            }
            Console.Clear();
            Console.WriteLine("Passed List :");
            DisplayAll(passedstudents);
            Console.WriteLine("Failed List :");
            DisplayAll(failedstudents);
        }
        public static void SearchedStudent (List<studentRecord> students)
        {
            int ID = GetValidInt("Enter ID for searching :" , 1 , int.MaxValue);
            studentRecord s = new studentRecord();
            int found = 0;
            for (int i = 0; i < students.Count; i++)
            {
                if ( ID == students[i].Id)
                {
                    s = students[i];
                    found = 1;
                }
            }
            if (found == 0)
            {
                Console.WriteLine(" Given ID not found.");
            }
            else
            {
                DisplayRecord(s);

            }
        }
        public static void ShowMenu(List<studentRecord> students)
        {
            // options 
            string[] options = {
                "1. Add Student ",
                "2. Update Student Scores ",
                "3. Display All Students ",
                "4. Show Top Performer ",
                "5. Total average score",
                "6. Show Pass/Fail Lists " ,
                "7. Search For Student",
                "8. Exit "
            };
            int selectedoption = 0;

            while (true)
            {
                Console.Clear();

                // display menu options and highlighted the first option as default .
                Console.WriteLine("Menu Options : \n  ");

                for (int i = 0; i < options.Length; i++)
                {

                    if (i == selectedoption)
                    {
                        // change color of selected option
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ResetColor();
                    }
                    Console.WriteLine(options[i]);
                }

                Console.ResetColor();
                // key arrows 
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    // + op.length to make sure to go the last choice if iam in first 
                    selectedoption = (selectedoption - 1 + options.Length) % options.Length;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    // % op.length to make sure the range of  [ 0 , op.length -1 ] 
                    selectedoption = (selectedoption + 1) % options.Length;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    switch (selectedoption)
                    {
                        case 0:
                            studentRecord addedstudent = AddStudent(students);
                            if (students.Any(s => s.Id == addedstudent.Id))
                            {
                                Console.WriteLine("Error: ID already exists. ");
                                return;
                            }
                            else
                            {
                                students.Add(addedstudent);
                                Console.WriteLine("Student added successfully.");
                                break;
                            }
                        case 1:
                            UpdateStudent(students);
                            break;
                        case 2:
                            DisplayAll(students);
                            break;
                        case 3:
                            highestavg(students);
                            break;
                        case 4:
                            if (!students.Any())
                            {
                                Console.WriteLine("No students avaliable.");
                            }
                            else
                            {
                                Console.WriteLine($"The average score of all students is {Math.Round(Total_avg(students), 4)}");
                            }
                            break;
                        case 5:
                            passfailStudents(students);
                            break;
                        case 6:
                            SearchedStudent(students);
                            break;
                        case 7:
                            return;

                    }
                    Console.WriteLine("\n\nPress any key to return to the menu");
                    Console.ReadKey();
                }

            }
        }
            static void Main(string[] args)
        {


            List<studentRecord> students = new List<studentRecord>();

            //// fixed students
            //studentRecord s1 = new studentRecord
            //{
            //    Id = 8,
            //    Name = "Alaa",
            //    Englishscore = 80,
            //    Arabicscore = 85,
            //    Mathscore = 90,
            //};
            //studentRecord s2 = new studentRecord
            //{
            //    Id = 2,
            //    Name = "Amera",
            //    Englishscore = 70,
            //    Arabicscore = 65,
            //    Mathscore = 90
            //};
            //studentRecord s3 = new studentRecord
            //{
            //    Id = 1,
            //    Name = "Nada",
            //    Englishscore = 50,
            //    Arabicscore = 50,
            //    Mathscore = 50
            //};
            //s1.avgScore = CalculateAverage(s1);
            //s2.avgScore = CalculateAverage(s2);
            //s3.avgScore = CalculateAverage(s3);
            //s1.grade = CalculateGrade(s1.avgScore);
            //s2.grade = CalculateGrade(s2.avgScore);
            //s3.grade = CalculateGrade(s3.avgScore);
            //students.Add(s1);
            //students.Add(s2);
            //students.Add(s3);

            ShowMenu(students);
         }

            
        
    }
}
