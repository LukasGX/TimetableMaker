namespace TimetableApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Test test = new();
            test.Start();
        }

        public static void PrintDebug(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[DEBUG] ");
            Console.ResetColor();
            Console.WriteLine(message);
        }

        public static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[ERROR] ");
            Console.ResetColor();
            Console.WriteLine(message);
        }
    }

    public class Environment
    {
        private int Id { get; }
        private bool Debugging { get; set; } = false;

        public List<int> PublicScopeIds { get; set; } = new();

        public Environment(int id)
        {
            Id = id;

            PublicScopeIds.Add(id);
            Program.PrintDebug("Created environment with ID " + id);
        }

        public int GetId()
        {
            return Id;
        }

        public void Debug(bool set)
        {
            Program.PrintDebug("Debug mode set to: " + set);
            Debugging = set;
        }

        public bool GetDebug()
        {
            return Debugging;
        }

        // creation methods
        public Class? CreateClass(int id, string name)
        {
            if (PublicScopeIds.Contains(id))
            {
                Program.PrintError("ID " + id + " already exists in public scope, cannot create class");
                return null;
            }

            PublicScopeIds.Add(id);

            Class newClass = new Class(id, name, this);
            return newClass;
        }

        public Student? CreateStudent(int id, string name, Class cls)
        {
            if (cls == null)
            {
                Program.PrintError("Class is null, cannot create student");
                return null;
            }

            if (PublicScopeIds.Contains(id))
            {
                Program.PrintError("ID " + id + " already exists in public scope, cannot create student");
                return null;
            }

            Student newStudent = new Student(id, name, cls, this);
            cls.AddStudent(newStudent);
            return newStudent;
        }

        public Teacher? CreateTeacher(int id, string name)
        {
            if (PublicScopeIds.Contains(id))
            {
                Program.PrintError("ID " + id + " already exists in public scope, cannot create teacher");
                return null;
            }

            PublicScopeIds.Add(id);

            Teacher newTeacher = new Teacher(id, name, this);
            return newTeacher;
        }
    }

    public class Class
    {
        private int Id { get; }
        private string Name { get; set; }
        private List<Student> Students { get; set; } = new();
        private Environment Env { get; set; }

        public Class(int id, string name, Environment env)
        {
            Id = id;
            Name = name;
            Env = env;

            if (Env.GetDebug() == true) Program.PrintDebug("Created class " + name + " with ID " + id);
        }

        public string GetName()
        {
            return Name;
        }

        public int GetId()
        {
            return Id;
        }

        public List<Student> GetStudents()
        {
            return Students;
        }

        public void AddStudent(Student student)
        {
            Students.Add(student);
        }
    }

    public class Student
    {
        private int Id { get; }
        private string Name { get; set; }
        private Class Class { get; set; }
        private Environment Env { get; set; }

        public Student(int id, string name, Class cls, Environment env)
        {
            Id = id;
            Name = name;
            Class = cls;
            Env = env;

            if (Env.GetDebug() == true) Program.PrintDebug("Created student " + name + " with ID " + id);
        }

        public string GetName()
        {
            return Name;
        }

        public int GetId()
        {
            return Id;
        }

        public Class GetClass()
        {
            return Class;
        }
    }

    public class Teacher
    {
        private int Id { get; }
        private string Name { get; set; }
        private List<Subject> Subjects { get; set; } = new();
        private int maxHoursPerWeek { get; set; } = 28;
        private List<Availability> Availabilities { get; set; } = new();
        private Environment Env { get; set; }

        public Teacher(int id, string name, Environment env)
        {
            Id = id;
            Name = name;
            Env = env;

            if (Env.GetDebug() == true) Program.PrintDebug("Created teacher " + name + " with ID " + id);
        }

        public string GetName()
        {
            return Name;
        }
    }

    // from here, we probably want to edit the classes later
    public class Subject
    {
        private string Name { get; set; }

        public Subject(string name)
        {
            Name = name;
        }

        public string GetName()
        {
            return Name;
        }
    }

    public class Availability
    {
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public Availability(DayOfWeek day, TimeSpan startTime, TimeSpan endTime)
        {
            Day = day;
            StartTime = startTime;
            EndTime = endTime;
        }
    }

    public enum RoomType
    {
        Classroom,
        Laboratory,
        SportsHall,
        MusicRoom,
        ArtRoom
    }

    public class Room
    {
        private string Name { get; set; }
        private int Capacity { get; set; }
        private List<Availability> Availabilities { get; set; } = new();
        private RoomType Type { get; set; }

        public Room(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
        }

        public string GetName()
        {
            return Name;
        }

        public int GetCapacity()
        {
            return Capacity;
        }
    }

    public class TimeGrid
    {
        private int Id { get; }
        private List<TimeSpan> StartTimes { get; set; } = new();
        private List<TimeSpan> EndTimes { get; set; } = new();

        public TimeGrid(int id, List<TimeSpan> startTimes, List<TimeSpan> endTimes)
        {
            Id = id;
            StartTimes = startTimes;
            EndTimes = endTimes;
        }
    }
}
