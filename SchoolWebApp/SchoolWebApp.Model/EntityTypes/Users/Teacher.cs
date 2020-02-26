using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDBModel.EntityTypes
{
   public class Teacher : User
    {
      public int UserId { get; set; }
        public string RoleDescription { get; set; }
        public bool StatusActive { get; set; }

      //  public IList<Class> TeacherClasses { get; set; } = new List<Class>();
      //  public IList<Teacher> TeachersList { get; set; } = new List<Teacher>();


    }
}

