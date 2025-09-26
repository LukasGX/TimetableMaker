namespace TimetableApp
{
    class Test
    {
        public void Start()
        {
            // create environment
            Environment stdEnv = new(0);
            stdEnv.Debug(true);

            // create test classes
            Class? c5a = stdEnv.CreateClass(1, "5a");
            Class? c5b = stdEnv.CreateClass(2, "5b");

            // create test students
            Student? sAlice = stdEnv.CreateStudent(3, "Alice", c5a);
            Student? sBob = stdEnv.CreateStudent(4, "Bob", c5a);
            Student? sCharlie = stdEnv.CreateStudent(5, "Charlie", c5b);

            // create test teachers
            Teacher? tMrSmith = stdEnv.CreateTeacher(6, "Mr. Smith");
            Teacher? tMsJohnson = stdEnv.CreateTeacher(7, "Ms. Johnson");
        }
    }
}