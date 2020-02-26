using SchoolDBModel.EntityTypes.ClassAndCourses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebApp.Services.Interfaces
{
    public interface IClassStudentRepository:IBaseRepository<ClassStudent>
    {
        IList<ClassStudent> GetStudentsIdByClassId(int id);
        IList<ClassStudent> GetClassIdByStudentId(int id);
    }
}
