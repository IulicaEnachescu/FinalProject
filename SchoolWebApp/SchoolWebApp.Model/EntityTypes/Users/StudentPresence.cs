using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDBModel.EntityTypes
{
    public class StudentPresence : EntityBase
    {
        public int ClassTimetableId { get; set; }
        public int StudentId { get; set; }
        public bool Presence { get; set; }

    }
}
