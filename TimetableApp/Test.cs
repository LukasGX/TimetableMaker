namespace TimetableApp
{
    class Test
    {
        int lastId = -1;

        public void Start()
        {
            // create environment
            Environment stdEnv = new(GetId());
            stdEnv.Debug(false);

            // create test classes
            Class? c5a = stdEnv.CreateClass(GetId(), "5a", 5);
            Class? c5b = stdEnv.CreateClass(GetId(), "5b", 5);

            // create test students
            Student? sAlice = stdEnv.CreateStudent(GetId(), "Alice", c5a);
            Student? sBob = stdEnv.CreateStudent(GetId(), "Bob", c5a);
            Student? sCharlie = stdEnv.CreateStudent(GetId(), "Charlie", c5b);

            // create teacher avaibilities
            List<Availability?> availabilitiesFull = new()
            {
                stdEnv.CreateAvailability(GetId(), DayOfWeek.Monday, new(8, 0), new(17, 0)),
                stdEnv.CreateAvailability(GetId(), DayOfWeek.Tuesday, new(8, 0), new(17, 0)),
                stdEnv.CreateAvailability(GetId(), DayOfWeek.Wednesday, new(8, 0), new(17, 0)),
                stdEnv.CreateAvailability(GetId(), DayOfWeek.Thursday, new(8, 0), new(17, 0)),
                stdEnv.CreateAvailability(GetId(), DayOfWeek.Friday, new(8, 0), new(17, 0))
            };

            // create test teachers
            Teacher? tMrSmith = stdEnv.CreateTeacher(GetId(), "Mr. Smith", availabilitiesFull);
            Teacher? tMsJohnson = stdEnv.CreateTeacher(GetId(), "Ms. Johnson", availabilitiesFull);

            // create subject allocations for core subjects (WWG Bavaria, Germany)
            List<SubjectAllocation?> mathAllocations = new()
            {
                stdEnv.CreateSubjectAllocation(GetId(), 5, 4),
                stdEnv.CreateSubjectAllocation(GetId(), 6, 4),
                stdEnv.CreateSubjectAllocation(GetId(), 7, 4),
                stdEnv.CreateSubjectAllocation(GetId(), 8, 3),
                stdEnv.CreateSubjectAllocation(GetId(), 9, 4),
                stdEnv.CreateSubjectAllocation(GetId(), 10, 3),
                stdEnv.CreateSubjectAllocation(GetId(), 11, 3)
            };
            List<SubjectAllocation?> englishAllocations = new()
            {
                stdEnv.CreateSubjectAllocation(GetId(), 5, 5),
                stdEnv.CreateSubjectAllocation(GetId(), 6, 4),
                stdEnv.CreateSubjectAllocation(GetId(), 7, 4),
                stdEnv.CreateSubjectAllocation(GetId(), 8, 3),
                stdEnv.CreateSubjectAllocation(GetId(), 9, 3),
                stdEnv.CreateSubjectAllocation(GetId(), 10, 3),
                stdEnv.CreateSubjectAllocation(GetId(), 11, 3)
            };

            List<SubjectAllocation?> germanAllocations = new()
            {
                stdEnv.CreateSubjectAllocation(GetId(), 5, 5),
                stdEnv.CreateSubjectAllocation(GetId(), 6, 4),
                stdEnv.CreateSubjectAllocation(GetId(), 7, 4),
                stdEnv.CreateSubjectAllocation(GetId(), 8, 4),
                stdEnv.CreateSubjectAllocation(GetId(), 9, 4),
                stdEnv.CreateSubjectAllocation(GetId(), 10, 4),
                stdEnv.CreateSubjectAllocation(GetId(), 11, 3)
            };

            List<SubjectAllocation?> frenchAllocations = new()
            {
                stdEnv.CreateSubjectAllocation(GetId(), 6, 4),
                stdEnv.CreateSubjectAllocation(GetId(), 7, 4),
                stdEnv.CreateSubjectAllocation(GetId(), 8, 3),
                stdEnv.CreateSubjectAllocation(GetId(), 9, 3),
                stdEnv.CreateSubjectAllocation(GetId(), 10, 3),
                stdEnv.CreateSubjectAllocation(GetId(), 11, 3)
            };

            List<SubjectAllocation?> latinAllocations = new()
            {
                stdEnv.CreateSubjectAllocation(GetId(), 6, 4),
                stdEnv.CreateSubjectAllocation(GetId(), 7, 4),
                stdEnv.CreateSubjectAllocation(GetId(), 8, 3),
                stdEnv.CreateSubjectAllocation(GetId(), 9, 3),
                stdEnv.CreateSubjectAllocation(GetId(), 10, 3),
                stdEnv.CreateSubjectAllocation(GetId(), 11, 3)
            };

            List<SubjectAllocation?> spanishAllocations = new()
            {
                stdEnv.CreateSubjectAllocation(GetId(), 8, 4),
                stdEnv.CreateSubjectAllocation(GetId(), 9, 4),
                stdEnv.CreateSubjectAllocation(GetId(), 10, 3),
                stdEnv.CreateSubjectAllocation(GetId(), 11, 3)
            };

            List<SubjectAllocation?> biologyAllocations = new()
            {
                stdEnv.CreateSubjectAllocation(GetId(), 5, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 7, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 8, 2)
            };

            List<SubjectAllocation?> physicsAllocations = new()
            {
                stdEnv.CreateSubjectAllocation(GetId(), 8, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 9, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 10, 2)
            };

            List<SubjectAllocation?> chemistryAllocations = new()
            {
                stdEnv.CreateSubjectAllocation(GetId(), 9, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 10, 2)
            };

            List<SubjectAllocation?> historyAllocations = new()
            {
                stdEnv.CreateSubjectAllocation(GetId(), 6, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 7, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 9, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 10, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 11, 2)
            };

            List<SubjectAllocation?> geographyAllocations = new()
            {
                stdEnv.CreateSubjectAllocation(GetId(), 5, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 7, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 10, 2)
            };

            List<SubjectAllocation?> socialStudiesAllocations = new()
            {
                stdEnv.CreateSubjectAllocation(GetId(), 10, 1),
                stdEnv.CreateSubjectAllocation(GetId(), 11, 2)
            };

            List<SubjectAllocation?> religionAllocations = new()
            {
                stdEnv.CreateSubjectAllocation(GetId(), 5, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 6, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 7, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 8, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 9, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 10, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 11, 2)
            };

            List<SubjectAllocation?> ethicsAllocations = new()
            {
                stdEnv.CreateSubjectAllocation(GetId(), 5, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 6, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 7, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 8, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 9, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 10, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 11, 2)
            };

            List<SubjectAllocation?> musicAllocations = new()
            {
                stdEnv.CreateSubjectAllocation(GetId(), 5, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 6, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 7, 2)
            };

            List<SubjectAllocation?> artAllocations = new()
            {
                stdEnv.CreateSubjectAllocation(GetId(), 5, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 6, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 7, 2)
            };

            List<SubjectAllocation?> peAllocations = new()
            {
                stdEnv.CreateSubjectAllocation(GetId(), 5, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 6, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 7, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 8, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 9, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 10, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 11, 2)
            };

            // WWG profile subjects
            List<SubjectAllocation?> wrAllocations = new()
            {
                stdEnv.CreateSubjectAllocation(GetId(), 8, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 9, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 10, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 11, 2)
            };

            List<SubjectAllocation?> bwrAllocations = new()
            {
                stdEnv.CreateSubjectAllocation(GetId(), 8, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 9, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 10, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 11, 2)
            };

            List<SubjectAllocation?> winfAllocations = new()
            {
                stdEnv.CreateSubjectAllocation(GetId(), 9, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 10, 2),
                stdEnv.CreateSubjectAllocation(GetId(), 11, 2)
            };

            // create subjects
            Subject? mathematics = stdEnv.CreateSubject(GetId(), "Mathematics", mathAllocations);
            Subject? english = stdEnv.CreateSubject(GetId(), "English", englishAllocations);
            Subject? german = stdEnv.CreateSubject(GetId(), "German", germanAllocations);
            Subject? french = stdEnv.CreateSubject(GetId(), "French", frenchAllocations);
            Subject? latin = stdEnv.CreateSubject(GetId(), "Latin", latinAllocations);
            Subject? spanish = stdEnv.CreateSubject(GetId(), "Spanish", spanishAllocations);
            Subject? biology = stdEnv.CreateSubject(GetId(), "Biology", biologyAllocations);
            Subject? physics = stdEnv.CreateSubject(GetId(), "Physics", physicsAllocations);
            Subject? chemistry = stdEnv.CreateSubject(GetId(), "Chemistry", chemistryAllocations);
            Subject? history = stdEnv.CreateSubject(GetId(), "History", historyAllocations);
            Subject? geography = stdEnv.CreateSubject(GetId(), "Geography", geographyAllocations);
            Subject? socialStudies = stdEnv.CreateSubject(GetId(), "Social Studies", socialStudiesAllocations);
            Subject? religion = stdEnv.CreateSubject(GetId(), "Religion", religionAllocations);
            Subject? ethics = stdEnv.CreateSubject(GetId(), "Ethics", ethicsAllocations);
            Subject? music = stdEnv.CreateSubject(GetId(), "Music", musicAllocations);
            Subject? art = stdEnv.CreateSubject(GetId(), "Art", artAllocations);
            Subject? pe = stdEnv.CreateSubject(GetId(), "Physical Education", peAllocations);
            Subject? wr = stdEnv.CreateSubject(GetId(), "Economics and Law", wrAllocations);
            Subject? bwr = stdEnv.CreateSubject(GetId(), "Business Administration and Accounting", bwrAllocations);
            Subject? winf = stdEnv.CreateSubject(GetId(), "Business Informatics", winfAllocations);

            // assign subjects to teachers
            if (tMrSmith != null && mathematics != null) tMrSmith.Subjects.Add(mathematics);
            if (tMsJohnson != null && english != null) tMsJohnson.Subjects.Add(english);

            // additional teachers for other subjects
            Teacher? tGerman = stdEnv.CreateTeacher(GetId(), "Ms. Müller", availabilitiesFull);
            Teacher? tFrench = stdEnv.CreateTeacher(GetId(), "Mr. Dupont", availabilitiesFull);
            Teacher? tLatin = stdEnv.CreateTeacher(GetId(), "Ms. Aurelia", availabilitiesFull);
            Teacher? tSpanish = stdEnv.CreateTeacher(GetId(), "Mr. García", availabilitiesFull);
            Teacher? tBiology = stdEnv.CreateTeacher(GetId(), "Ms. Darwin", availabilitiesFull);
            Teacher? tPhysics = stdEnv.CreateTeacher(GetId(), "Mr. Newton", availabilitiesFull);
            Teacher? tChemistry = stdEnv.CreateTeacher(GetId(), "Ms. Curie", availabilitiesFull);
            Teacher? tHistory = stdEnv.CreateTeacher(GetId(), "Mr. Herodotus", availabilitiesFull);
            Teacher? tGeography = stdEnv.CreateTeacher(GetId(), "Ms. Humboldt", availabilitiesFull);
            Teacher? tSocial = stdEnv.CreateTeacher(GetId(), "Mr. Weber", availabilitiesFull);
            Teacher? tReligion = stdEnv.CreateTeacher(GetId(), "Ms. Faith", availabilitiesFull);
            Teacher? tEthics = stdEnv.CreateTeacher(GetId(), "Mr. Kant", availabilitiesFull);
            Teacher? tMusic = stdEnv.CreateTeacher(GetId(), "Ms. Mozart", availabilitiesFull);
            Teacher? tArt = stdEnv.CreateTeacher(GetId(), "Mr. Picasso", availabilitiesFull);
            Teacher? tPE = stdEnv.CreateTeacher(GetId(), "Ms. Fit", availabilitiesFull);
            Teacher? tWR = stdEnv.CreateTeacher(GetId(), "Mr. Schmitt", availabilitiesFull);
            Teacher? tBWR = stdEnv.CreateTeacher(GetId(), "Ms. Ledger", availabilitiesFull);
            Teacher? tWINF = stdEnv.CreateTeacher(GetId(), "Mr. Turing", availabilitiesFull);

            if (tGerman != null && german != null) tGerman.Subjects.Add(german);
            if (tFrench != null && french != null) tFrench.Subjects.Add(french);
            if (tLatin != null && latin != null) tLatin.Subjects.Add(latin);
            if (tSpanish != null && spanish != null) tSpanish.Subjects.Add(spanish);
            if (tBiology != null && biology != null) tBiology.Subjects.Add(biology);
            if (tPhysics != null && physics != null) tPhysics.Subjects.Add(physics);
            if (tChemistry != null && chemistry != null) tChemistry.Subjects.Add(chemistry);
            if (tHistory != null && history != null) tHistory.Subjects.Add(history);
            if (tGeography != null && geography != null) tGeography.Subjects.Add(geography);
            if (tSocial != null && socialStudies != null) tSocial.Subjects.Add(socialStudies);
            if (tReligion != null && religion != null) tReligion.Subjects.Add(religion);
            if (tEthics != null && ethics != null) tEthics.Subjects.Add(ethics);
            if (tMusic != null && music != null) tMusic.Subjects.Add(music);
            if (tArt != null && art != null) tArt.Subjects.Add(art);
            if (tPE != null && pe != null) tPE.Subjects.Add(pe);
            if (tWR != null && wr != null) tWR.Subjects.Add(wr);
            if (tBWR != null && bwr != null) tBWR.Subjects.Add(bwr);
            if (tWINF != null && winf != null) tWINF.Subjects.Add(winf);

            // create room availabilities
            List<Availability?> roomAvailabilities = new()
            {
                stdEnv.CreateAvailability(GetId(), DayOfWeek.Monday, new(8, 0), new(17, 0)),
                stdEnv.CreateAvailability(GetId(), DayOfWeek.Tuesday, new(8, 0), new(17, 0)),
                stdEnv.CreateAvailability(GetId(), DayOfWeek.Wednesday, new(8, 0), new(17, 0)),
                stdEnv.CreateAvailability(GetId(), DayOfWeek.Thursday, new(8, 0), new(17, 0)),
                stdEnv.CreateAvailability(GetId(), DayOfWeek.Friday, new(8, 0), new(17, 0))
            };

            // create rooms
            Room? r101 = stdEnv.CreateRoom(GetId(), "101", 30, roomAvailabilities, RoomType.Classroom);
            Room? r102 = stdEnv.CreateRoom(GetId(), "102", 30, roomAvailabilities, RoomType.Laboratory);

            // create time grid
            List<TimeOnly> startTimes = new()
            {
                new(07, 55),
                new(08, 40),
                new(09, 25),
                new(10, 25),
                new(11, 25),
                new(12, 10),
                new(13, 50),
                new(14, 35),
                new(15, 30),
                new(16, 15)
            };

            List<TimeOnly> endTimes = new()
            {
                new(08, 40),
                new(09, 25),
                new(10, 25),
                new(11, 10),
                new(12, 10),
                new(12, 55),
                new(14, 35),
                new(15, 20),
                new(16, 15),
                new(17, 00)
            };

            TimeGrid? stdTimeGrid = stdEnv.CreateTimeGrid(GetId(), startTimes, endTimes);

            // generate timetable
            Program.GenerateTimetable(stdEnv);
        }

        private int GetId()
        {
            lastId++;
            return lastId;
        }
    }
}