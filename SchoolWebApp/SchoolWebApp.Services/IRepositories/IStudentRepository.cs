using SchoolDBModel.EntityTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolWebApp.Services.Interfaces;

namespace SchoolWebApp.Services.Interfaces
{
    public interface IStudentRepository:IBaseRepository<Student>
    {
       // IList<Student> GetStudentsByClassId(int id);
        IList<Student> GetStudentsByContactPersonId(int id);
    }
}

