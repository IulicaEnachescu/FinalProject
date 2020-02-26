using SchoolDBModel.EntityTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebApp.Services.Interfaces
{
    public interface IStudentClassEvaluationRepository:IBaseRepository<StudentClassEvaluation>
    {
      
       IList<StudentClassEvaluation> GetStudentClassEvaluationById(int id);
     
        IList<StudentClassEvaluation> GetStudentClassEvaluationByStudentIdAndByClassId(int idStudent, int idClass);
    }
}
