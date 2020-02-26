using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDBModel.EntityTypes
{
    public class ClassTimetable:EntityBase
    {
        public int ClassId { get; set; }
        public int LessonNumber { get; set; }
        public DateTime ClassDate { get; set; }


    }
}
