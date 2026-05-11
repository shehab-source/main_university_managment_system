using System;
using System.Collections.Generic;

namespace uni_mang_sys
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();
            List<Professor> professors = new List<Professor>();

            int choice = -1;

            while (choice != 0)
            {
                Console.WriteLine("\n=== University Management System ===");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Add Professor");
                Console.WriteLine("3. Display All Students");
                Console.WriteLine("4. Display All Professors");
                Console.WriteLine("5. Manage Student Courses");
                Console.WriteLine("6. Manage Professor Courses");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice: ");

                bool ok = int.TryParse(Console.ReadLine(), out choice);
                if (!ok)
                {
                    Console.WriteLine("Please enter a number.");
                    continue;
                }

                if (choice == 1)
                {
                    Console.WriteLine("\n=== Add Student ===");
                    students.Add(new Student("", 0, 0, 0.0, ""));
                    Console.WriteLine("Student added successfully!");
                }
                else if (choice == 2)
                {
                    Console.WriteLine("\n=== Add Professor ===");
                    professors.Add(new Professor("", 0, 0, ""));
                    Console.WriteLine("Professor added successfully!");
                }
                else if (choice == 3)
                {
                    Console.WriteLine("\n=== All Students ===");
                    if (students.Count == 0)
                        Console.WriteLine("No students found.");
                    else
                        foreach (Student s in students)
                        {
                            s.display();
                            Console.WriteLine();
                        }
                }
                else if (choice == 4)
                {
                    Console.WriteLine("\n=== All Professors ===");
                    if (professors.Count == 0)
                        Console.WriteLine("No professors found.");
                    else
                        foreach (Professor p in professors)
                        {
                            p.display();
                            Console.WriteLine();
                        }
                }
                else if (choice == 5)
                {
                    if (students.Count == 0)
                    {
                        Console.WriteLine("No students found. Add a student first.");
                        continue;
                    }

                    // Pick a student
                    Console.WriteLine("\n=== Select Student ===");
                    for (int i = 0; i < students.Count; i++)
                        Console.WriteLine($"{i + 1}. {students[i].GetName()} (ID: {students[i].GetId()})");

                    Console.Write("Enter student number: ");
                    bool sOk = int.TryParse(Console.ReadLine(), out int sChoice);
                    if (!sOk || sChoice < 1 || sChoice > students.Count)
                    {
                        Console.WriteLine("Invalid choice.");
                        continue;
                    }

                    Student selectedStudent = students[sChoice - 1];

                    // Pick an action
                    Console.WriteLine("\n--- Student Course Management ---");
                    Console.WriteLine("1. Enroll in Course");
                    Console.WriteLine("2. Drop Course");
                    Console.WriteLine("3. Set Course Grade");
                    Console.WriteLine("4. View My Courses");
                    Console.Write("Choice: ");

                    bool cOk = int.TryParse(Console.ReadLine(), out int cChoice);
                    if (!cOk)
                    {
                        Console.WriteLine("Invalid choice.");
                        continue;
                    }

                    if (cChoice == 1) selectedStudent.EnrollCourse(professors);
                    else if (cChoice == 2) selectedStudent.DropCourse();
                    else if (cChoice == 3) selectedStudent.SetGrade();
                    else if (cChoice == 4) selectedStudent.DisplayCourses();
                    else Console.WriteLine("Invalid choice.");
                }
                else if (choice == 6)
                {
                    if (professors.Count == 0)
                    {
                        Console.WriteLine("No professors found. Add a professor first.");
                        continue;
                    }

                    // Pick a professor
                    Console.WriteLine("\n=== Select Professor ===");
                    for (int i = 0; i < professors.Count; i++)
                        Console.WriteLine($"{i + 1}. {professors[i].GetName()} (ID: {professors[i].GetId()})");

                    Console.Write("Enter professor number: ");
                    bool pOk = int.TryParse(Console.ReadLine(), out int pChoice);
                    if (!pOk || pChoice < 1 || pChoice > professors.Count)
                    {
                        Console.WriteLine("Invalid choice.");
                        continue;
                    }

                    Professor selectedProf = professors[pChoice - 1];

                    // Pick an action
                    Console.WriteLine("\n--- Professor Course Management ---");
                    Console.WriteLine("1. Assign Course");
                    Console.WriteLine("2. Unassign Course");
                    Console.WriteLine("3. View My Courses");
                    Console.Write("Choice: ");

                    bool pcOk = int.TryParse(Console.ReadLine(), out int pcChoice);
                    if (!pcOk)
                    {
                        Console.WriteLine("Invalid choice.");
                        continue;
                    }

                    if (pcChoice == 1) selectedProf.AssignCourse();
                    else if (pcChoice == 2) selectedProf.UnassignCourse();
                    else if (pcChoice == 3) selectedProf.DisplayCourses();
                    else Console.WriteLine("Invalid choice.");
                }
                else if (choice == 0)
                {
                    Console.WriteLine("Goodbye!");
                }
                else
                {
                    Console.WriteLine("Invalid choice! Please enter a number from the menu.");
                }
            }
        }
    }
}
