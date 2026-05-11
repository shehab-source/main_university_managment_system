using System;

namespace uni_mang_sys
{
    
    public abstract class Person : IDisplay
    {
        protected string Name;
        protected int Age;
        protected int Id;
        protected string Department;

        
        public string GetName() { return Name; }
        public int GetId() { return Id; }

        public Person(string name, int age, int id, string department)
        {
            while (true)
            {
                Console.WriteLine("Enter name:");
                Name = Console.ReadLine();

                if (Name == "")
                {
                    Console.WriteLine("Name cannot be empty.");
                    continue;
                }

                bool nameIsValid = true;
                foreach (char c in Name)
                {
                    if (!char.IsLetter(c) && c != ' ')
                    {
                        nameIsValid = false;
                        break;
                    }
                }

                if (!nameIsValid)
                    Console.WriteLine("Name must contain letters only.");
                else
                    break;
            }

         
            while (true)
            {
                Console.WriteLine("Enter age (16 - 60):");
                bool ok = int.TryParse(Console.ReadLine(), out age);
                if (!ok || age < 16 || age > 60)
                    Console.WriteLine("Invalid age! Enter a number between 16 and 60.");
                else
                    break;
            }
            Age = age;

            
            while (true)
            {
                Console.WriteLine("Enter ID (positive number, max 9 digits):");
                bool ok = int.TryParse(Console.ReadLine(), out id);
                if (!ok || id <= 0 || id.ToString().Length > 9)
                    Console.WriteLine("Invalid ID! Enter a positive number with max 9 digits.");
                else
                    break;
            }
            Id = id;

            
            while (true)
            {
                Console.WriteLine("Enter department:");
                Department = Console.ReadLine();

                if (Department == "")
                {
                    Console.WriteLine("Department cannot be empty.");
                    continue;
                }

                bool deptIsValid = true;
                foreach (char c in Department)
                {
                    if (!char.IsLetter(c) && c != ' ')
                    {
                        deptIsValid = false;
                        break;
                    }
                }

                if (!deptIsValid)
                    Console.WriteLine("Department must contain letters only.");
                else
                    break;
            }
        }

        
        public virtual void display()
        {
            Console.WriteLine($"Name: {Name} | Age: {Age} | ID: {Id} | Department: {Department}");
        }
    }
}
