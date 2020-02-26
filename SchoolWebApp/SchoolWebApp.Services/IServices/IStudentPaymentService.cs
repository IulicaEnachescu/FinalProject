using SchoolWebApp.Services.StudentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebApp.Services.IServices
{
    public interface IStudentPaymentService:IService
    {

        StudentStudentPayment GetStudentPaymentsByClassId(int stId, int classId);
        double GetStudentTotalPaymentByClassId(int stId, int classId);
    }
}
