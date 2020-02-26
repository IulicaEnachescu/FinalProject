using SchoolDBModel.EntityTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebApp.Services.Interfaces
{
   public interface IStudentPaymentRepository:IBaseRepository<StudentPayment>
    {
        IList<StudentPayment> GetStudentPaymentsByStudentIdAndByClassId(int studentId, int classId);
        IList<StudentPayment> GetStudentPaymentsByClassId(int clId);
    }
}
