namespace uni_mang_sys
{
    // All available courses in the university
    enum Course
    {
        Vectors,
        Data_Structures,
        Object_Oriented_Programming,
        Digital_Sys_Analysis,
        Discrete_Mathematics,
        Electronics,
        Electrical_Circuits,
    }

    
    class CourseEnrollment
    {
        public Course Course;
        public double? Grade;
        public string ProfessorName;

        public CourseEnrollment(Course course, string professorName)
        {
            Course = course;
            Grade = null;
            ProfessorName = professorName;
        }

        public override string ToString()
        {
            string grade = Grade.HasValue ? $"{Grade:F1}" : "Not graded";
            return $"{Course.ToString().Replace("_", " ")} | Professor: {ProfessorName} | Grade: {grade}";
        }
    }
}
