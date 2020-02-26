using SchoolDBModel.EntityTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebApp.Services.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetUserByUserNameAndPassword(string name, string pass);



        User FindByUserName(string userName);
    }

}
