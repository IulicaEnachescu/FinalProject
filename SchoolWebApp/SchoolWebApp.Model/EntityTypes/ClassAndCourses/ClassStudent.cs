using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDBModel.EntityTypes.ClassAndCourses
{
   public class ClassStudent:EntityBase
    { public int ClassId { get; set; }
      public  int StudentId { get; set; }
        
    }
}
