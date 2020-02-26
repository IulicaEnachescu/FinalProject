using SchoolDBModel.EntityTypes;
using SchoolWebApp.Services.Interfaces;
using SchoolWebApp.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebApp.Services.StudentServices
{
    public class StudentPresenceService:IStudentPresenceService
    {
        private readonly IStudentRepository studentRepository;
        private readonly IClassRepository classRepository;
        private readonly IStudentPresenceRepository studentPresenceRepository;
        private readonly IClassTimetableRepository classTimetableRepository;



        public StudentPresenceService(IStudentRepository studentRepository, IStudentPresenceRepository studentPresenceRepository,
        IClassRepository classRepository, IClassTimetableRepository classTimetableRepository)
        {
            this.studentRepository = studentRepository;
            this.studentPresenceRepository = studentPresenceRepository;
            this.classRepository = classRepository;
            this.classTimetableRepository = classTimetableRepository;
        }

        public string GetStudentTotalPresenceByClassId(int studId, int classId)
        {
            var presence = this.GetStudentPresenceByClassId(studId, classId);
            int pres = presence.DatePresence.Count(x => x.Presence == true);
            double ratio = pres / presence.DatePresence.Count();
            return string.Format("{0:0.0%}", ratio);

        }


        public StudentPresenceByClass GetStudentPresenceByClassId(int studId, int classId)
        {
            var timeTables = this.classTimetableRepository.GetClassTimetablesByClassId(classId);
            var student = this.studentRepository.GetById(studId);
            var clas = this.classRepository.GetById(classId);
            StudentPresenceByClass st = new StudentPresenceByClass();
            st.Student = student;
            st.Class = clas;
            foreach (var item in timeTables)
            {
                var presence = this.studentPresenceRepository.GetPresenceByStudentIdAndClassTimetableId(studId, item.Id);
                DatePresence datepresence = new DatePresence();
                datepresence.Date = item.ClassDate;
                datepresence.Presence = presence.Presence;
                st.DatePresence.Add(datepresence);
            }
            return st;


        }
    }
    public class StudentPresenceByClass
    {
        public Student Student { get; set; }
        public Class Class { get; set; }
        public IList<DatePresence> DatePresence { get; set; }
    }
    public class DatePresence
    {
        public DateTime Date { get; set; }
        public bool Presence { get; set; }
    }
}

