using SchoolDBModel.EntityTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebApp.Services.Services
{
    public interface IStudentContactService
    {
        ContactPerson GetContactPersonByStudentId(int id);
    }
}
