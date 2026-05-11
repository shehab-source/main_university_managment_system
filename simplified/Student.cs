using System;
using System.Collections.Generic;

namespace uni_mang_sys
{
    class Student : Person
    {
        private double Gpa;
        private List<CourseEnrollment> Enrollments = new List<CourseEnrollment>();

        public Student(string name, int age, int id, double gpa, string department)
            : base(name, age, id, department)
        {
            // GPA validation
            while (true)
            {
                Console.WriteLine("Enter GPA (0.0 - 4.0):");
                bool ok = double.TryParse(Console.ReadLine(), out gpa);
                if (!ok || gpa < 0.0 || gpa > 4.0)
                    Console.WriteLine("Invalid GPA! Enter a number between 0.0 and 4.0.");
                else
                    break;
            }
            Gpa = gpa;
        }

        // =====================================================================
        // BUG FIX: The workflow is now clear:
        //   Step 1 - Build a list of courses that have at least one professor
        //   Step 2 - Show those courses with who teaches them
        //   Step 3 - Student picks a course
        //   Step 4 - Show professors teaching that course, student picks one
        //   Step 5 - Enroll
        // =====================================================================
        public void EnrollCourse(List<Professor> professors)
        {
            // Step 1: Find which courses have at least one professor assigned
            // We use a simple list of courses and a matching list of professor lists
            List<Course> availableCourses = new List<Course>();
            List<List<Professor>> profForCourse = new List<List<Professor>>();

            Course[] allCourses = (Course[])Enum.GetValues(typeof(Course));

            foreach (Course course in allCourses)
            {
                // Skip if student is already enrolled in this course
                bool alreadyEnrolled = false;
                foreach (CourseEnrollment e in Enrollments)
                {
                    if (e.Course == course)
                    {
                        alreadyEnrolled = true;
                        break;
                    }
                }
                if (alreadyEnrolled) continue;

                // Find all professors who teach this course
                List<Professor> teachingProfs = new List<Professor>();
                foreach (Professor prof in professors)
                {
                    foreach (Course c in prof.GetAssignedCourses())
                    {
                        if (c == course)
                        {
                            teachingProfs.Add(prof);
                            break;
                        }
                    }
                }

                // Only add the course if at least one professor teaches it
                if (teachingProfs.Count > 0)
                {
                    availableCourses.Add(course);
                    profForCourse.Add(teachingProfs);
                }
            }

            if (availableCourses.Count == 0)
            {
                Console.WriteLine("No courses available right now. Make sure professors have been assigned to courses first.");
                return;
            }

            // Step 2: Show available courses with who teaches them
            Console.WriteLine("\n=== Available Courses ===");
            for (int i = 0; i < availableCourses.Count; i++)
            {
                // Build a string of professor names for this course
                string profNames = "";
                for (int j = 0; j < profForCourse[i].Count; j++)
                {
                    if (j > 0) profNames += ", ";
                    profNames += "Prof. " + profForCourse[i][j].GetName();
                }
                Console.WriteLine($"{i + 1}. {availableCourses[i].ToString().Replace("_", " ")}  (Taught by: {profNames})");
            }

            // Step 3: Student picks a course
            Console.WriteLine("Enter course number to enroll (0 to cancel):");
            bool courseOk = int.TryParse(Console.ReadLine(), out int courseChoice);
            if (!courseOk || courseChoice < 0 || courseChoice > availableCourses.Count)
            {
                Console.WriteLine("Invalid choice.");
                return;
            }
            if (courseChoice == 0) return;

            Course selectedCourse = availableCourses[courseChoice - 1];
            List<Professor> teachingProfessors = profForCourse[courseChoice - 1];

            // Step 4: Show professors for this course, student picks one
            Console.WriteLine($"\n=== Professors teaching {selectedCourse.ToString().Replace("_", " ")} ===");
            for (int i = 0; i < teachingProfessors.Count; i++)
                Console.WriteLine($"{i + 1}. Prof. {teachingProfessors[i].GetName()} (ID: {teachingProfessors[i].GetId()})");

            Console.WriteLine("Enter professor number (0 to cancel):");
            bool profOk = int.TryParse(Console.ReadLine(), out int profChoice);
            if (!profOk || profChoice < 0 || profChoice > teachingProfessors.Count)
            {
                Console.WriteLine("Invalid choice.");
                return;
            }
            if (profChoice == 0) return;

            Professor selectedProf = teachingProfessors[profChoice - 1];

            // Step 5: Enroll
            Enrollments.Add(new CourseEnrollment(selectedCourse, selectedProf.GetName()));
            Console.WriteLine($"Successfully enrolled in {selectedCourse.ToString().Replace("_", " ")} with Prof. {selectedProf.GetName()}!");
        }

        public void DropCourse()
        {
            if (Enrollments.Count == 0)
            {
                Console.WriteLine("You are not enrolled in any courses.");
                return;
            }

            Console.WriteLine("\n=== Your Enrolled Courses ===");
            for (int i = 0; i < Enrollments.Count; i++)
                Console.WriteLine($"{i + 1}. {Enrollments[i]}");

            Console.WriteLine("Enter course number to drop (0 to cancel):");
            bool ok = int.TryParse(Console.ReadLine(), out int choice);
            if (!ok || choice < 0 || choice > Enrollments.Count)
            {
                Console.WriteLine("Invalid choice.");
                return;
            }
            if (choice == 0) return;

            string courseName = Enrollments[choice - 1].Course.ToString().Replace("_", " ");
            Enrollments.RemoveAt(choice - 1);
            Console.WriteLine($"Successfully dropped {courseName}!");
        }

        public void SetGrade()
        {
            if (Enrollments.Count == 0)
            {
                Console.WriteLine("You are not enrolled in any courses.");
                return;
            }

            Console.WriteLine("\n=== Your Enrolled Courses ===");
            for (int i = 0; i < Enrollments.Count; i++)
                Console.WriteLine($"{i + 1}. {Enrollments[i]}");

            Console.WriteLine("Enter course number to set grade (0 to cancel):");
            bool ok = int.TryParse(Console.ReadLine(), out int choice);
            if (!ok || choice < 0 || choice > Enrollments.Count)
            {
                Console.WriteLine("Invalid choice.");
                return;
            }
            if (choice == 0) return;

            double grade;
            while (true)
            {
                Console.WriteLine("Enter grade (0.0 - 100.0):");
                bool gradeOk = double.TryParse(Console.ReadLine(), out grade);
                if (!gradeOk || grade < 0.0 || grade > 100.0)
                    Console.WriteLine("Invalid grade! Enter a number between 0.0 and 100.0.");
                else
                    break;
            }

            Enrollments[choice - 1].Grade = grade;
            Console.WriteLine("Grade set successfully!");
        }

        public void DisplayCourses()
        {
            if (Enrollments.Count == 0)
            {
                Console.WriteLine("  No enrolled courses.");
                return;
            }
            foreach (CourseEnrollment e in Enrollments)
                Console.WriteLine($"  - {e}");
        }

        public override void display()
        {
            Console.WriteLine($"[Student] Name: {Name} | Age: {Age} | ID: {Id} | Department: {Department} | GPA: {Gpa}");
            Console.WriteLine("  Courses:");
            DisplayCourses();
        }
    }
}
