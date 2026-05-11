using System;
using System.Collections.Generic;

namespace uni_mang_sys
{
    class Professor : Person
    {
        // List of courses this professor teaches
        private List<Course> AssignedCourses = new List<Course>();

        public Professor(string name, int age, int id, string department)
            : base(name, age, id, department)
        {
            
        }

        public List<Course> GetAssignedCourses()
        {
            return AssignedCourses;
        }

        // Assign a new course to this professor
        public void AssignCourse()
        {
            // Get all possible courses from the enum
            Course[] allCourses = (Course[])Enum.GetValues(typeof(Course));

            Console.WriteLine("\n=== Available Courses ===");
            for (int i = 0; i < allCourses.Length; i++)
            {
                string alreadyAssigned = AssignedCourses.Contains(allCourses[i]) ? " [Already Assigned]" : "";
                Console.WriteLine($"{i + 1}. {allCourses[i].ToString().Replace("_", " ")}{alreadyAssigned}");
            }

            Console.WriteLine("Enter course number to assign (0 to cancel):");
            bool ok = int.TryParse(Console.ReadLine(), out int choice);

            if (!ok || choice < 0 || choice > allCourses.Length)
            {
                Console.WriteLine("Invalid choice.");
                return;
            }
            if (choice == 0) return;

            Course selected = allCourses[choice - 1];

            if (AssignedCourses.Contains(selected))
            {
                Console.WriteLine($"{selected.ToString().Replace("_", " ")} is already assigned to you.");
                return;
            }

            AssignedCourses.Add(selected);
            Console.WriteLine($"Successfully assigned {selected.ToString().Replace("_", " ")} to Prof. {Name}!");
        }

        // Remove a course from this professor
        public void UnassignCourse()
        {
            if (AssignedCourses.Count == 0)
            {
                Console.WriteLine("You have no assigned courses.");
                return;
            }

            Console.WriteLine("\n=== Your Assigned Courses ===");
            for (int i = 0; i < AssignedCourses.Count; i++)
                Console.WriteLine($"{i + 1}. {AssignedCourses[i].ToString().Replace("_", " ")}");

            Console.WriteLine("Enter course number to unassign (0 to cancel):");
            bool ok = int.TryParse(Console.ReadLine(), out int choice);

            if (!ok || choice < 0 || choice > AssignedCourses.Count)
            {
                Console.WriteLine("Invalid choice.");
                return;
            }
            if (choice == 0) return;

            string courseName = AssignedCourses[choice - 1].ToString().Replace("_", " ");
            AssignedCourses.RemoveAt(choice - 1);
            Console.WriteLine($"Successfully unassigned {courseName} from Prof. {Name}!");
        }

        public void DisplayCourses()
        {
            if (AssignedCourses.Count == 0)
            {
                Console.WriteLine("  No assigned courses.");
                return;
            }
            foreach (Course c in AssignedCourses)
                Console.WriteLine($"  - {c.ToString().Replace("_", " ")}");
        }

        public override void display()
        {
            Console.WriteLine($"[Professor] Name: {Name} | Age: {Age} | ID: {Id} | Department: {Department}");
            Console.WriteLine("  Courses:");
            DisplayCourses();
        }
    }
}
