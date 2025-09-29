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

        public static void PrintSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[SUCCESS] ");
            Console.ResetColor();
            Console.WriteLine(message);
        }

        public static void PrintInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[INFO] ");
            Console.ResetColor();
            Console.WriteLine(message);
        }

        public static void GenerateTimetable(Environment env)
        {
            // 1. Erstelle eine Datenstruktur, z.B. ein Dictionary, das für jede Klasse ein Raster aller Zeiten (z.B. [Tag, Stunde]) vorsieht
            var timetable = new Dictionary<int, Dictionary<DayOfWeek, List<LessonSlot>>>();

            foreach (var cls in env.Classes.Values)
            {
                timetable[cls.GetId()] = new Dictionary<DayOfWeek, List<LessonSlot>>();
                foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
                {
                    if (day == DayOfWeek.Saturday || day == DayOfWeek.Sunday) continue;
                    timetable[cls.GetId()][day] = new List<LessonSlot>();
                    // Fülle mit leeren Slots entsprechend env.TimeGrids
                    var grid = env.TimeGrids.Values.FirstOrDefault();
                    if (grid == null) throw new Exception("No time grid defined.");
                    for (int i = 0; i < grid.StartTimes.Count; i++)
                    {
                        timetable[cls.GetId()][day].Add(new LessonSlot
                        {
                            Start = grid.StartTimes[i],
                            End = grid.EndTimes[i],
                            Subject = null,
                            Teacher = null,
                            Room = null
                        });
                    }
                }
            }

            // Lokale Hilfsfunktionen, um Doppelbelegungen zu vermeiden
            bool IsTeacherOccupied(Teacher teacher, DayOfWeek day, int slotIndex)
            {
                foreach (var otherCls in env.Classes.Values)
                {
                    var lesson = timetable[otherCls.GetId()][day][slotIndex];
                    if (lesson.Teacher == teacher) return true;
                }
                return false;
            }

            bool IsRoomOccupied(Room room, DayOfWeek day, int slotIndex)
            {
                foreach (var otherCls in env.Classes.Values)
                {
                    var lesson = timetable[otherCls.GetId()][day][slotIndex];
                    if (lesson.Room == room) return true;
                }
                return false;
            }

            // 2. Sammle alle Klassen, Fächer und die jeweils benötigten Wochenstunden
            foreach (var cls in env.Classes.Values)
            {
                foreach (var subj in env.Subjects.Values)
                {
                    // FIX: Match subject allocation by class grade level, not by class ID
                    var allocation = subj.GetAllocation().FirstOrDefault(a => a?.GradeLevel == cls.GetGradeLevel());
                    if (allocation == null) continue;
                    int hoursToPlace = allocation.WeeklyHours;

                    while (hoursToPlace > 0)
                    {
                        // 3. Suche einen passenden Slot in timetable[cls.GetId()][day][stunde]
                        bool placed = false;
                        foreach (DayOfWeek day in timetable[cls.GetId()].Keys)
                        {
                            for (int slot = 0; slot < timetable[cls.GetId()][day].Count; slot++)
                            {
                                if (timetable[cls.GetId()][day][slot].Subject == null)
                                {
                                    // 4. Suche einen verfügbaren Lehrer (mit Qualifikation und Verfügbarkeit)
                                    var teacher = env.Teachers.Values.FirstOrDefault(t =>
                                        t.Subjects.Any(s => s.GetId() == subj.GetId()) &&
                                        t.Availabilities.Any(a =>
                                            a != null &&
                                            a.Day == day &&
                                            a.StartTime <= timetable[cls.GetId()][day][slot].Start &&
                                            a.EndTime >= timetable[cls.GetId()][day][slot].End
                                        ) && !IsTeacherOccupied(t, day, slot));

                                    if (teacher == null) continue;

                                    // 5. Suche einen Raum mit Verfügbarkeit
                                    var room = env.Rooms.Values.FirstOrDefault(r =>
                                        r.Availabilities.Any(a =>
                                            a != null &&
                                            a.Day == day &&
                                            a.StartTime <= timetable[cls.GetId()][day][slot].Start &&
                                            a.EndTime >= timetable[cls.GetId()][day][slot].End
                                        ) && !IsRoomOccupied(r, day, slot));

                                    if (room == null) continue;

                                    // 6. Slot füllen
                                    timetable[cls.GetId()][day][slot].Subject = subj;
                                    timetable[cls.GetId()][day][slot].Teacher = teacher;
                                    timetable[cls.GetId()][day][slot].Room = room;

                                    hoursToPlace--;
                                    placed = true;
                                    break;
                                }
                            }
                            if (placed) break;
                        }
                        if (!placed)
                        {
                            PrintError($"Kein Platz mehr für {subj.GetName()} in Klasse {cls.GetName()}.");
                            break;
                        }
                    }
                }
            }

            // 7. Ausgabe (z.B. als Debug oder später in Datei/Tabelle)
            foreach (var cls in env.Classes.Values)
            {
                PrintDebug($"Stundenplan für Klasse {cls.GetName()}:");
                foreach (DayOfWeek day in timetable[cls.GetId()].Keys)
                {
                    foreach (var slot in timetable[cls.GetId()][day])
                    {
                        if (slot.Subject != null)
                            PrintDebug($"  {day} {slot.Start}-{slot.End}: {slot.Subject!.GetName()} ({slot.Teacher!.GetName()} in {slot.Room!.GetName()})");
                    }
                }
            }
        }
    }

    public class LessonSlot
    {
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }
        public Subject? Subject { get; set; }
        public Teacher? Teacher { get; set; }
        public Room? Room { get; set; }
    }

    public class Environment
    {
        private int Id { get; }
        private bool Debugging { get; set; } = false;

        public List<int> PublicScopeIds { get; set; } = new();

        public Dictionary<int, Class> Classes { get; set; } = new();
        public Dictionary<int, Student> Students { get; set; } = new();
        public Dictionary<int, Teacher> Teachers { get; set; } = new();
        public Dictionary<int, Subject> Subjects { get; set; } = new();
        public Dictionary<int, SubjectAllocation> SubjectAllocations { get; set; } = new();
        public Dictionary<int, Availability> Availabilities { get; set; } = new();
        public Dictionary<int, Room> Rooms { get; set; } = new();
        public Dictionary<int, TimeGrid> TimeGrids { get; set; } = new();

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
        public Class? CreateClass(int id, string name, int gradeLevel)
        {
            if (PublicScopeIds.Contains(id))
            {
                Program.PrintError("ID " + id + " already exists in public scope, cannot create class");
                return null;
            }

            PublicScopeIds.Add(id);

            Class newClass = new(id, name, gradeLevel, this);

            Classes.Add(id, newClass);
            return newClass;
        }

        public Student? CreateStudent(int id, string name, Class? cls)
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

            PublicScopeIds.Add(id);

            Student newStudent = new(id, name, cls, this);
            cls.AddStudent(newStudent);
            Students.Add(id, newStudent);
            return newStudent;
        }

        public Teacher? CreateTeacher(int id, string name, List<Availability?> availabilities)
        {
            if (PublicScopeIds.Contains(id))
            {
                Program.PrintError("ID " + id + " already exists in public scope, cannot create teacher");
                return null;
            }

            PublicScopeIds.Add(id);

            Teacher newTeacher = new(id, name, availabilities, this);
            Teachers.Add(id, newTeacher);
            return newTeacher;
        }

        public Subject? CreateSubject(int id, string name, List<SubjectAllocation?> allocation)
        {
            if (PublicScopeIds.Contains(id))
            {
                Program.PrintError("ID " + id + " already exists in public scope, cannot create subject");
                return null;
            }

            PublicScopeIds.Add(id);

            Subject newSubject = new(id, name, allocation, this);
            Subjects.Add(id, newSubject);
            return newSubject;
        }

        public SubjectAllocation? CreateSubjectAllocation(int id, int gradeLevel, int weeklyHours)
        {
            if (PublicScopeIds.Contains(id))
            {
                Program.PrintError("ID " + id + " already exists in public scope, cannot create subject allocation");
                return null;
            }

            PublicScopeIds.Add(id);

            SubjectAllocation newAllocation = new(id, gradeLevel, weeklyHours, this);
            SubjectAllocations.Add(id, newAllocation);
            return newAllocation;
        }

        public Availability? CreateAvailability(int id, DayOfWeek day, TimeOnly startTime, TimeOnly endTime)
        {
            if (PublicScopeIds.Contains(id))
            {
                Program.PrintError("ID " + id + " already exists in public scope, cannot create availability");
                return null;
            }

            PublicScopeIds.Add(id);

            Availability newAvailability = new(id, day, startTime, endTime, this);
            Availabilities.Add(id, newAvailability);
            return newAvailability;
        }

        public Room? CreateRoom(int id, string name, int capacity, List<Availability?> availabilities, RoomType roomType)
        {
            if (PublicScopeIds.Contains(id))
            {
                Program.PrintError("ID " + id + " already exists in public scope, cannot create room");
                return null;
            }

            PublicScopeIds.Add(id);

            Room newRoom = new(id, name, capacity, availabilities, roomType, this);
            Rooms.Add(id, newRoom);
            return newRoom;
        }

        public TimeGrid? CreateTimeGrid(int id, List<TimeOnly> startTimes, List<TimeOnly> endTimes)
        {
            if (PublicScopeIds.Contains(id))
            {
                Program.PrintError("ID " + id + " already exists in public scope, cannot create time grid");
                return null;
            }

            PublicScopeIds.Add(id);

            TimeGrid newTimeGrid = new(id, startTimes, endTimes, this);
            TimeGrids.Add(id, newTimeGrid);
            return newTimeGrid;
        }
    }

    public class Class
    {
        private int Id { get; }
        private string Name { get; set; }
        private int GradeLevel { get; set; }
        private List<Student> Students { get; set; } = new();
        private Environment Env { get; set; }

        public Class(int id, string name, int gradeLevel, Environment env)
        {
            Id = id;
            Name = name;
            GradeLevel = gradeLevel;
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

        public int GetGradeLevel()
        {
            return GradeLevel;
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
        private Class? Class { get; set; }
        private Environment Env { get; set; }

        public Student(int id, string name, Class? cls, Environment env)
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

        public Class? GetClass()
        {
            return Class;
        }
    }

    public class Teacher
    {
        private int Id { get; }
        private string Name { get; set; }
        public List<Subject> Subjects { get; set; } = new();
        private int maxHoursPerWeek { get; set; } = 28;
        public List<Availability?> Availabilities { get; set; }
        private Environment Env { get; set; }

        public Teacher(int id, string name, List<Availability?> availabilities, Environment env)
        {
            Id = id;
            Name = name;
            Availabilities = availabilities;
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
        private int Id { get; }
        private string Name { get; set; }
        private List<SubjectAllocation?> Allocation { get; set; }
        private Environment Env { get; set; }

        public Subject(int id, string name, List<SubjectAllocation?> allocation, Environment env)
        {
            Id = id;
            Name = name;
            Allocation = allocation;
            Env = env;

            if (Env.GetDebug() == true) Program.PrintDebug("Created subject " + name + " with ID " + id);
        }

        public int GetId()
        {
            return Id;
        }
        public string GetName()
        {
            return Name;
        }
        public List<SubjectAllocation?> GetAllocation()
        {
            return Allocation;
        }
    }

    public class SubjectAllocation
    {
        public int Id { get; }
        public int GradeLevel { get; set; }
        public int WeeklyHours { get; set; }
        private Environment Env { get; set; }

        public SubjectAllocation(int id, int gradeLevel, int weeklyHours, Environment env)
        {
            Id = id;
            GradeLevel = gradeLevel;
            WeeklyHours = weeklyHours;
            Env = env;

            if (Env.GetDebug() == true) Program.PrintDebug("Created subject allocation with ID " + id);
        }
    }

    public class Availability
    {
        public int Id { get; }
        public DayOfWeek Day { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        private Environment Env { get; set; }

        public Availability(int id, DayOfWeek day, TimeOnly startTime, TimeOnly endTime, Environment env)
        {
            Id = id;
            Day = day;
            StartTime = startTime;
            EndTime = endTime;
            Env = env;

            if (Env.GetDebug() == true) Program.PrintDebug("Created aviability with ID " + id);
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
        private int Id { get; }
        private string Name { get; set; }
        private int Capacity { get; set; }
        public List<Availability?> Availabilities { get; set; }
        private RoomType Type { get; set; }
        private Environment Env { get; set; }

        public Room(int id, string name, int capacity, List<Availability?> availabilities, RoomType roomType, Environment env)
        {
            Id = id;
            Name = name;
            Capacity = capacity;
            Availabilities = availabilities;
            Type = roomType;
            Env = env;

            if (Env.GetDebug() == true) Program.PrintDebug("Created room " + name + " with ID " + id);
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
        public List<TimeOnly> StartTimes { get; set; } = new();
        public List<TimeOnly> EndTimes { get; set; } = new();
        private Environment Env { get; set; }

        public TimeGrid(int id, List<TimeOnly> startTimes, List<TimeOnly> endTimes, Environment env)
        {
            Id = id;
            StartTimes = startTimes;
            EndTimes = endTimes;
            Env = env;

            if (Env.GetDebug() == true) Program.PrintDebug("Created time grid with ID " + id);
        }
    }
}
