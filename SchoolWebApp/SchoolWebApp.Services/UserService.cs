using SchoolWebApp.Services.Interfaces;
using SchoolWebApp.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        public bool UserNameNotFound(string name)
        {
            var totUsers = this._userRepository.GetAll();
            var el=totUsers.FirstOrDefault(x =>(x.UserName == name));
            return (Object.Equals(el, null));

        }
    }
}
