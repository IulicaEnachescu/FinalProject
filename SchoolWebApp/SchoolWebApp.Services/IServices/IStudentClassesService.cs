using SchoolDBModel.EntityTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebApp.Services.IServices
{
    public interface IStudentClassesService:IService

    {
        IList<Class> GetClassesByStudentId(int id);
        IList<Student> GetStudentsByClassId(int id);
    }
}
