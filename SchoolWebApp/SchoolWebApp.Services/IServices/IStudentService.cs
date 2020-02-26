using SchoolDBModel.EntityTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebApp.Services.IServices
{
   public interface IStudentService:IService
    {
        Student GetStudentWithUser(int id);
       IList<Student> GetAllStudentsWithUser();
    }
}
