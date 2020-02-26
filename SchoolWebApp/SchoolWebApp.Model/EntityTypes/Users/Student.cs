using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDBModel.EntityTypes
{
    public class Student:User
    {
        

        public int UserId { get; set; }
        public bool StatusActive { get; set; }

        public int ContactId { get; set; }


        //public IList<Student> AllStudentsList { get; set; } = new List<Student>();


    }
}

