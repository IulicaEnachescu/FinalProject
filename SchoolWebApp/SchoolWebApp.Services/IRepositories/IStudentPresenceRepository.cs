using SchoolDBModel.EntityTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebApp.Services.Interfaces
{
    public interface IStudentPresenceRepository:IBaseRepository<StudentPresence>
    {
        IList<StudentPresence> GetPresenceByStudentId(int id);
        IList<StudentPresence> GetPresenceByClassTimetableId(int id);
        StudentPresence GetPresenceByStudentIdAndClassTimetableId(int stId, int clId);
    }
}
