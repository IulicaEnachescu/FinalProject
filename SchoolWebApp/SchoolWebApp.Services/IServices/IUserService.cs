using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebApp.Services.IServices
{
    public interface IUserService
    {
        bool UserNameNotFound(string name);
    }
}
