using SchoolDBModel.EntityTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebApp.Services.Interfaces
{
    public interface IClassTimetableRepository:IBaseRepository<ClassTimetable>
    {
        IList<ClassTimetable> GetClassTimetablesByClassId(int id);
    }
}
