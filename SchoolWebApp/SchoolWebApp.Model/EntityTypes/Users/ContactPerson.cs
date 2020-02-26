using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDBModel.EntityTypes
{
    public class ContactPerson:EntityBase
    {
        
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        

        public string Email { get; set; }



        //public List<Student> Students { get; set; } = new List<Student>();
    }
}

