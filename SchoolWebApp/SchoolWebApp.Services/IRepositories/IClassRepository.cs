using SchoolDBModel.EntityTypes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebApp.Services.Interfaces
{
   public interface IClassRepository: IBaseRepository<Class>
    {
        //IList<Class> GetClassesByStudentId(int id);
        IList<Class> GetClassesByCourseId(int id);
        IList<Class> GetClassesByTeacherId(int id);

    }
}
