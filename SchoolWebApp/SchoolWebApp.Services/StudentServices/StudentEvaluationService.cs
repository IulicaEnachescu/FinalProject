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
    public class StudentEvaluationService:IStudentEvaluationService
    {
        private readonly IStudentRepository studentRepository;
        private readonly IClassRepository classRepository;
        private readonly IStudentClassEvaluationRepository evaluationRepository;



        public StudentEvaluationService(IStudentRepository studentRepository, IClassRepository classRepository,
            IStudentClassEvaluationRepository evaluationRepository)
        {
            this.studentRepository = studentRepository;
            this.classRepository = classRepository;
            this.evaluationRepository = evaluationRepository;
        }
        public decimal GetStudentEvaluationByClassId(int studId, int classId)
        {
            var evaluation = this.GetStudentGradesByClassId(studId, classId);
            int number = evaluation.DateGrades.Count();
            IList<int> grades = evaluation.DateGrades.Select(x => x.Grade).ToList();
            int sum = grades.Sum();
            return (decimal)sum / number;
        }

        public StudentGradesByClass GetStudentGradesByClassId(int studId, int classId)
        {
            var evaluation = this.evaluationRepository.GetStudentClassEvaluationByStudentIdAndByClassId(studId, classId);
            StudentGradesByClass st = new StudentGradesByClass();
            st.Student = this.studentRepository.GetById(studId);
            st.Class = this.classRepository.GetById(classId);
            IList<DateGrades> dateGrades = new List<DateGrades>();
            foreach (var item in evaluation)
            {
                var n = new DateGrades { Date = item.Date, Grade = item.Grade };
                dateGrades.Add(n);
            }
            st.DateGrades = dateGrades;
            return st;
        }
        public class StudentGradesByClass
        {
            public Student Student { get; set; }
            public Class Class { get; set; }

            public IList<DateGrades> DateGrades { get; set; }

        }
    }
    public class DateGrades
    {
        public DateTime Date { get; set; }
        public int Grade { get; set; }
    }
}

