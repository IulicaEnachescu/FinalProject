using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDBModel.EntityTypes
{
    public class Admin:User
    {
              
        public int UserId { get; set; }
        public string Role { get; set; }
        //public IList<Admin> AllAdmins { get; set; } = new List<Admin>();

     }
}
