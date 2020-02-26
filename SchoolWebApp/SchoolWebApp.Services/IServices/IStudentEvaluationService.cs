using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SchoolWebApp.Services.IServices
{
    interface IStudentEvaluationService:IService
    {
        StudentServices.StudentEvaluationService.StudentGradesByClass GetStudentGradesByClassId(int studId, int classId);
        decimal GetStudentEvaluationByClassId(int studId, int classId);
    }
}
