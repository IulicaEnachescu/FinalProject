using SchoolWebApp.Services.StudentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebApp.Services.IServices
{
    interface IStudentPresenceService:IService
    {
        StudentPresenceByClass GetStudentPresenceByClassId(int studId, int classId);
        string GetStudentTotalPresenceByClassId(int studId, int classId);
    }
}
