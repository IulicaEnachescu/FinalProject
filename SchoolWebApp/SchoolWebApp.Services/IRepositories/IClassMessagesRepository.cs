using SchoolDBModel.EntityTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebApp.Services.Interfaces
{
    public interface IClassMessagesRepository:IBaseRepository<ClassMessages>
    {
        IList<ClassMessages> GetMessagesByClassId(int id);
    }
}
