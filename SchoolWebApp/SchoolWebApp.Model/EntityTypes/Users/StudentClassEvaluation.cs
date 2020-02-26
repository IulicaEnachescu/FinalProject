using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDBModel.EntityTypes
{
   public class StudentClassEvaluation:EntityBase
    {

        public int ClassId { get; set; }
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
        public int Grade { get; set; }

        
    }
}
