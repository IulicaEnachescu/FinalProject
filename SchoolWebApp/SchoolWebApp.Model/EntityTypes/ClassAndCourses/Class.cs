using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDBModel.EntityTypes
{
   public class Class:Course
    {
        
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string ClassDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Decimal Price { get; set; }



        //public IList<Student> Students { get; set; } = new List<Student>();
        //public IList<Message> Messages { get; set; } = new List<Message>();

        //public IList<ClassTimeTable> ClassTimetables { get; set; } = new List<ClassTimeTable>();

        //  public IList<StudentPayment> StudentPayments { get; set; }=new List<StudentPayment>(); 

        // public IList <StudentClassEvaluation> StudentClassEvaluations { get; set; } = new List<StudentClassEvaluation>();
        // public IList <Class> AllClasses { get; set; } = new List<Class>();

    }
}
